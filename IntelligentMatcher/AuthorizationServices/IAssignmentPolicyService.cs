using BusinessModels;
using BusinessModels.UserAccessControl;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAccessControlServices
{
    public interface IAssignmentPolicyService
    {
        Task<Result<int>> CreateAssignmentPolicy(AssignmentPolicyModel assignmentPolicyModel);
        Task<Result<bool>> DeleteAssignmentPolicyModel(int id);
        Task<Result<AssignmentPolicyModel>> GetAssignmentPolicyByRole(string role, int priority);
    }
}