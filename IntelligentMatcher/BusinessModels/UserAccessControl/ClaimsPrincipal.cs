using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class ClaimsPrincipal
    {
        public string Role { get; set; }
        public List<UserScopeModel> Scopes { get; set; }
        public List<UserClaimModel> Claims { get; set; }
        public int UserAccountId { get; set; }

    }
}
