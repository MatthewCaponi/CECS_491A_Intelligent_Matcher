using System;

namespace UserManagement.Models
{
    public class WebUserProfileModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public int UserAccountId { get; set; }

    }
}
