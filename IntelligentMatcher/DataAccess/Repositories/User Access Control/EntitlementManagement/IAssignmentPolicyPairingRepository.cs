using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IAssignmentPolicyPairingRepository
    {
        Task<int> CreateAssignmentPolicyPairing(AssignmentPolicyPairingModel model);
        Task<int> DeleteAssignmentPolicyPairing(int id);
        Task<IEnumerable<AssignmentPolicyPairingModel>> GetAllAssignmentPolicyPairings();
        Task<AssignmentPolicyPairingModel> GetAssignmentPolicyPairingById(int id);
        Task<int> UpdateAssignmentPolicyPairing(AssignmentPolicyPairingModel model);
    }
}