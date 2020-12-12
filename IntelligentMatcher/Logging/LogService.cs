using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class LogService : ILogService
    {
        private readonly List<ILogTarget> _logTargets;

        public LogService(List<ILogTarget> logTargets)
        {
            _logTargets = logTargets;
        }

        public void LogCritical<T>(ILoggingEvent loggingEvent, Exception e, string message)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        private void SetMessage(DateTime dateTime, string caller,)
        {

        }

        private void WriteToTargets(List<ILogTarget> targets)
        {

        }

       
    }
}
