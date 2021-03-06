
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

        public async Task<bool> UpdateTitle(int id, string title)
        {
            int returnValue = await _listingRepository.UpdateTitle(id,title);

            return true;
        }

        public async Task<bool> UpdateDetails(int id, string details)
        {
            int returnValue = await _listingRepository.UpdateDetails(id, details);
             
            return true;
        }

    }
}

