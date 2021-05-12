using BusinessModels.UserAccessControl;
using System.Collections.Generic;

namespace AuthorizationPolicySystem
{
    public interface IAuthorizationPolicyManager
    {
        AccessPolicyModel ConfigureCustomPolicy(List<string> scopes, List<UserClaimModel> claims);
        AccessPolicyModel ConfigureDefaultPolicy(string resource, string role, bool read, bool write, bool delete);
        AccessPolicyModel ConfigureDefaultPolicy(string resource, string role, string id, bool read, bool write, bool delete);
        AccessPolicyModel ConfigureCustomPolicy(string scope, int id);
        AccessPolicyModel ConfigureCustomPolicy(string scope, string role);
        AccessPolicyModel ConfigureCustomPolicy(string scope, string role, string id);
    }
}