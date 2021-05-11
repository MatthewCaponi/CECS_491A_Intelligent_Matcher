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
            if (accountType == "admin")
            {
                scopes.Add("user_management:write");
                scopes.Add("user_management:delete");
                scopes.Add("user_profile:self:write");
                scopes.Add("listing:write");
                scopes.Add("messaging.channel:read");
                scopes.Add("messaging.channel:write");
                scopes.Add("messaging.channel.owner:delete");
                scopes.Add("archiving:write");
            }
            if (accountType == "user")
            {
                scopes.Add("user_profile:self:write");
                scopes.Add("listing:read");
                scopes.Add("llisting.owner.write");
                scopes.Add("messaging.channel:read");
                scopes.Add("messaging.channel:write");
                scopes.Add("messaging.channel.owner:delete");
            }

            return scopes;
        }
    }
}
