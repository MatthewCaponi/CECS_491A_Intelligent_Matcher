﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.ListingModels
{
    public class BusinessCollaborationModel : BusinessListingModel 
    {
        public String CollaborationType { get; set; }
        public String InvolvementType { get; set; }
        public String Experience { get; set; }

    }
}
