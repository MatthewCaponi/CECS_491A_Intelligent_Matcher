using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserProfileModel: GenericEntityModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int userAccountId { get; set; }

    }
}
