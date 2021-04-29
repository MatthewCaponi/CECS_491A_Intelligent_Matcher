using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Logging
{
    public class LogService : ILogService
    {
        public void Log(string message, LogTarget logTarget, LogLevel logLevel, string caller, string folder)
        {
            Log(message, logTarget, logLevel, null, caller, folder);
        }

        public void Log(string message, LogTarget logTarget, LogLevel logLevel, Exception exception, string caller, string folder)
        {
            DateTime date = DateTime.UtcNow;

            IDictionary<string, string> typeValues = new Dictionary<string, string>()
            {
                {"Log Level", logLevel.ToString() },
                {"Caller", caller },
                {"Date", date.ToString() },
                {"Message", message }
            };

            if (exception != null)
            {
                typeValues.Add("Exception", exception.Message);
            }

            WriteToTarget(typeValues, logTarget, folder);
        }

        private void WriteToTarget(IDictionary<string, string> finalMessage, LogTarget logTarget, string folder)
        {
            ILogWriter logger = (ILogWriter)Activator.CreateInstance(Type.GetType($"{Type.GetType(this.ToString()).Namespace}.{logTarget}LogWriter"));
            logger.Write(finalMessage, folder);
        }
    }
}
