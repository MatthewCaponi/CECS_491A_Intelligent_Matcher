using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ScopeModel> Scopes { get; set; }
    }
}
