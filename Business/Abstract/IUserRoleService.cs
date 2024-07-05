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
    public interface IUserRoleService
    {
        IDataResult<UserRole> GetById(int userRoleId);
        IDataResult<List<UserRole>> GetList();
        IResult Add(UserRoleForAddDto userRoleForAddDto);
        IResult Update(UserRole userRole);
        IResult DeleteById(int userRoleId);
    }
}
