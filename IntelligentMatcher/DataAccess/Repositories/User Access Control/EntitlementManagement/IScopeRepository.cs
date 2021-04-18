using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IScopeRepository
    {
        Task<int> CreateScope(ScopeModel model);
        Task<int> DeleteScope(int id);
        Task<IEnumerable<ScopeModel>> GetAllScopes();
        Task<ScopeModel> GetScopeById(int id);
        Task<int> UpdateScope(ScopeModel model);
    }
}