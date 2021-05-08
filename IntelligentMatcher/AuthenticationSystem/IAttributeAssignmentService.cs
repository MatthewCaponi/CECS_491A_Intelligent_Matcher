using BusinessModels.UserAccessControl;
using System.Collections.Generic;

namespace AuthenticationSystem
{
    public interface IAttributeAssignmentService
    {
        List<string> GetScopes(string token);
    }
}