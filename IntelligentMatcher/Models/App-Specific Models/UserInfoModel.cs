using System;
using System.Collections.Generic;
using System.Text;

namespace Models.App_Specific_Models
{
    public class UserInfoModel: UserProfileModel
    {
        public DateTimeOffset DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
    }
}
