using System;
using System.Collections.Generic;
using System.Text;
using static Models.UserProfileModel;

namespace UserManagement.Models
{
    public class UserCreateModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public AccountType accountType { get; set; }
        public string email { get; set; }
    }
}
