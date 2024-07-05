using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {

        private IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public IDataResult<Role> GetById(int roleId)
        {
            return new SuccessDataResult<Role>(_roleDal.Get(r => r.Id == roleId));
        }

        public IDataResult<List<Role>> GetList()
        {
            return new SuccessDataResult<List<Role>>(_roleDal.GetList());
        }

        public IResult Add(RoleForAddDto roleForAddDto)
        {
            var role = new Role
            {
                Name = roleForAddDto.Name
            };
            _roleDal.Add(role);
            return new SuccessResult();
        }

        public IResult Update(Role role)
        {
	        _roleDal.UpdateAsync(role); 
            return new SuccessResult();
        }

        public IResult DeleteById(int roleId)
        {
            _roleDal.Delete(_roleDal.Get(r => r.Id == roleId));
            return new SuccessResult();
        }
    }
}
