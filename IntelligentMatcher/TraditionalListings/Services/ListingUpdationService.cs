
using BusinessModels.ListingModels;
using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public class ListingUpdationService
    {
        private IListingRepository _listingRepository;

        public ListingUpdationService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<bool> UpdateListing(BusinessListingModel businessListingModel)
        {
            int returnValue = await _listingRepository.UpdateTitle(id,title);

            return true;
        }

        
    }
}

