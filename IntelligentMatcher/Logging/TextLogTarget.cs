using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logging
{
    public class TextLogTarget : ILogTarget
    {
        public void LogToTarget(string message)
        {
            throw new NotImplementedException();
        }

        private void WriteText(string message, TextWriter w)
        {

        }
    }
}
