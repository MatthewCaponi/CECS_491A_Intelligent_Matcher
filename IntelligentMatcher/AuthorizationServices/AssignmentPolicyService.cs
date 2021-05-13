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
                scopes.Add("user_profile:write");
                scopes.Add("listing:write");
                scopes.Add("messaging.channel:read");
                scopes.Add("messaging.channel:write");
                scopes.Add("messaging.channel.owner:delete");
                scopes.Add("archiving:write");
                scopes.Add("friends_list:read");
                scopes.Add("friends_list:write");
                scopes.Add("friends_list:delete");
            }
            if (accountType == "user")
            {
                scopes.Add("user_profile:read");
                scopes.Add("user_profile.owner:write");
                scopes.Add("listing:read");
                scopes.Add("llisting.owner.write");
                scopes.Add("messaging:read");
                scopes.Add("messaging.channel:read");
                scopes.Add("messaging:write");
                scopes.Add("messaging.channel:write");
                scopes.Add("messaging.channel:delete");
                scopes.Add("friends_list:read");
                scopes.Add("friends_list:write");
                scopes.Add("friends_list:delete");
            }

            return scopes;
        }
    }
}
