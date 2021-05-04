using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserReportsModel
    {
        public int Id { get; set; }

        public string Report { get; set; }
        public int ReportingId { get; set; }

        public int ReportedId { get; set; }

        public DateTime Date { get; set; }


    }
}
