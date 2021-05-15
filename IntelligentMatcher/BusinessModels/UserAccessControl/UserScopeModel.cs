using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class UserScopeModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int UserAccountId { get; set; }
    }
}
