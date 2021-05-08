using BusinessModels.UserAccessControl;
using System.Collections.Generic;

namespace AuthenticationSystem
{
    public interface IAuthenticationService
    {
        List<string> GetScopes(string token, AssignmentPolicyModel assignmentPolicyModel);
    }
}