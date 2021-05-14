using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class UserScopeClaimModel
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public int UserScopeId { get; set; }
        public int UserClaimId { get; set; }
        public string Role { get; set; }

        public UserScopeClaimModel()
        {

        }

        public UserScopeClaimModel(int id, int userAccountId, int userScopeId, int userClaimId, string role)
        {
            Id = id;
            UserAccountId = userAccountId;
            UserScopeId = userScopeId;
            UserClaimId = userClaimId;
            Role = role;
        }
    }
}
