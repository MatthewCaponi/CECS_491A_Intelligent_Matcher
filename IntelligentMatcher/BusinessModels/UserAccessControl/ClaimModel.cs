using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsDefault { get; set; }
    }
}
