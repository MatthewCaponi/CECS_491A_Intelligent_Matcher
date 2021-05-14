using BusinessModels;
using BusinessModels.ListingModels;
using Services.ListingServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ListingSearch
{
    public class ListingSearchManager : IListingSearchManager
    {
        private readonly IListingGetterService _listingGetterService;


        public ListingSearchManager(IListingGetterService listingGetterService)
        {
            _listingGetterService = listingGetterService;
        }

        public async Task<Result<List<BusinessListingModel>>> GetAllListings()
        {

            var result = new Result<List<BusinessListingModel>>();
            result.WasSuccessful = true;
            result.SuccessValue = await _listingGetterService.GetAllListing();
            return result;

        }
        public async Task<Result<List<BusinessCollaborationModel>>> GetAllCollaborationListings()
        {
            var result = new Result<List<BusinessCollaborationModel>>();
            result.WasSuccessful = true;
            result.SuccessValue = await _listingGetterService.GetAllCollaborationListing();
            return result;
        }
        public async Task<Result<List<BusinessRelationshipModel>>> GetAllRelationshipListings()
        {
            var result = new Result<List<BusinessRelationshipModel>>();
            result.WasSuccessful = true;
            result.SuccessValue = await _listingGetterService.GetAllRelationshipListing();
            return result;
        }

        public async Task<Result<List<BusinessTeamModel>>> GetAllTeamListings()
        {
            var result = new Result<List<BusinessTeamModel>>();
            result.WasSuccessful = true;
            result.SuccessValue = await _listingGetterService.GetAllTeamModelListing();
            return result;
        }
    }
}
