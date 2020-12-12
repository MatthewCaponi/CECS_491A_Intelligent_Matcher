using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class UserDisplayModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public UserDisplayModel()
        {

        }
        public UserDisplayModel(int userId, string username, string firstName, string lastName, DateTime accountCreationDate)
        {
            UserId = userId;
            Username = username;
            FirstName = FirstName;
            LastName = LastName;
            AccountCreationDate = AccountCreationDate;
        }

    }
}
