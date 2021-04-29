using System;
using System.Collections.Generic;
using System.Reflection;

namespace Logging
{
    public class LogService : ILogService
    {
        public void Log(string message, LogTarget logTarget)
        {
            Log(message, logTarget, LogLevel.info, null);
        }

        public void Log(string message, LogTarget logTarget, LogLevel logLevel)
        {
            Log(message, logTarget, logLevel, null);
        }

        public void Log(string message, LogTarget logTarget, Exception exception)
        {
            Log(message, logTarget, LogLevel.info, exception);
        }

        public void Log(string message, LogTarget logTarget, LogLevel logLevel, Exception exception)
        {
            DateTime date = DateTime.UtcNow;

            IDictionary<string, string> typeValues = new Dictionary<string, string>()
            {
                {"Log Level", logLevel.ToString() },
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
