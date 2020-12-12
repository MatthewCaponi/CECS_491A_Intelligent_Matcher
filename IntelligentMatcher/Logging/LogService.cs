using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.critical, loggingEvent, eventName, e);
        }

        public void LogCritical(ILoggingEvent loggingEvent, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.critical, loggingEvent, eventName);
        }

        public void LogCritical(string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            WriteToTargets(currentDateTime, caller, message, LogLevel.critical);
        }


        public void LogDebug(ILoggingEvent loggingEvent, Exception e, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.debug, loggingEvent, eventName, e);
        }

        public void LogDebug(ILoggingEvent loggingEvent, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.debug, loggingEvent, eventName);
        }

        public void LogDebug(string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            WriteToTargets(currentDateTime, caller, message, LogLevel.debug);
        }

        public void LogError(ILoggingEvent loggingEvent, Exception e, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.error, loggingEvent, eventName, e);
        }

        public void LogError(ILoggingEvent loggingEvent, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.error, loggingEvent, eventName);
        }

        public void LogError(string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            WriteToTargets(currentDateTime, caller, message, LogLevel.error);
        }

        public void LogInfo(ILoggingEvent loggingEvent, Exception e, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.info, loggingEvent, eventName, e);
        }

        public void LogInfo(ILoggingEvent loggingEvent, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.info, loggingEvent, eventName);
        }

        public void LogInfo(string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            WriteToTargets(currentDateTime, caller, message, LogLevel.info);
        }

        public void LogTrace(ILoggingEvent loggingEvent, Exception e, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.trace, loggingEvent, eventName, e);
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
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            WriteToTargets(currentDateTime, caller, message, LogLevel.trace);
        }

        public void LogWarning(ILoggingEvent loggingEvent, Exception e, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.warning, loggingEvent, eventName, e);
        }

        public void LogWarning(ILoggingEvent loggingEvent, string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            var eventName = loggingEvent.GetEventName();
            WriteToTargets(currentDateTime, caller, message, LogLevel.warning, loggingEvent, eventName);
        }

        public void LogWarning(string message)
        {
            var currentDateTime = GetCurrentDateTime();
            var caller = SetCaller(typeof(T));
            WriteToTargets(currentDateTime, caller, message, LogLevel.warning);
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


        private void WriteToTargets(DateTime dateTime, string caller, string message, LogLevel logLevel, ILoggingEvent loggingEvent, EventName eventName, Exception e)
        {
            foreach (ILogTarget target in _logTargets)
            {
                Type targetType = target.GetType();

                if (targetType == typeof(TextLogTarget))
                {
                    var builtMessage = $"{ dateTime } { logLevel.ToString() } { caller } {loggingEvent.GetEventInfo()} { loggingEvent.GetEventInfo().ToString() } {e} {message}";
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
                            Type = logType,
                            Error = e
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
                            UserAgent = userAgent,
                            Error = e

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
                            URL = url,
                            Error = e

                        };

                        var builtMessage = JsonSerializer.Serialize(anonLog);

                        ILogTarget logTarget = new JsonLogTarget();

                        logTarget.LogToTarget(builtMessage, eventName);
                    }
                }
                else if (targetType == typeof(ConsoleLogTarget))
                {
                    var builtMessage = $"{ logLevel.ToString() } : { dateTime } { caller } { loggingEvent.GetEventInfo().ToString() } {e} { message }";
                    ILogTarget logTarget = new ConsoleLogTarget();
                    logTarget.LogToTarget(builtMessage, eventName);
                }
            }
        }

        private void WriteToTargets(DateTime dateTime, string caller, string message, LogLevel logLevel)
        {
            var builtMessage = $"{ logLevel.ToString() } : { dateTime } { caller } { message }";
            string fileName = "Generic" + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + "Generic";
            string logPath = Path.Combine(directory, fileName);

            //create the log directory under the user profile if it does not exist
            if (!Directory.Exists(directory))
            {
                DirectoryInfo di = Directory.CreateDirectory(directory);
            }

            //create the file first then write
            Console.WriteLine(logPath);
            if (!File.Exists(logPath))
            {
                try
                {
                    using (StreamWriter writer = File.CreateText(logPath))
                    {
                        writer.WriteLine(message);
                    }
                }
                catch
                {
                    throw new IOException("Cannot Write To file");
                }
            }
            //if file exists just write to the log file
            else
            {
                try { 
                    using (StreamWriter writer = File.AppendText(logPath))
                    {
                        writer.WriteLine(message);
                    }
                }
                catch
                {
                    throw new IOException("Cannot Write To file");
                }
        }


        

    }


    private string SetCaller(Type type)
        {
            return type.ToString();
        }

    }
}
