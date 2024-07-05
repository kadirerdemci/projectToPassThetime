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
    public interface IRoleService
    {

        IDataResult<Role> GetById(int roleId);
        IDataResult<List<Role>> GetList();
        IResult Add(RoleForAddDto roleForAddDto);
        IResult Update(Role role);
        IResult DeleteById(int roleId);
    }
}
