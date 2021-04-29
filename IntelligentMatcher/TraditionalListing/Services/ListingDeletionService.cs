
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public class ListingDeletionService : IListingDeletionService
    {
        private IListingRepository _listingRepository;

        public ListingDeletionService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<bool> DeleteListing(int id)
        {
            int returnValue = await _listingRepository.DeleteListing(id);

            return true;
        }
    }
}

