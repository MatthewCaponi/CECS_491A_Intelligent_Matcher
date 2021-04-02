using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LoginAttemptsModel
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public int LoginCounter { get; set; }
        public DateTimeOffset SuspensionEndTime { get; set; }
    }
}
