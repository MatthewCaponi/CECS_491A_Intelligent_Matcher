using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logging
{
    public class TextLogTarget : ILogTarget
    {
        public void LogToTarget(string message, EventName eventname)
        {
            string fileName = eventname + DateTime.Today.Date.ToString() + ".txt";
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(logPath))
            {

                using (StreamWriter writer = File.CreateText(logPath))
                {
                    writer.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(logPath))
                {
                    writer.WriteLine(message);
                }
            }


        }
    }

    //Writes to the log folder and selects the correct folder baised on the enum value


}
