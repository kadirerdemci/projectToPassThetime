using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using ZstdSharp;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private string[] _permissions;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles, string permissions)
        {
            _roles = roles.Split(',');
            _permissions = permissions.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>()!;

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext?.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    var permissionClaims = _httpContextAccessor.HttpContext?.User.Claims("permissions");
                    foreach (var permission in _permissions)
                    {
                        if (permissionClaims.Contains(permission))
                        {
                            return;
                        }
                    }
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
