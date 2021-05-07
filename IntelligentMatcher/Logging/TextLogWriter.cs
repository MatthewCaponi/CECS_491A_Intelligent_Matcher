using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Logging
{
    public class TextLogWriter : ILogWriter
    {
        public void Write(IDictionary<string, string> message, string folder)
        {
            string fileName = $"{folder}_{(DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.txt";
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).FullName;
            string textDirectory = $"{projectDirectory}\\logs\\{folder}\\text";
            string logPath = Path.Combine(textDirectory, fileName);

            string builtMessage = "";

            foreach(var key in message)
            {
                builtMessage += key.Value + " ";
            }
            //create the log directory under the user profile if it does not exist
            if (!Directory.Exists(textDirectory))
            {
                DirectoryInfo di = Directory.CreateDirectory(textDirectory);
            }

            //create the file first then write
            Console.WriteLine("Text Log Created At: " + textDirectory);
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
