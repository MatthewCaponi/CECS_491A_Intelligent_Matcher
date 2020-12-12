using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logging
{
    public class ConsoleLogTarget : ILogTarget
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public void LogToTarget(string message , EventName eventname)
        {
            Console.Write("New " + eventname.ToString() + ": " + message);

        }



    }
}



