using System;

namespace UserManagement.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string EmailAddress { get; set; }

        public UserModel(string userName, string password, string firstName, 
            string lastName, string dateOfBirth, string accountType, string accountStatus, 
            string emailAddress, DateTime accountCreationDate)
        {
            Username = userName;
            FirstName = firstName;
            Surname = lastName;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
        }
    }
}
