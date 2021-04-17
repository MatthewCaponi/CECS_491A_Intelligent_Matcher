using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class AssignmentPolicyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string RequiredAccountType { get; set; }
        public int Priority { get; set; }
    }
}
