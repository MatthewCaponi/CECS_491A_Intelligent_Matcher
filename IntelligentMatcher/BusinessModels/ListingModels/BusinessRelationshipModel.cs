using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.ListingModels
{
    class BusinessRelationshipModel : BusinessListingModel

    {
        public String RelationshipType { get; set; }
        public int Age { get; set; }
        public String Interests { get; set; }
        public String GenderPreference { get; set; }

    }
}
