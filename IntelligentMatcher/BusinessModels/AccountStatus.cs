using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Models
{
    public enum AccountStatus
    {
        None,
        Active,
        Inactive,
        Disabled,
        Suspended,
        Banned,
        Deleted
    }
}
