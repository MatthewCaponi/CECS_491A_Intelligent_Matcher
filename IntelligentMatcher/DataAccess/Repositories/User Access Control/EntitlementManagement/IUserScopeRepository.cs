using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IUserScopeRepository
    {
        Task<int> CreateScope(UserScopeModel model);
        Task<int> DeleteUserScopeByUserScopeId(int userAccountId);
        Task<IEnumerable<UserScopeModel>> GetAllUserScopes();
        Task<IEnumerable<UserScopeModel>> GetAllUserScopesByUserAccountId(int userAccountId);
        Task<UserScopeModel> GetUserScopeByScopeId(int scopeId);
        Task<int> UpdateScope(UserScopeModel model);
    }
}