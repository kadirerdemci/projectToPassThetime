using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ETradeContext>, IUserDal
    {
        public List<Permission> GetPermissions(User user)
        {
            using (var context = new ETradeContext())
            {
                var result = from permission in context.Permissions
                             join rolePermission in context.RolePermissions
                                 on permission.Id equals rolePermission.PermissionId
                             join userRole in context.UserRoles
                                 on rolePermission.RoleId equals userRole.RoleId
                             where userRole.UserId == user.Id
                             select new Permission { Id = permission.Id, Name = permission.Name };
                return result.ToList();
            }
        }

        public List<Role> GetRoles(User user)
        {
            using (var context = new ETradeContext())
            {
                var result = from role in context.Roles
                             join userRole in context.UserRoles
                                 on role.Id equals userRole.RoleId
                             where userRole.UserId == user.Id
                             select new Role { Id = role.Id, Name = role.Name };
                return result.ToList();

            }
        }
    }
}
