using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserListTransferModel
    {
        public int UserAccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public DateTime AccountCreationDate { get; set; }
    }
}
