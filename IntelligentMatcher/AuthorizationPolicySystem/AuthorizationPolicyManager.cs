using BusinessModels.UserAccessControl;
using System;
using System.Collections.Generic;

namespace AuthorizationPolicySystem
{
    public class AuthorizationPolicyManager : IAuthorizationPolicyManager
    {
        public AccessPolicyModel ConfigureDefaultPolicy(string resource, string role, bool read, bool write)
        {
            var scopes = new List<string>();
            if (write)
            {
                scopes.Add(nameof(write));
            }

            scopes.Add(nameof(read));
            scopes.Add(resource);

            var accessPolicy = new AccessPolicyModel()
            {

                Scopes = scopes,
                Claims = new List<UserClaimModel>()
                {
                    new UserClaimModel()
                    {
                        Type = nameof(role),
                        Value = role
                    }
                }
            };

            return accessPolicy;
        }

        public AccessPolicyModel ConfigureDefaultPolicy(string resource, string role, string id, bool read, bool write)
        {
            var scopes = new List<string>();
            if (write)
            {
                scopes.Add(nameof(write));
            }

            scopes.Add(nameof(read));
            scopes.Add(resource);

            var accessPolicy = new AccessPolicyModel()
            {

                Scopes = scopes,
                Claims = new List<UserClaimModel>()
                {
                    new UserClaimModel()
                    {
                        Type = nameof(role),
                        Value = role
                    },
                    new UserClaimModel()
                    {
                        Type = nameof(id),
                        Value = id.ToString()
                    }
                }
            };

            return accessPolicy;
        }

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
