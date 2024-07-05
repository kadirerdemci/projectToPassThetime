using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IRolePermissionService
    {
        IDataResult<RolePermission> GetById(int rolePermissionId);
        IDataResult<List<RolePermission>> GetList();
        IResult Add(RolePermission rolePermission);
        IResult Update(RolePermission rolePermission);
        IResult DeleteById(int rolePermissionId);
        Task<IResult> BulkDeleteAsync(List<RolePermission> rolePermissions);
	}
}
