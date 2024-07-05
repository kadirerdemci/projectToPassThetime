using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class LogManager : ILogService
    {

        private ILogDal _logDal;

        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public IResult DeleteAll()
        {
            _logDal.DeleteAll();
            return new SuccessResult();
        }

        public List<Log> GetAll()
        {
            return _logDal.GetList();
        }
    }
}
