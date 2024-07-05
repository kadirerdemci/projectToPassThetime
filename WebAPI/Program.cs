using System.Collections.ObjectModel;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.CCS;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using Serilog.Events;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using ILogger = Business.CCS.ILogger;
using Core.CrossCuttingConcerns.Caching;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
// Autofac implementation

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

var connectionString = "Server=DESKTOP-I4OGBFN;Database=testdb2;Integrated Security=True;TrustServerCertificate=True;";
var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn { ColumnName = "LogMessage", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1 },
        new SqlColumn { ColumnName = "LogMessageTemplate", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1 },
        new SqlColumn { ColumnName = "LogLevel", DataType = System.Data.SqlDbType.NVarChar, DataLength = 128 },
        new SqlColumn { ColumnName = "LogTimeStamp", DataType = System.Data.SqlDbType.DateTime },
        new SqlColumn { ColumnName = "LogException", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1 },
        new SqlColumn { ColumnName = "LogProperties", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1 },
        new SqlColumn { ColumnName = "LogEvent", DataType = System.Data.SqlDbType.NVarChar, DataLength = -1 }
    }
};

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
        columnOptions: columnOptions,
        restrictedToMinimumLevel: LogEventLevel.Information
    )
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("Authorization");
    logging.ResponseHeaders.Add("Authorization");
    logging.ResponseBodyLogLimit = 100;
    logging.CombineLogs = false;
});

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddMemoryCache();


var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.JWT.TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule(),
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(option => new HttpLoggingOptions());


var app = builder.Build();


// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.ConfigureCustomExceptionMiddleware();

app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

app.UseSerilogRequestLogging();

app.Use(async (context, next) =>
{
    context.Response.Headers["MyResponseHeader"] =
        new string[] { "My Response Header Value" };
    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
