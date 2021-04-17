using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class ScopeClaimModel
    {
        public int Id { get; set; }
        public int ScopeId { get; set; }
        public int ClaimId { get; set; }
    }
}
