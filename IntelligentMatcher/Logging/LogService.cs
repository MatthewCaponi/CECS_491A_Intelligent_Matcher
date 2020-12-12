using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class LogService<T> : ILogService
    {
        private readonly List<ILogTarget> _logTargets;

        public LogService(List<ILogTarget> logTargets)
        {
            _logTargets = logTargets;
        }

        public void LogCritical(ILoggingEvent loggingEvent, Exception e, string message)
        {

            Console.WriteLine("An error has occured");

            Console.WriteLine("Hello");

        }

        public void LogCritical(ILoggingEvent loggingEvent, string message)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(string message)
        {
            throw new NotImplementedException();
        }


        public void LogDebug(ILoggingEvent loggingEvent, Exception e, string message)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(ILoggingEvent loggingEvent, string message)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(string message)
        {
            throw new NotImplementedException();
        }

        public void LogError(ILoggingEvent loggingEvent, Exception e, string message)
        {
            throw new NotImplementedException();
        }

        public void LogError(ILoggingEvent loggingEvent, string message)
        {
            throw new NotImplementedException();
        }

        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(ILoggingEvent loggingEvent, Exception e, string message)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(ILoggingEvent loggingEvent, string message)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(ILoggingEvent loggingEvent, Exception e, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));

            WriteToTargets(currentDateTime, caller, message, LogLevel.trace);

            
            
        }

        

        public void LogTrace(ILoggingEvent loggingEvent, string message)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(string message)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(ILoggingEvent loggingEvent, Exception e, string message)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(ILoggingEvent loggingEvent, string message)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message)
        {
            throw new NotImplementedException();
        }

        private DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow;
        }

        private void WriteToTargets(DateTime dateTime, string caller, string message, LogLevel logLevel)
        {
            foreach (ILogTarget target in _logTargets)
            {
                Type targetType = target.GetType();

                if (targetType == typeof(TextLogTarget))
                {
                    var builtMessage = $"{ dateTime } { logLevel.ToString() } { caller } { message }";
                    ILogTarget logTarget = new TextLogTarget();
                    logTarget.LogToTarget(builtMessage);
                }


                else if (targetType == typeof(JsonLogTarget))
                {

                }
                else if (targetType == typeof(ConsoleLogTarget))
                {

                }
            }


        }
            
        private string SetCaller(Type type)
        {
            return type.ToString();
        }

    }
}
