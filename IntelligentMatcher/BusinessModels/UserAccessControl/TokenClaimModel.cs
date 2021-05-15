using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class TokenClaimModel
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public TokenClaimModel()
        {

        }
        public TokenClaimModel(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
