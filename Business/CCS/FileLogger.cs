using System;
using System.IO;

namespace Business.CCS
{
    public class FileLogger : ILogger
    {
        private readonly string _filepath;

        public FileLogger(string filepath)
        {
            _filepath = filepath;
        }

        public void Log()
        {
            using (StreamWriter file = new StreamWriter(_filepath, true))
            {
                file.WriteLine("Dosyaya loglandı: " + DateTime.Now);
            }
            Console.WriteLine("Dosyaya loglandı");
        }
    }
}