using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.trace, eventName);
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

        private void WriteToTargets(DateTime dateTime, string caller, string message, LogLevel logLevel, EventName eventName)
        {
            foreach (ILogTarget target in _logTargets)
            {
                Type targetType = target.GetType();

                if (targetType == typeof(TextLogTarget))
                {
                    var builtMessage = $"{ dateTime } { logLevel.ToString() } { caller } { message }";
                    ILogTarget logTarget = new TextLogTarget();
                    logTarget.LogToTarget(builtMessage, eventName);
                }

                else if (targetType == typeof(JsonLogTarget))
                {
                    var anonLog = new
                    {
                        DateTime = dateTime,
                        LogLevel = logLevel.ToString(),
                        Caller = caller,
                        Message = message
                    };

                    var builtMessage = JsonSerializer.Serialize(anonLog);

                    ILogTarget logTarget = new JsonLogTarget();

                    logTarget.LogToTarget(builtMessage, eventName);
                }
                else if (targetType == typeof(ConsoleLogTarget))
                {
                    var builtMessage = $"{ logLevel.ToString() } : { dateTime } { caller } { message }";
                    ILogTarget logTarget = new ConsoleLogTarget();
                    logTarget.LogToTarget(builtMessage, eventName);

                }
            }
        }
            
        private string SetCaller(Type type)
        {
            return type.ToString();
        }

    }
}
