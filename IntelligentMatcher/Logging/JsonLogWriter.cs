using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Logging
{
    public class JsonLogWriter : ILogWriter
    {
        public void Write(IDictionary<string, string> message)
        {
            // Read the file as one string. 
            string fileName = $"{(DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.json";
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            string jsonDirectory = $"{projectDirectory}\\logs\\json";
            string logPath = Path.Combine(jsonDirectory, fileName);

            Debug.WriteLine("Json Log Created At: " + jsonDirectory);
            Console.WriteLine("Json Log Created At: " + jsonDirectory);
            var realJson = JsonConvert.SerializeObject(message);

            //create the log directory under the user profile if it does not exist
            if (!Directory.Exists(jsonDirectory))
            {
                DirectoryInfo di = Directory.CreateDirectory(jsonDirectory);
            }                                                                                                              

            //create the file first then write
                if (!File.Exists(logPath))
                {
                    using (StreamWriter writer = File.CreateText(logPath))
                    {
                    }
                }
                //if file exists just write to the log file
                
                using (FileStream fileStream = new FileStream(logPath, FileMode.Open, FileAccess.ReadWrite))
                {
                    var info = new FileInfo(logPath);
                    fileStream.Seek(0, SeekOrigin.End);

                    byte[] convertedText;

                    if (info.Length > 6)
                    {
                        fileStream.SetLength(fileStream.Length - 1);
                        convertedText = Encoding.ASCII.GetBytes(", \n\"" + DateTime.UtcNow + "\": " + realJson + "}");
                    }
                    else
                    {
                        convertedText = Encoding.ASCII.GetBytes("{\"" + DateTime.UtcNow + "\": " + realJson + "}");
                    }

                    fileStream.Write(convertedText, 0, convertedText.Length);
                }
        }     
    }
}
