using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class EmailOptionsModel
    {

        public string Sender { get; set; }

        public bool TrackOpens { get; set; }

        public string Subject { get; set; }

        public string TextBody { get; set; }

        public string MessageStream { get; set; }

        public string Tag { get; set; }

        public string HtmlBody { get; set; }

    }
}
