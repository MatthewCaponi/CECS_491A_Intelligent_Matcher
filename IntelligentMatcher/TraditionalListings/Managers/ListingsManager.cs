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
    class ListingsManager : IListingsManager
    {
        private ListingCreationService _listingCreationService;
        private ListingDeletionService _listingDeletionService;
        private ListingUpdationService _listUpdationService;
        private ListingGetterService _listingGetterService;


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

        public Task<Tuple<bool, ResultModel<int>>> DeleteListing(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, ResultModel<int>>> EditListing(BusinessListingModel businessListingModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetListing(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateListing(BusinessListingModel businessListingModel)
        {
            throw new NotImplementedException();
        }
    }
}
