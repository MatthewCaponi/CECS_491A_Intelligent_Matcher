using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IUserClaimRepository
    {
        Task<int> CreateUserClaim(UserClaimModel model);
        Task<int> DeleteUserClaimByUserClaimId(int userClaimId);
        Task<IEnumerable<UserClaimModel>> GetAllUserClaims();
        Task<IEnumerable<UserClaimModel>> GetAllUserClaimsByUserAccountId(int userAccountId);
        Task<UserClaimModel> GetUserClaimByUserClaimId(int userClaim);
        Task<int> UpdateUserClaim(UserClaimModel model);
    }
}