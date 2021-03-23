using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels
{
    public class ResultModel<T>
    {
        public ErrorMessage ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
