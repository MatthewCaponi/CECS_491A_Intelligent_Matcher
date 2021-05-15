using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class UserClaimModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int UserAccountId { get; set; }

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
