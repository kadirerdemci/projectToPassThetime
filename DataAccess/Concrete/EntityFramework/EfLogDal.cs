using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLogDal :  EfEntityRepositoryBase<Log, ETradeContext>, ILogDal
    {
        public void DeleteAll()
        {
            using (var context = new ETradeContext())
            {
                context.Database.ExecuteSqlRaw("TRUNCATE TABLE Logs");
            }
        }
    }
}
