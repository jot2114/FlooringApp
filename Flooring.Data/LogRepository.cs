using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class LogRepository
    {
        string filePath = @"DataFiles\logs.txt";

        public void logError(Exception ex)
        {
            var logs = GetAllLogs();
            var log = new Log() {Date = DateTime.Now, Message = ex.Message.Replace(",",";")};

            logs.Add(log);  //it is still in a variable

            OverwriteFile(logs);
        }

        public List<Log> GetAllLogs()
        {
            var logs = new List<Log>();
            if (File.Exists(filePath))
            {
                var rows = File.ReadAllLines(filePath);

                for (int i = 1; i < rows.Length; i++)
                {
                    var log = new Log();
                   
                    var columns = rows[i].Split(',');
                    log.Message = columns[0];
                    log.Date = DateTime.Parse(columns[1]);
                    logs.Add(log);
                }   
            }
            return logs;
        }

        private void OverwriteFile(List<Log> logs )
        {
            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine( "Messsage, Date");
                foreach (var log in logs)
                {
                    writer.WriteLine("{0},{1}",log.Message,log.Date);
                }
            }
        }
    }
}
