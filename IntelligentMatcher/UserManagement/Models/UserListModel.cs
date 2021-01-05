using System;
using static Models.UserProfileModel;

namespace UserManagement
{
    public class UserListModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public AccountStatus accountStatus { get; set; }

        public UserListModel(int userId, string username, string firstName, string lastName, DateTime accountCreationDate, AccountStatus accountStatus)
        {
            UserId = userId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            AccountCreationDate = accountCreationDate;
            this.accountStatus = accountStatus;
        }

        public UserListModel()
        {
        }
    }
}