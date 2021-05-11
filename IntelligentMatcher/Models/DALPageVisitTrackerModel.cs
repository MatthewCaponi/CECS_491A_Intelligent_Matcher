using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DALPageVisitTrackerModel
    {
        public string PageVisitedName { get; set; }
        public DateTime PageVisitTime { get; set; }
        public int UserId { get; set; }

    }
}
