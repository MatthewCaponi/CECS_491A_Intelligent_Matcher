using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.ListingModels
{
    class BusinessCollaborationModel : BusinessListingModel 
    {
        public String CollaborationType { get; set; }
        public String InvolvementType { get; set; }
        public String Experience { get; set; }

    }
}
