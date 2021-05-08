using BusinessModels;
using BusinessModels.ListingModels;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Services;

namespace TraditionalListingSearch
{
    public class ListingSearchManager : IListingSearchManager
    {
        private IListingGetterService _listingGetterService;


        public ListingSearchManager(ListingGetterService listingGetterService)
        {
            _listingGetterService = listingGetterService;
        }

        public async Task<Result<List<BusinessListingModel>>> GetAllListings()
        {

            var result = new Result<List<BusinessListingModel>>();
            result.Success = true;
            result.SuccessValue = await _listingGetterService.GetAllListing();
            return result;

        }
        public async Task<Result<List<BusinessCollaborationModel>>> GetAllCollaborationListings()
        {
            var result = new Result<List<BusinessCollaborationModel>>();
            result.Success = true;
            result.SuccessValue = await _listingGetterService.GetAllCollaborationListing();
            return result;
        }
        public async Task<Result<List<BusinessRelationshipModel>>> GetAllRelationshipListings()
        {
            var result = new Result<List<BusinessRelationshipModel>>();
            result.Success = true;
            result.SuccessValue = await _listingGetterService.GetAllRelationshipListing();
            return result;
        }

        public async Task<Result<List<BusinessTeamModel>>> GetAllTeamListings()
        {
            var result = new Result<List<BusinessTeamModel>>();
            result.Success = true;
            result.SuccessValue = await _listingGetterService.GetAllTeamModelListing();
            return result;
        }


        public async Task<Result<List<BusinessDatingModel>>> GetAllDatingListings()
        {
            var result = new Result<List<BusinessDatingModel>>();
            result.Success = true;
            result.SuccessValue = await _listingGetterService.GetAllDatingListing();
            return result;
        }

        public async Task<Result<List<BusinessListingModel>>> GetAllListingById(int id)
        {
            var result = new Result<List<BusinessListingModel>>();
            result.Success = true;
            result.SuccessValue = await _listingGetterService.GetAllListingById();
            return result;
        }
    }
}
