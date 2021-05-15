using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User_Access_Control
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsDefault { get; set; }
    }
}
