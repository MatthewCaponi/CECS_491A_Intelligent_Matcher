using BusinessModels.UserAccessControl;
using System.Collections.Generic;

namespace AuthorizationPolicySystem
{
    public interface IAuthorizationPolicyManager
    {
        AccessPolicyModel ConfigureCustomPolicy(List<string> scopes, List<UserClaimModel> claims);
        AccessPolicyModel ConfigureDefaultPolicy(string resource, string role, bool read, bool write);
        AccessPolicyModel ConfigureDefaultPolicy(string resource, string role, string id, bool read, bool write);
    }
}