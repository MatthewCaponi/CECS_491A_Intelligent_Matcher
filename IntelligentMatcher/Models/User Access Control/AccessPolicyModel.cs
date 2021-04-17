using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class AccessPolicyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResourceId { get; set; }
        public int Priority { get; set; }
    }
}
