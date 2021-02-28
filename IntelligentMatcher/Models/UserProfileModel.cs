using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public int UserAccountId { get; set; }

    }
}
