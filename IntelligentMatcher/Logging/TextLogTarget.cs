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
            if(eventname == EventName.SecurityEvent)
            {
                WriteSecurityLog(message);
            }

            if (eventname == EventName.NetworkEvent)
            {
                WriteNetworkLog(message);
            }
            if (eventname == EventName.UserEvent)
            {
                WriteUserLog(message);
            }
        }

        //Writes to the security log folder
        //Checks to see if a log file alrady exists for the day if so appends if not create a new one and append to it
        private void WriteSecurityLog(string message)
        {

            string securityLogPath = "logs/security/security-log- " + DateTime.Today + ".txt";
            if (!File.Exists(securityLogPath)){

                using (StreamWriter writer = File.CreateText(securityLogPath))
                {
                    writer.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(securityLogPath))
                {
                    writer.WriteLine(message);
                }
            }

        }

        //Writes to the network log folder
        //Checks to see if a log file alrady exists for the day if so appends if not create a new one and append to it
        private void WriteNetworkLog(string message)
        {
            string securityLogPath = "logs/network/network-log- " + DateTime.Today + ".txt";
            if (!File.Exists(securityLogPath))
            {

                using (StreamWriter writer = File.CreateText(securityLogPath))
                {
                    writer.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(securityLogPath))
                {
                    writer.WriteLine(message);
                }
            }
        }
        //Writes to the user log folder
        //Checks to see if a log file alrady exists for the day if so appends if not create a new one and append to it
        private void WriteUserLog(string message)
        {
            string securityLogPath = "logs/user/user-log- " + DateTime.Today + ".txt";
            if (!File.Exists(securityLogPath))
            {

                using (StreamWriter writer = File.CreateText(securityLogPath))
                {
                    writer.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(securityLogPath))
                {
                    writer.WriteLine(message);
                }
            }
        }
    }
}
