using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IScopeClaimRepository
    {
        Task<int> CreateScopeClaim(ScopeClaimModel model);
        Task<int> DeleteScopeClaim(int id);
        Task<IEnumerable<ScopeClaimModel>> GetAllScopeClaims();
        Task<IEnumerable<ScopeClaimModel>> GetAllScopeClaimsByScopeId(int scopeId);
        Task<ScopeClaimModel> GetScopeClaimById(int id);
        Task<int> UpdateScopeClaim(ScopeClaimModel model);
    }
}