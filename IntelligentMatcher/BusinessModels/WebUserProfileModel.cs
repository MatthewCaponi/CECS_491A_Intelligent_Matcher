using System;

namespace UserManagement.Models
{
    public class WebUserProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public int UserAccountId { get; set; }
    }
}
