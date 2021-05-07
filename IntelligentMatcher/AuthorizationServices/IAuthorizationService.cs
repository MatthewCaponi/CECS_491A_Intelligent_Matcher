using BusinessModels.UserAccessControl;

namespace UserAccessControlServices
{
    public interface IAuthorizationService
    {
        bool Validate(ClaimsPrincipal claimsPrincipal);
    }
}