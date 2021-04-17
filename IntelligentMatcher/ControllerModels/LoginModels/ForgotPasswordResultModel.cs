using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerModels.LoginModels
{
    public class ForgotPasswordResultModel
    {
        public bool Success { get; set; }
        public int AccountId { get; set; }
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}
