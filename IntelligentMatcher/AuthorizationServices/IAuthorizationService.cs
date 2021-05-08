using BusinessModels.UserAccessControl;

namespace UserAccessControlServices
{
    public interface IAuthorizationService
    {
        bool ValidateAccessPolicy(ClaimsPrincipal claimsPrincipal, AccessPolicyModel accessPolicy);
    }
}