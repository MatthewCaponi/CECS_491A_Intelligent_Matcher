using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Logging
{
    public class TextLogWriter : ILogWriter
    {
        public void Write(IDictionary<string, string> message)
        {
            // Read the file as one string. 
            string fileName = $"Text-{(DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.txt";
            string directory = $"C:\\Users\\{ Environment.UserName}\\logs\\Text";
            string logPath = Path.Combine(directory, fileName);

            string builtMessage = "";

            foreach(var value in message)
            {
                builtMessage += value + " ";
            }
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
                    writer.WriteLine(builtMessage);
                }
            }
            //if file exists just write to the log file
            else
            {
                using (StreamWriter writer = File.AppendText(logPath))
                {
                    writer.WriteLine(builtMessage);
                }
            }
        }
    }



}
