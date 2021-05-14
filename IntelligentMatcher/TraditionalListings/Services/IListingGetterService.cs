

using BusinessModels.ListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings
{
    public interface IListingGetterService
    {
        Task<List<BusinessListingModel>> GetAllListing();
        Task<List<BusinessCollaborationModel>> GetAllCollaborationListing();
        Task<List<BusinessRelationshipModel>> GetAllRelationshipListing();
        Task<List<BusinessTeamModel>> GetAllTeamModelListing();

    }
}
