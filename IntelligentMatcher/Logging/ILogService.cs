using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public interface ILogService
    {
        void LogTrace(ILoggingEvent loggingEvent, Exception e, string message);

        void LogTrace(ILoggingEvent loggingEvent, string message);

        void LogTrace(string message);

        void LogDebug(ILoggingEvent loggingEvent, Exception e, string message);

        void LogDebug(ILoggingEvent loggingEvent, string message);

        void LogDebug(string message);

        void LogInfo(ILoggingEvent loggingEvent, Exception e, string message);

        void LogInfo(ILoggingEvent loggingEvent, string message);

        void LogInfo(string message);

        void LogWarning(ILoggingEvent loggingEvent, Exception e, string message);

        void LogWarning(ILoggingEvent loggingEvent, string message);

        void LogWarning(string message);

        void LogError(ILoggingEvent loggingEvent, Exception e, string message);

        void LogError(ILoggingEvent loggingEvent, string message);

        void LogError(string message);

        void LogCritical(ILoggingEvent loggingEvent, Exception e, string message);

        void LogCritical(ILoggingEvent loggingEvent, string message);

        void LogCritical(string message);
    }
}
