using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserAccountCodeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }
        public int UserAccountId { get; set; }
    }
}
