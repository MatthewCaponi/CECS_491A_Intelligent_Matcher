using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public interface ILogService
    {
        void Log(string message, LogTarget logTarget);
        void Log(string message, LogTarget logTarget, LogLevel logLevel);
        void Log(string message, LogTarget logTarget, Exception exception);
        void Log(string message, LogTarget logTarget, LogLevel logLevel, Exception exception);

    }
}
