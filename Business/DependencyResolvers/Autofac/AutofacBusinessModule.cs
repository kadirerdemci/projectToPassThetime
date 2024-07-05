using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<FileHelperManager>().As<IFileHelper>().SingleInstance();
            builder.RegisterType<FileLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<RoleManager>().As<IRoleService>().SingleInstance();
            builder.RegisterType<EfRoleDal>().As<IRoleDal>().SingleInstance();
            builder.RegisterType<PermissionManager>().As<IPermissionService>().SingleInstance();
            builder.RegisterType<EfPermissionDal>().As<IPermissionDal>().SingleInstance();
            builder.RegisterType<UserRoleManager>().As<IUserRoleService>().SingleInstance();
            builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>().SingleInstance();
            builder.RegisterType<RolePermissionManager>().As<IRolePermissionService>().SingleInstance();
            builder.RegisterType<EfRolePermissionDal>().As<IRolePermissionDal>().SingleInstance();
            builder.RegisterType<LogManager>().As<ILogService>();
            builder.RegisterType<EfLogDal>().As<ILogDal>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();
			builder.RegisterType<ProductManager>().As<IProductService>();

			// Tüm sınıflar için oluşturulmuş aspect varmı? Varsa bunları kontrol et ve çalıştır. 
			var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
