using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public interface ILogService
    {
        void Log(string message, LogTarget logTarget, string caller);
        void Log(string message, LogTarget logTarget, LogLevel logLevel, string caller);
        void Log(string message, LogTarget logTarget, Exception exception, string caller);
        void Log(string message, LogTarget logTarget, LogLevel logLevel, Exception exception, string caller);

    }
}
