using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public enum TargetType
    {
        Console,
        Text,
        Json
    }

    public interface ILogServiceFactory
    {
        ILogService CreateLogService<T>();

        void AddTarget(TargetType targetType);
    }
}
