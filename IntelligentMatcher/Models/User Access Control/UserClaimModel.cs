using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class UserClaimModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int UserAccountId { get; set; }
    }
}
