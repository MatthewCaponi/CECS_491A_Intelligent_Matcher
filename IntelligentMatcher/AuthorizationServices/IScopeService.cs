using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServices
{
    public interface IScopeService
    {
        Task<List<BusinessModels.UserAccessControl.ScopeModel>> GetAllScopes();
        Task<List<BusinessModels.UserAccessControl.UserClaimModel>> GetAllUserScopes();
        Task<BusinessModels.UserAccessControl.ScopeModel> GetScope(int id);
        Task<int> CreateScope(BusinessModels.UserAccessControl.ScopeModel scopeModel);
        Task<bool> UpdateScope(BusinessModels.UserAccessControl.ScopeModel scopeModel);
        Task<bool> DeleteScope(int id);
    }
}
