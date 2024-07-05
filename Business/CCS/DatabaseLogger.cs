using System;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.CCS
{
    public class DatabaseLogger : ILogger
    {
        private readonly ETradeContext _context;

        public DatabaseLogger(ETradeContext context)
        {
            _context = context;
        }

        public void Log()
        {
            var log = new Log
            {
                Message = "Loglandı",
                Level = "Info",
                TimeStamp = DateTime.Now
            };

            _context.Logs.Add(log);
            _context.SaveChanges();

            Console.WriteLine("Veritabanına loglandı: ");
        }
    }
}