using BusinessModels.UserAccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserAccessControlServices
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool ValidateAccessPolicy(ClaimsPrincipal claimsPrincipal, AccessPolicyModel accessPolicy)
        {
            var claims = claimsPrincipal.Claims;
            var scopes = claimsPrincipal.Scopes;

            foreach (var scope in accessPolicy.Scopes)
            {
                if (!scopes.Contains(scope))
                {
                    return false;
                }
            }

            foreach (var claim in accessPolicy.Claims)
            {
                var key = claims.Where(a => a.Type == claim.Type).FirstOrDefault();

                if (key == null)
                {
                    return false;
                }

                if (key.Value != claim.Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
