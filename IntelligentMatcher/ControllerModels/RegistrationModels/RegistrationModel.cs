using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerModels
{
    public class RegistrationModel
    {
        public string firstName { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string emailAddress { get; set; }
        public string dateOfBirth { get; set; }
        public string ipAddress { get; set; }
    }
}
