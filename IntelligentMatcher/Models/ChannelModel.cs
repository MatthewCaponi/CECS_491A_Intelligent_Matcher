using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ChannelModel
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        public string Name { get; set; }
    }
}
