using BusinessModels.ListingModels;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public interface IListingUpdationService
    {
       Task<int> UpdateParentListing(BusinessListingModel businessListingMode);
       Task<int> UpdateCollaborationListing(BusinessCollaborationModel businesscollaborationModel);
       Task<int> UpdateRelationshipListing(BusinessRelationshipModel businessrelationshipModel);
       Task<int> UpdateTeamListing(BusinessTeamModel businessTeamModel);
     
    }
}
