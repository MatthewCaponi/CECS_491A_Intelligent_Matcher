using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BusinessLayerUnitTests
{
    public static class LogTargetHelper
    {
        public static string ReadTestLog(EventName eventName, TargetType targetType)
        {
            string fileName = "";
            switch (targetType)
            {
                case TargetType.Json:
                     fileName = $"{eventName.ToString()}{(DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.json";
                    break;

                case TargetType.Text:
                     fileName = $"{eventName.ToString()}{(DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.txt";
                    break;
            }
            
            string directory = $"C:\\Users\\{Environment.UserName}\\logs\\{eventName.ToString()}";
            string logPath = Path.Combine(directory, fileName);

            var message = File.ReadLines(logPath).Last();

            return message;
        }
    }
}

public class ConsoleOutputChecker : IDisposable
{
    private StringWriter stringWriter;
    private TextWriter originalOutput;

    public ConsoleOutputChecker()
    {
        stringWriter = new StringWriter();
        originalOutput = Console.Out;
        Console.SetOut(stringWriter);
    }

    public string GetOuput()
    {
        return stringWriter.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(originalOutput);
        stringWriter.Dispose();
    }
}
