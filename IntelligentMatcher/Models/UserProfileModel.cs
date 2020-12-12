using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  DateTime DateOfBirth { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public enum AccountType { Admin, User };
        public AccountType accountType { get; set; }
        public enum AccountStatus { Active, Disabled, Suspended, Banned, Deleted}
        public AccountStatus accountStatus { get; set; }
        public UserAccountModel userAccountModel { get; set; }

    }
}
