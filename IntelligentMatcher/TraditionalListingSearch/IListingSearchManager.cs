using BusinessModels;
using BusinessModels.ListingModels;
using BusinessLayer.CrossCuttingConcerns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TraditionalListingSearch
{
    public interface IListingSearchManager
    {
        Task<Result<List<BusinessListingModel>>> GetAllListings();
        Task<Result<List<BusinessCollaborationModel>>> GetAllCollaborationListings();
        Task<Result<List<BusinessRelationshipModel>>> GetAllRelationshipListings();
        Task<Result<List<BusinessTeamModel>>> GetAllTeamListings();



    }
}
