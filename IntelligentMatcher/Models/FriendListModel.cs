using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FriendListModel
    {
        public int id { get; set; }

        public string username { get; set; }

        public int userId { get; set; }

        public DateTime date { get; set; }

        public string userProfileImage { get; set; }

        public string status { get; set; }
    }
}
