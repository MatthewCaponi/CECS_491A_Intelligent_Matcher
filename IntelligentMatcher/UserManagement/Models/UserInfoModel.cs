using System;
using System.Collections.Generic;
using System.Text;
using static Models.UserProfileModel;

namespace UserManagement.Models
{
    public class UserInfoModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string accountType { get; set; }
        public string email { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public string accountStatus { get; set; }
    }
}
