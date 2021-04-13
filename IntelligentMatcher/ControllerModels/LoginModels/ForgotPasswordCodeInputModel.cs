using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerModels
{
    public class ForgotPasswordCodeInputModel
    {
        public string code { get; set; }
        public int accountId { get; set; }
    }
}
