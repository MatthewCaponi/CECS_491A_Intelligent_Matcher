using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DALListingModels
{
    public class DALDatingModel : DALRelationshipModel
    {
        public String LookingFor { get; set; }
        public String SexualOrientationPreference { get; set; }
    }
}
