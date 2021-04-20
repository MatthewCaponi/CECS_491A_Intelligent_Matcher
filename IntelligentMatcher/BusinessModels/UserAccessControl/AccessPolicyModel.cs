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
        public List<ScopeModel> Scopes { get; set; }
        public List<ClaimModel> Claims { get; set; }
        public int Priority { get; set; }
    }
}
