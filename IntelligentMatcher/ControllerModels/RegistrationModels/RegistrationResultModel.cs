using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerModels.RegistrationModels
{
    public class RegistrationResultModel
    {
        public bool Success { get; set; }
        public int AccountId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
