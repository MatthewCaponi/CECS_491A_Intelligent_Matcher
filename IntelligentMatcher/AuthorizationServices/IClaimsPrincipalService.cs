using BusinessModels;
using BusinessModels.UserAccessControl;
using System.Threading.Tasks;

namespace AuthorizationServices
{
    public interface IClaimsPrincipalService
    {
        Task<Result<bool>> AddScope(string scope, int accountId);
        Task<Result<bool>> AddUserClaim(UserClaimModel userClaimModel, string scope, string description);
        Task<Result<bool>> CreateClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
        Task<Result<bool>> DeleteClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
        Task<Result<ClaimsPrincipal>> GetUserClaimsPrincipal(int id, string role);
        Task<Result<bool>> UpdateClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
    }
}