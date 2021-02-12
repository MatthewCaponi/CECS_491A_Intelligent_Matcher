using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Models;

namespace UserAccessControl
{
    interface IModeratable
    {
        bool CheckUserPermissions(UserInfoModel model);
        bool SetComponentsAccess(UserInfoModel model);
    }
}

