using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DALListingModels
{
    public class DALRelationshipModel : DALListingModel
    {
        public String RelationshipType { get; set; }
        public int Age { get; set; }
        public String Interests { get; set; }
        public String GenderPreference { get; set; }
        public DateTime CreationDate { get; set; }
        public int ListingId { get; set; }
    }
}
