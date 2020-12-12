using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayerUnitTests
{
    public static class LogTargetHelper
    {
        public static string ReadTestLog(string path)
        {
            string message = "";
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    message = s;
                }

                return message;
            }
        }
    }
}

 public class ConsoleOutputReader : IDisposable
    {
        StringWriter sw;
        TextWriter oo;

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
