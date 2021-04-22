using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class AssignmentPolicyPairingModel
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public int ScopeId { get; set; }
    }
}
