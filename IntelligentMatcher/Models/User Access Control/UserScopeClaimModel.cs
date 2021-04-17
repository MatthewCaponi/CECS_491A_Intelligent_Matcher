using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class UserScopeClaimModel
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public int ScopeClaimId { get; set; }
    }
}
