using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class ClaimsPrincipal
    {
        public int Id { get; set; }
        public List<UserClaimModel> Claims { get; set; }
        public List<string> Scopes { get; set; }
    }
}

