using BusinessModels.UserAccessControl;
using System;
using System.Collections.Generic;

namespace AuthorizationPolicySystem
{
    public class AuthorizationPolicyManager : IAuthorizationPolicyManager
    {
        public AccessPolicyModel ConfigureCustomPolicy(List<string> scopes, List<UserClaimModel> claims)
        {
            var accessPolicy = new AccessPolicyModel()
            {

                Scopes = scopes,
                Claims = claims
            };

            return accessPolicy;
        }
    }
}
