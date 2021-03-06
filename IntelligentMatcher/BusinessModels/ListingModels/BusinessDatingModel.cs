using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.ListingModels
{
    class BusinessDatingModel :BusinessRelationshipModel
    {
        public String LookingFor { get; set; }
        public String SexualOrientationPreference { get; set; }
    }
}
