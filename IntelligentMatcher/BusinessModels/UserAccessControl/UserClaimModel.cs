using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class UserClaimModel
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public UserClaimModel()
        {

        }

        public UserClaimModel(string key, string value)
        {
            Type = key;
            Value = value;
        }
    }
}
