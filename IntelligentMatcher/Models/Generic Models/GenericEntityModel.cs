using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GenericEntityModel
    {
        public int Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset UpdationDate { get; set; }
    }
}
