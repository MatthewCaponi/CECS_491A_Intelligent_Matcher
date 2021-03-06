﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels
{
    public class EmailModel
    {
        public string Recipient { get; set; }
        public string Sender { get; set; }
        public bool TrackOpens { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
        public string MessageStream { get; set; }
        public string Tag { get; set; }

        public EmailModel()
        {

        }

        public EmailModel(string recipient, string sender, bool trackOpens, string subject, string textBody,
            string htmlBody, string messageStream, string tag)
        {
            Recipient = recipient;
            Sender = sender;
            TrackOpens = trackOpens;
            Subject = subject;
            TextBody = textBody;
            HtmlBody = htmlBody;
            MessageStream = messageStream;
            Tag = tag;
        }
    }
}
