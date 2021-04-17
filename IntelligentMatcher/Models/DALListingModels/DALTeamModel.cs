using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DALListingModels
{
    public class DALTeamModel : DALListingModel
    {
        public String TeamType { get; set; }
        public String GameType { get; set; }
        public String Platform { get; set; }
        public String Experience { get; set; }
        public int ListingId { get; set; }
    }
}
