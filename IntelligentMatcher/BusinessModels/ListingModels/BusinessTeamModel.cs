using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.ListingModels
{
    class BusinessTeamModel: BusinessListingModel    
    {
        public String TeamType { get; set; }
        public String GameType { get; set; }
        public String Platform { get; set; }
        public String Experience { get; set; }

    }
}
