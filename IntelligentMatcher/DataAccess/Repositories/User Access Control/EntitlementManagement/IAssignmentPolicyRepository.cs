using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IAssignmentPolicyRepository
    {
        Task<int> CreateAssignmentPolicy(AssignmentPolicyModel model);
        Task<int> DeleteAssignmentPolicy(int id);
        Task<IEnumerable<AssignmentPolicyModel>> GetAllAssignmentPolicies();
        Task<AssignmentPolicyModel> GetAssignmentPolicyById(int id);
        Task<int> UpdateAssignmentPolicy(AssignmentPolicyModel model);
    }
}