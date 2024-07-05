using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<Role>> GetRoles(User user);
        IResult Add(User user);
        IDataResult<User> GetByMail(string email);

        List<User> GetAll();
        IDataResult<List<Permission>> GetPermissions(User user);
    }
}
