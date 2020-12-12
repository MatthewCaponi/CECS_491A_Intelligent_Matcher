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
        public string accountType { get; set; }
        public enum AccountStatus { Active, Disabled, Suspended, Banned, Deleted}
        public string accountStatus { get; set; }
        public int userAccountId { get; set; }


        public UserProfileModel()
        {

        }

        public UserProfileModel(string firstName, string lastName, DateTime dateOfBirth, DateTime accountCreationDate, string accountType, string accountStatus, int userAccountId)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            AccountCreationDate = accountCreationDate;
            this.accountType = accountType;
            this.accountStatus = accountStatus;
            this.userAccountId = userAccountId;
        }


    }
}
