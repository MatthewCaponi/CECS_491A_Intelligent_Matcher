using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class AccessPolicyPairingModel
    {
        public int Id { get; set; }
        public int ScopeId { get; set; }
        public int ClaimId { get; set; }
        public int AccessPolicyId { get; set; }
    }
}
