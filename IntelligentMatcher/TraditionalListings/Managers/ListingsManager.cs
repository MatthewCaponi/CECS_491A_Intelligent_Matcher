using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessModels;
using DataAccess.Repositories;
using TraditionalListings.Services;
using Models;
using UserManagement.Models;

using Services;
using BusinessModels.ListingModels;

namespace TraditionalListings.Managers
{
    public class ListingsManager : IListingsManager
    {
        private ListingCreationService _listingCreationService;
        private ListingDeletionService _listingDeletionService;
        private ListingUpdationService _listUpdationService;


        public ListingsManager(ListingCreationService listingCreationService, ListingDeletionService listingDeletionService,
           ListingUpdationService listingUpdationService)
        {
            _listingCreationService = listingCreationService;
            _listingDeletionService = listingDeletionService;
            _listUpdationService = listingUpdationService;

        }

        public Task<bool> CreateListing(WebUserProfileModel webUserProfileModel, BusinessListingModel businessListingModels)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<bool, ResultModel<int>>> DeleteListing(int Id)
        {
            ResultModel<int> resultModel = new ResultModel<int>();
            await _listingDeletionService.DeleteListing(Id);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public Task<Tuple<bool, ResultModel<int>>> EditListing(BusinessListingModel businessListingModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetListing(int Id)
        {
            throw new NotImplementedException();
        }


        //public async Task<Tuple<bool, ResultModel<int>>> EditListing(BusinessListingModel businessListingModel)
        //{
        //    ResultModel<int> resultModel = new ResultModel<int>();

        //    await _listUpdationService.UpdateListing(businessListingModel);

        //    return new Tuple<bool, ResultModel<int>>(true, resultModel);
        //}

        /*public async Task<bool> GetListing(int Id)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            resultModel.Result=await _listingGetterService.GetListing(Id);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }
        */

        public Task<bool> UpdateListing(BusinessListingModel businessListingModel)
        {
            throw new NotImplementedException();
        }
    }
}
