using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels
{
    public class BusinessLoginTrackerModel
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
