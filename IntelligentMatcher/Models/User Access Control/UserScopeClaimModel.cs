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
        public string Role { get; set; }

        public UserScopeClaimModel()
        {

        }

        public UserScopeClaimModel(int id, int userAccountId, int scopeClaimId, string role)
        {
            Id = id;
            UserAccountId = userAccountId;
            ScopeClaimId = scopeClaimId;
            Role = role;
        }
    }
}
