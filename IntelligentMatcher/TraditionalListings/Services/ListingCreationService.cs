using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;



namespace TraditionalListings.Services
{
    public class ListingCreationService
    {
        private IListingRepository _listingRepository;

        public ListingCreationService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<bool> CreateListing(UserProfileModel _userprofilemodel, DALListingModel _dalListingmodel)
        {

            int returnValue = await _listingRepository.CreateListing(_userprofilemodel, _dalListingmodel);

            return true;
        }
    }


   



}
