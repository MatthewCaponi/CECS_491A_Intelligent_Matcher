using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Models
{
    public class UserAccountModel
    {
        public string Username { get; set; }
        public AccountType AccountType { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}
