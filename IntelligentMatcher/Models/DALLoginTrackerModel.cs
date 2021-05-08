using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DALLoginTrackerModel
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public DateTime LoginTime { get; set; }
    }   
}
