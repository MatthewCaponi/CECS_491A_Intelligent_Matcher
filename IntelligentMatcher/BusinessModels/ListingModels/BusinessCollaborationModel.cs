using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.ListingModels
{
    public class BusinessCollaborationModel : BusinessListingModel 
    {
        public string CollaborationType { get; set; }
        public string InvolvementType { get; set; }
        public string Experience { get; set; }

    }
}
