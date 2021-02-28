using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Models
{
    public class UserAccountModel
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public AccountType AccountType { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset UpdationDate { get; set; }
    }
}
