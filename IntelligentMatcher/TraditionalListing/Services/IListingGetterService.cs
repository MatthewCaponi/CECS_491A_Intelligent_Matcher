using BusinessModels.ListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public interface IListingGetterService
    {
        Task<List<BusinessListingModel>> GetAllListing();
        Task<List<BusinessListingModel>> GetAllListingById();
        Task<List<BusinessCollaborationModel>> GetAllCollaborationListing();
        Task<List<BusinessRelationshipModel>> GetAllRelationshipListing();
        Task<List<BusinessTeamModel>> GetAllTeamModelListing();
        Task<List<BusinessDatingModel>> GetAllDatingListing();

    }
}
