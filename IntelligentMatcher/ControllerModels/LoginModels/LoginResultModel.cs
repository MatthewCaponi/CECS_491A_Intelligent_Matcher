using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerModels.LoginModels
{
    public class LoginResultModel
    {
        public bool Success { get; set; }
        public string Username { get; set; }
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}
