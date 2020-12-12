using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        }

        public void LogTrace(ILoggingEvent loggingEvent, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.trace, loggingEvent, eventName);
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

        private void WriteToTargets(DateTime dateTime, string caller, string message, LogLevel logLevel, ILoggingEvent loggingEvent, EventName eventName)
        {
            foreach (ILogTarget target in _logTargets)
            {
                Type targetType = target.GetType();

                if (targetType == typeof(TextLogTarget))
                {
                    var builtMessage = $"{ dateTime } { logLevel.ToString() } { caller } {loggingEvent.GetEventInfo()} { loggingEvent.GetEventInfo().ToString() } {message}";
                    ILogTarget logTarget = new TextLogTarget();
                    logTarget.LogToTarget(builtMessage, eventName);
                }

                else if (targetType == typeof(JsonLogTarget))
                {

                    if (loggingEvent.GetEventName().ToString() == "UserEvent")
                    {
                        string[] logginEventsSplit = loggingEvent.GetEventInfo().ToString().Split(' ');

                        string userID = logginEventsSplit[0];
                        string ipAddress = logginEventsSplit[1];
                        string logType = logginEventsSplit[2];

                        var anonLog = new
                        {
                            DateTime = dateTime,
                            LogLevel = logLevel.ToString(),
                            Caller = caller,
                            Message = message,
                            UserID = userID,
                            IP = ipAddress,
                            Type = logType
                        };

                        var builtMessage = JsonSerializer.Serialize(anonLog);

                        ILogTarget logTarget = new JsonLogTarget();

                        logTarget.LogToTarget(builtMessage, eventName);
                    }
                    else if (loggingEvent.GetEventName().ToString() == "NetworkEvent")
                    {
                        string[] logginEventsSplit = loggingEvent.GetEventInfo().ToString().Split(' ');

                        string userID = logginEventsSplit[0];
                        string ipAddress = logginEventsSplit[1];
                        string pageRequest = logginEventsSplit[2];
                        string urlReferrer = logginEventsSplit[3];
                        string userAgent = logginEventsSplit[4];

                        var anonLog = new
                        {
                            DateTime = dateTime,
                            LogLevel = logLevel.ToString(),
                            Caller = caller,
                            Message = message,
                            UserID = userID,
                            IP = ipAddress,
                            PageRequest = pageRequest,
                            UrlRefferer = urlReferrer,
                            UserAgent = userAgent
                        };

                        var builtMessage = JsonSerializer.Serialize(anonLog);

                        ILogTarget logTarget = new JsonLogTarget();

                        logTarget.LogToTarget(builtMessage, eventName);
                    }
                    else if (loggingEvent.GetEventName().ToString() == "SecurityEvent")
                    {
                        string[] logginEventsSplit = loggingEvent.GetEventInfo().ToString().Split(' ');

                        string userID = logginEventsSplit[0];
                        string url = logginEventsSplit[1];

                        var anonLog = new
                        {
                            DateTime = dateTime,
                            LogLevel = logLevel.ToString(),
                            Caller = caller,
                            Message = message,
                            UserID = userID,
                            URL = url
                        };

                        var builtMessage = JsonSerializer.Serialize(anonLog);

                        ILogTarget logTarget = new JsonLogTarget();

                        logTarget.LogToTarget(builtMessage, eventName);
                    }
                }
                else if (targetType == typeof(ConsoleLogTarget))
                {
                    var builtMessage = $"{ logLevel.ToString() } : { dateTime } { caller } { loggingEvent.GetEventInfo().ToString() } { message }";
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
