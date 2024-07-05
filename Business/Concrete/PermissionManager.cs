using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.CCS;
using Core.Aspects.Autofac.Caching;

using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;

namespace Business.Concrete
{
    public class PermissionManager : IPermissionService
    {

        private IPermissionDal _permissionDal;
 

        public PermissionManager(IPermissionDal permissionDal)
        {
 
            _permissionDal = permissionDal;
        }

        [CacheAspect]
        public IDataResult<Permission> GetById(int permissionId)
        {
            return new SuccessDataResult<Permission>(_permissionDal.Get(p => p.Id == permissionId));
        }

        public IDataResult<List<Permission>> GetList()
        {

            return new SuccessDataResult<List<Permission>>(_permissionDal.GetList());
        }
        public IResult Add(PermissionAddDto permissionAddDto)
        {
            Permission permission = new Permission
            {
                Name = permissionAddDto.Name
            };
            _permissionDal.Add(permission);
            return new SuccessResult();
        }
        public IResult Delete(int permissionId)
        {
            _permissionDal.Delete(_permissionDal.Get(p => p.Id == permissionId));
            return new SuccessResult();
        }
        public IResult Update(Permission permission)
        {
            _permissionDal.Update(permission);
            return new SuccessResult();
        }

        public IResult DeleteAll()
        {
            _permissionDal.BulkDelete(new List<Permission>());
            return new SuccessResult();
        }

		public int Count()
		{
			return _permissionDal.Count();
		}
	}
}
