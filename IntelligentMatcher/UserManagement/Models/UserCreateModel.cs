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
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public string Email { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public UserCreateModel(string userName, string password, string firstName, string lastName, 
            string dateOfBirth, string accountType, string accountStatus, string email, DateTime accountCreationDate)
        {
            Username = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            AccountType = accountType;
            AccountStatus = accountStatus;
            Email = email;
            AccountCreationDate = accountCreationDate;
        }

        public UserCreateModel()
        {
        }
    }
}
