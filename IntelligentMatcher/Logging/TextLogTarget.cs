using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Logging
{
    public class TextLogTarget : ILogTarget
    {
        public TextLogTarget()
        {

        }
        public void LogToTarget(string message, EventName eventName)
        {
            // Read the file as one string. 
            string fileName = $"{eventName}{(DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.txt";
            string directory = $"C:\\Users\\{ Environment.UserName}\\logs\\{ eventName.ToString()}";
            string logPath = Path.Combine(directory, fileName);

            //create the log directory under the user profile if it does not exist
            if (!Directory.Exists(directory))
            {
                DirectoryInfo di = Directory.CreateDirectory(directory);
            }

            //create the file first then write
            Console.WriteLine(logPath);
            if (!File.Exists(logPath))
            {
                using (StreamWriter writer = File.CreateText(logPath))
                {
                    writer.WriteLine(message);
                }
            }
            //if file exists just write to the log file
            else
            {
                using (StreamWriter writer = File.AppendText(logPath))
                {
                    writer.WriteLine(message);
                }
            }
        }
    }



}
