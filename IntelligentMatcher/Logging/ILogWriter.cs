using System.Collections.Generic;

namespace Logging
{
    public interface ILogWriter
    {
        void Write(IDictionary<string, string> message, string folder);
    }
}