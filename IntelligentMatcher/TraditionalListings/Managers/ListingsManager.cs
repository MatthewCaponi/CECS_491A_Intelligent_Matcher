using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessModels;
using DataAccess.Repositories;
using TraditionalListings.Services;
using Models;
using UserManagement.Models;
using TraditionalListings.Models;
using Services;

namespace TraditionalListings.Managers
{
    class ListingsManager : IListingsManager
    {
        private ListingCreationService _listingCreationService;
        private ListingDeletionService _listingDeletionService;
        private ListingUpdationService _listUpdationService;
        private ListingGetterService _listingGetterService;


        public ListingsManager (ListingCreationService listingCreationService, ListingDeletionService listingDeletionService,
           ListingUpdationService listingUpdationService)
        {
            _listingCreationService = listingCreationService;
            _listingDeletionService = listingDeletionService;
            _listUpdationService = listingUpdationService;
          
        }



        public async Task<bool> CreateListing(WebUserProfileModel webUserProfileModel, CollaborationModel businessListingModels)
        {
            throw new NotImplementedException();

        }

        public async Task<Tuple<bool,ResultModel<int>>> DeleteListing(int accountId)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            await _listingDeletionService.DeleteListing(accountId);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool,ResultModel<int>>> EditListingTitle(int accountId,string update)
        {
            ResultModel<int> resultModel = new ResultModel<int>();
            await _listUpdationService.UpdateTitle(accountId,update);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);

        }
        
        public async Task<Tuple<bool, ResultModel<int>>> EditListingDetails(int accountId, string update)
        {
            ResultModel<int> resultModel = new ResultModel<int>();
            await _listUpdationService.UpdateDetails(accountId, update);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool,ResultModel<int>>> GetDetails(int id)
        {
            throw new NotImplementedException();
        } 

        public async Task<bool> GetPresets()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTitle(string Title)
        {
            throw new NotImplementedException();
        }

        
    }
}
