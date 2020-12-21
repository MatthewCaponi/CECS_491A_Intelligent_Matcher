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

        public UserCreateModel(string userName, string password, string firstName, string lastName, string dateOfBirth, AccountType accountType, string email)
        {
            Username = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            this.accountType = accountType;
            this.email = email;
        }
    }
}
