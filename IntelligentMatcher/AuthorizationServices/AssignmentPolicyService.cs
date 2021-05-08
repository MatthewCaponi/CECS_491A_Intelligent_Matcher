using BusinessModels.UserAccessControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserAccessControlServices
{
    public class AssignmentPolicyService : IAssignmentPolicyService
    {
        public List<string> ConfigureAssignmentPolicy(string accountType)
        {
            var scopes = new List<string>();
            if (accountType == "user")
            {
                scopes.Add("user_profile:any:read");
                scopes.Add("user_profile:self:write");
                scopes.Add("messaging_write");
                scopes.Add("listing.read");
                scopes.Add("listing.self.write");
            }

            if (accountType == "admin")
            {
                scopes.Add("user_management.write");
            }

            return scopes;
        }
    }
}
