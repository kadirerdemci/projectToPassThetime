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
    public class UserRoleManager: IUserRoleService
    {

        private IUserRoleDal _userRoleDal;

        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }
        public IDataResult<UserRole> GetById(int userRoleId)
        {
            var result = _userRoleDal.Get(x => x.Id == userRoleId);
            return new SuccessDataResult<UserRole>(result);
        }

        public IDataResult<List<UserRole>> GetList()
        {
            var result = _userRoleDal.GetList();
            return new SuccessDataResult<List<UserRole>>(result);
        }

        public IResult Add(UserRoleForAddDto userRoleForAddDto)
        {
            var userRole = new UserRole
            {
                UserId = userRoleForAddDto.UserId,
                RoleId = userRoleForAddDto.RoleId
            };
            _userRoleDal.Add(userRole);
            return new SuccessResult();
        }

        public IResult Update(UserRole userRole)
        {
            _userRoleDal.Update(userRole);
            return new SuccessResult();
        }

        public IResult DeleteById(int userRoleId)
        {
            _userRoleDal.Delete(_userRoleDal.Get(x => x.Id == userRoleId));
            return new SuccessResult();
        }
    }
}
