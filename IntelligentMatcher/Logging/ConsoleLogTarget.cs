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

        public static string GetConsoleOutput()
        {
            var currentConsoleOut = Console.Out;

            using (var consoleOutputReader = new ConsoleOutputReader())
            {
                consoleOutputReader.GetOuput();
            }

            return Console.Out.ToString();
        }

    }
}



public class ConsoleOutputReader : IDisposable
{
    static StringWriter sw;
    static TextWriter oo;

    public ConsoleOutputReader()
    {
        sw = new StringWriter();
        oo = Console.Out;
        Console.SetOut(sw);
    }

    public string GetOuput()
    {
        return sw.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(oo);
        oo.Dispose();
    }
}
