using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserPermissionModel
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int userId { get; set; }
    }
}
