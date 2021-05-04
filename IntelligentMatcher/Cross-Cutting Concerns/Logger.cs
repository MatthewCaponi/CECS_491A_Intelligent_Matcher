using Logging;
using System;

namespace Cross_Cutting_Concerns
{
    public class Logger
    {
        public static ILogService LogService { get; set; }
    }
}
