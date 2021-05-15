
using DataAccess.Repositories;

using System.Threading.Tasks;

namespace Services.ListingServices
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

