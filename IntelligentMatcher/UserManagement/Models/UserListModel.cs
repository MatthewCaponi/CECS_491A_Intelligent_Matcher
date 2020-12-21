using System;

namespace UserManagement
{
    public class UserListModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public UserListModel(int userId, string username, string firstName, string lastName, DateTime accountCreationDate)
        {
            UserId = userId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            AccountCreationDate = accountCreationDate;
        }

        public UserListModel()
        {
        }
    }
}