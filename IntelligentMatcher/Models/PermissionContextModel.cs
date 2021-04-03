using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PermissionContextModel
    {
        public int Id { get; set; }
        public int UserPermissionId { get; set; }
        public int ContextId { get; set; }
    }
}
