using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IAccessPolicyRepository
    {
        Task<int> CreateAccessPolicy(AccessPolicyModel model);
        Task<int> DeleteAccessPolicy(int id);
        Task<AccessPolicyModel> GetAccessPolicyById(int id);
        Task<IEnumerable<AccessPolicyModel>> GetAllAccessPolicies();
        Task<int> UpdateAccessPolicy(AccessPolicyModel model);
    }
}