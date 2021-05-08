using System.Collections.Generic;

namespace UserAccessControlServices
{
    public interface IAssignmentPolicyService
    {
        List<string> ConfigureAssignmentPolicy(string accountType);
    }
}