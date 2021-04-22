using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class AssignmentPolicyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Default { get; set; }
        public string RequiredAccountType { get; set; }
        public List<ScopeModel> AssignedScopes { get; set; }
        public int Priority { get; set; }
    }
}
