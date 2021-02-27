using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserAccountModel : GenericEntityModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
    }
}
