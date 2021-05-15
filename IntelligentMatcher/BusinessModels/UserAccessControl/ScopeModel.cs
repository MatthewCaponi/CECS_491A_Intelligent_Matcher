using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class ScopeModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public List<ClaimModel> Claims { get; set; }
    }
}
