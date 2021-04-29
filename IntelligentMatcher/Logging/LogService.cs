using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Logging
{
    public class LogService : ILogService
    {
        public void Log(string message, LogTarget logTarget, string caller)
        {
            Log(message, logTarget, LogLevel.info, null, caller);
        }

        public void Log(string message, LogTarget logTarget, LogLevel logLevel, string caller)
        {
            Log(message, logTarget, logLevel, null, caller);
        }

        public void Log(string message, LogTarget logTarget, Exception exception, string caller)
        {
            Log(message, logTarget, LogLevel.info, exception, caller);
        }

        public void Log(string message, LogTarget logTarget, LogLevel logLevel, Exception exception, string caller)
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

            WriteToTarget(typeValues, logTarget);
        }

        private void WriteToTarget(IDictionary<string, string> finalMessage, LogTarget logTarget)
        {
            ILogWriter logger = (ILogWriter)Activator.CreateInstance(Type.GetType($"{Type.GetType(this.ToString()).Namespace}.{logTarget}LogWriter"));
            logger.Write(finalMessage);
        }
    }
}
