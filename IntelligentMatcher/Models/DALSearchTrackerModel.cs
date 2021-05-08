using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class DALSearchTrackerModel
    {
        public string Search { get; set; }
        public DateTime SearchTime { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
