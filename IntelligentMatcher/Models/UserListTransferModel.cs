using System;
using System.Collections.Generic;
using System.Text;
using static Models.UserProfileModel;

namespace Models
{
    public class UserListTransferModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public AccountStatus accountStatus { get; set; }
    }
}
