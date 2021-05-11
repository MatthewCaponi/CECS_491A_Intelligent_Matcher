using ControllerModels;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Models;

namespace Login
{
    public class AuthnResponse
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public WebUserAccountModel UserInfo { get; set; }
    }
}
