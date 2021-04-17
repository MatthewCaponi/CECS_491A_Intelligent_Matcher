using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IAccessPolicyPairingRepository
    {
        Task<int> CreateAccessPolicyPairing(AccessPolicyPairingModel model);
        Task<int> DeleteAccessPairingPolicy(int id);
        Task<AccessPolicyPairingModel> GetAccessPolicyPairingById(int id);
        Task<IEnumerable<AccessPolicyPairingModel>> GetAllAccessPoliciesPairings();
        Task<int> UpdateAccessPolicyPairing(AccessPolicyPairingModel model);
    }
}