using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;


namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        [TransactionScopeAspect]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded); 
        }

        [CacheRemoveAspect("Business.Abstract.IUserService.GetAll()")]
        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));

        }

        [PerformanceAspect(10)]
        [CacheAspect]
        public List<User> GetAll()
        {
            return _userDal.GetList();
        }
        public IDataResult<List<Role>> GetRoles(User user)
        {
            return new SuccessDataResult<List<Role>>(_userDal.GetRoles(user));
        }
        public IDataResult<List<Permission>> GetPermissions(User user)
        {
            return new SuccessDataResult<List<Permission>>(_userDal.GetPermissions(user));
        }

    }
}
