using DataAccess.Repositories;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Models;

namespace TraditionalListings.Services
{
    class ListingGetterService
    {

        private IListingRepository _listingRepository;

        public ListingGetterService (IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<CollaborationModel> GetListing(int id)
        {
            var listingModel = await _listingRepository.GetListing(id);
            var businessListingModel = ModelConverterService.ConvertTo(listingModel, new CollaborationModel());

            return businessListingModel;
        }

        public async Task<CollaborationModel> GetDetails(int id)
        {
            var listingModel = await _listingRepository.GetDetails(id);
            var businessListingModel = ModelConverterService.ConvertTo(listingModel, new CollaborationModel());

            return businessListingModel;
        }

        public async Task<CollaborationModel> GetTitle(int id)
        {
            var listingModel = await _listingRepository.GetTitle(id);
            var businessListingModel = ModelConverterService.ConvertTo(listingModel, new CollaborationModel());

            return businessListingModel;
        }



    }
}
