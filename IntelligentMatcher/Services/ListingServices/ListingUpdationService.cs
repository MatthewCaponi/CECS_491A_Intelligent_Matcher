
using BusinessModels.ListingModels;
using DataAccess;
using DataAccess.Repositories;
using Models.DALListingModels;

using System.Threading.Tasks;

namespace Services.ListingServices
{
    public class ListingUpdationService : IListingUpdationService
    {
        private IListingRepository _listingRepository;

        public ListingUpdationService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<int> UpdateListing(DALListingModel dalListingModel)
        {

            int returnValue = await _listingRepository.UpdateListing(dalListingModel);

            return returnValue;

        }


    }
}
