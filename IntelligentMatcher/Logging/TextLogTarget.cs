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

            WriteLog(message, eventname);
            

        }

        //Writes to the security log folder
        //Checks to see if a log file alrady exists for the day if so appends if not create a new one and append to it
        private void WriteLog(string message, EventName eventname)
        {

            string logPath = "logs/" + eventname.ToString() + "/" + eventname.ToString() + "-log- " + DateTime.Today + ".txt";
            if (!File.Exists(logPath)){

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

        //Writes to the network log folder
        //Checks to see if a log file alrady exists for the day if so appends if not create a new one and append to it
   
        //Writes to the user log folder
        //Checks to see if a log file alrady exists for the day if so appends if not create a new one and append to it
 
    }
}
