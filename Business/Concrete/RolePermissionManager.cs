using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class RolePermissionManager : IRolePermissionService
    {
        private IRolePermissionDal _rolePermissionDal;
        public RolePermissionManager(IRolePermissionDal rolePermissionDal)
        {
            _rolePermissionDal = rolePermissionDal;
        }
        public IDataResult<RolePermission> GetById(int rolePermissionId)
        {
            return new SuccessDataResult<RolePermission>(_rolePermissionDal.Get(r => r.Id == rolePermissionId));
        }
        public IDataResult<List<RolePermission>> GetList()
        {
            return new SuccessDataResult<List<RolePermission>>(_rolePermissionDal.GetList());
        }
        public IResult Add(RolePermission rolePermission)
        {
            _rolePermissionDal.AddAsync(rolePermission);
            return new SuccessResult();
        }
        public IResult Update(RolePermission rolePermission)
        {
            _rolePermissionDal.Update(rolePermission);
            return new SuccessResult();
        }
        public IResult DeleteById(int rolePermissionId)
        {
            _rolePermissionDal.Delete(_rolePermissionDal.Get(r => r.Id == rolePermissionId));
            return new SuccessResult();
        }

		public Task<IResult> BulkDeleteAsync(List<RolePermission> rolePermissions)
		{
			try
			{
				_rolePermissionDal.BulkDeleteAsync(rolePermissions);
				return Task.FromResult<IResult>(new SuccessResult());
			}
			catch (Exception e)
			{
				return Task.FromResult<IResult>(new ErrorResult(e.Message));
			}
		}
	}
}
