using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DALListingModels
{
    public class DALListingModel
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Details { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public int NumberOfParticipants { get; set; }
        public String InPersonOrRemote { get; set; }
        public UserAccountModel UserAccountId { get; set; }


    }
}
