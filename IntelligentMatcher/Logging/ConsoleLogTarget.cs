using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class ConsoleLogTarget : ILogTarget
    {
        public void LogToTarget(string message , EventName eventname)
        {

            Console.WriteLine("New " + eventname.ToString() + ": " + message);
        }

    }
}
