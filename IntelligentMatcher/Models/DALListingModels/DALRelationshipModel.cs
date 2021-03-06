using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DALListingModels
{
    public class DALRelationshipModel : DALListingModels
    {
        public String RelationshipType { get; set; }
        public int Age { get; set; }
        public String Interests { get; set; }
        public String GenderPreference { get; set; }
    }
}
