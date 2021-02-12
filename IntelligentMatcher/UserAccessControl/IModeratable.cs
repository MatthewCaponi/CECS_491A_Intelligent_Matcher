using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Models;

namespace UserAccessControl
{
    //a
    interface IModeratable
    {
        bool CheckUserPermissions(UserInfoModel model);
        bool SetComponentsAccess(UserInfoModel model);
    }
}

