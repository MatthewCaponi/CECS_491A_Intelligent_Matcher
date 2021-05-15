using BusinessModels.UserAccessControl;
using System.Collections.Generic;

namespace AuthorizationPolicySystem
{
    public interface IAuthorizationPolicyManager
    {
        AccessPolicyModel ConfigureCustomPolicy(List<string> scopes, List<UserClaimModel> claims);
    }
}