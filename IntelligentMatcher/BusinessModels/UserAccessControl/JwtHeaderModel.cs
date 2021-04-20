using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class JwtHeaderModel
    {
        public string Type { get; set; }
        public string SignAlgorithm { get; set; }
    }
}
