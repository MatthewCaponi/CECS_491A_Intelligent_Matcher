using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DALListingModels
{
    public class DALCollaborationModel : DALListingModel
    {
        public string CollaborationType { get; set; }
        public string InvolvementType { get; set; }
        public string Experience { get; set; }
        public int ListingId { get; set; }
        
    }
}
