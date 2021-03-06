using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DALListingModels
{
    public class DALCollaborationModel : DALListingModel
    {
        public String CollaborationType { get; set; }
        public String InvolvementType { get; set; }
        public String Experience { get; set; }
    }
}
