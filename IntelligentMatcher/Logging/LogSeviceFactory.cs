using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class LogSeviceFactory : ILogServiceFactory
    {
        private TargetType _targetType;
        private List<ILogTarget> _logTargets;
    
        public void AddTarget(TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Console:
                    {
                        _logTargets.Add(new ConsoleLogTarget());
                        break;
                    }

                case TargetType.Json:
                    {
                        _logTargets.Add(new JsonLogTarget());
                        break;
                    }

                case TargetType.Text:
                    {
                        _logTargets.Add(new TextLogTarget());
                        break;
                    }
            }            
        }

        public ILogService CreateLogService<T>(List<ILogTarget> targets)
        {
            return new LogService(_logTargets);
        }
    }
}
