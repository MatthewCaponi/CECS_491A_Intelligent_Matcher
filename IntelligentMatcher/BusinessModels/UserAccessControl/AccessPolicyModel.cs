using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class AccessPolicyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public List<string> Scopes { get; set; }
        public List<UserClaimModel> Claims { get; set; }
        public int Priority { get; set; }
    }
}
