using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserAccountModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

        public UserAccountModel()
        {

        }

        public UserAccountModel(string username, string password, string email)
        {
            Username = username;
            Password = password;
            EmailAddress = email;
        }

        public UserAccountModel(int id)
        {
            this.Id = id;
        }
    }
}
