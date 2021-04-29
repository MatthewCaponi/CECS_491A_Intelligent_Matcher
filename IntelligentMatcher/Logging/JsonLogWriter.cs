using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Logging
{
    public class JsonLogWriter : ILogWriter
    {
        public void Write(IDictionary<string, string> message)
        {

            // Read the file as one string. 
            string fileName = $"Json-{(DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.json";
            string directory = $"C:\\Users\\{ Environment.UserName}\\logs\\Json";
            string logPath = Path.Combine(directory, fileName);

            var realJson = JsonConvert.SerializeObject(message);

            //create the log directory under the user profile if it does not exist
            if (!Directory.Exists(directory))
            {
                DirectoryInfo di = Directory.CreateDirectory(directory);
            }                                                                                                              

            //create the file first then write
            Console.WriteLine(logPath);
            using (FileStream fileStream = new FileStream(logPath, FileMode.Open, FileAccess.ReadWrite))
            {
                if (!File.Exists(logPath))
                {
                    using (StreamWriter writer = File.CreateText(logPath))
                    {
                        writer.WriteLine(realJson);
                    }
                }
                //if file exists just write to the log file
                else
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
}
