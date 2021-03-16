using System;
using System.Collections.Generic;
using System.Text;

namespace Messaging
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public int ChannelMessageId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
      
        public DateTimeOffset Time { get; set; }

    }
}
