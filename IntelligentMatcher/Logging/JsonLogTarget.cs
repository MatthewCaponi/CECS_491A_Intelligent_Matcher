using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Logging
{
    public class JsonLogTarget : ILogTarget
    {
        public void LogToTarget(string message, EventName eventname)
        {
            string logPath = "logs/" + eventname.ToString() + "/" + eventname.ToString() + "-log- " + DateTime.Today + ".json";
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

}
