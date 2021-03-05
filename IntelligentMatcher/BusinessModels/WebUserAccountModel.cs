using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Models
{
    public class WebUserAccountModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset UpdationDate { get; set; }
    }
}
