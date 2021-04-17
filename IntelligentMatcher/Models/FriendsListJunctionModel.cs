using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FriendsListJunctionModel
    {
        public int Id { get; set; }

        public int User1Id { get; set; }

        public int User2Id { get; set; }

        public DateTime Date { get; set; }
    }
}
