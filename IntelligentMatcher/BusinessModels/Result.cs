using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public ErrorMessage ErrorMessage { get; set; }
        public T SuccessValue { get; set; }
    }
}
