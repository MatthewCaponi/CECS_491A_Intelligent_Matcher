using BusinessModels.UserAccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserAccessControlServices
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool Validate(ClaimsPrincipal claimsPrincipal)
        {
            var role = claimsPrincipal.Claims.Where(a => a.Type == "Role").FirstOrDefault();

            if (!(claimsPrincipal.Scopes.Contains("User Management") && claimsPrincipal.Scopes.Contains("Read") && role.Value == "Admin"))
            {
                return false;
            }

            return true;
        }
    }
}
