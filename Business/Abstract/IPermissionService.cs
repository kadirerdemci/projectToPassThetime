using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IPermissionService
    {
        IDataResult<Permission> GetById(int permissionId);
        IDataResult<List<Permission>> GetList();
        IResult Add(PermissionAddDto permissionAddDto);
        IResult Delete(int permissionId);
        IResult Update(Permission permission);
        IResult DeleteAll();
		//Count
		int Count();
	}
}
