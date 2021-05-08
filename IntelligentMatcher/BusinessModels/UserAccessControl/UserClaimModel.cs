using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class UserClaimModel
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public UserClaimModel(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
