using System;

namespace Exceptions
{
    [Serializable]
    public class SqlCustomException : Exception
    {
        public SqlCustomException() { }

        public SqlCustomException(string message)
            : base(message) {  }

        public SqlCustomException(string message, Exception inner)
            : base(message, inner) { }
    }

}
