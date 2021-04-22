using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IClaimRepository
    {
        Task<int> CreateClaim(ClaimModel model);
        Task<int> DeleteClaim(int id);
        Task<IEnumerable<ClaimModel>> GetAllClaims();
        Task<ClaimModel> GetClaimById(int id);
        Task<int> UpdateClaim(ClaimModel model);
    }
}