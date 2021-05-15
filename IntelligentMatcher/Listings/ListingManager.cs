
using System;
using System.Threading.Tasks;
using BusinessModels;
using UserManagement.Models;
using BusinessModels.ListingModels;
using UserManagement.Services;

using Services.ListingServices;

namespace Listings
{
    public class ListingManager : IListingManager
    {
        private IListingCreationService _listingCreationService;
        private IListingDeletionService _listingDeletionService;
        private IListingUpdationService _listUpdationService;
        private IUserProfileService _userProfileService;



        public ListingManager(IListingCreationService listingCreationService, IListingDeletionService listingDeletionService,
           IListingUpdationService listingUpdationService)
        {
            _listingCreationService = listingCreationService;
            _listingDeletionService = listingDeletionService;
            _listUpdationService = listingUpdationService;

        }

        public async Task<Result<int>> CreateListing(BusinessListingModel businessListingModels)
        {
            var result = new Result<int>();
            result.WasSuccessful = false;
           
            await _listingCreationService.CreateListing(businessListingModels);
            result.WasSuccessful = true;
          
            return result;


        }

        public async Task<Tuple<bool, ResultModel<int>>> DeleteListing(int Id)
        {
            ResultModel<int> resultModel = new ResultModel<int>();
            await _listingDeletionService.DeleteListing(Id);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public Task<Tuple<bool, ResultModel<int>>> EditListing(BusinessListingModel businessListingModel)
        {
            //var listingModel = ModelConverterService.ConvertTo(dalListingModel, new DALListingModel());
            //return await _listUpdationService.UpdateListing(businessListingModel);
            throw new NotImplementedException();

        }

        public Task<bool> GetListing(int Id)
        {
            throw new NotImplementedException();
        }


        //public async Task<Tuple<bool, ResultModel<int>>> EditListing(BusinessListingModel businessListingModel)
        //{
        //    ResultModel<int> resultModel = new ResultModel<int>();

        //  await _listUpdationService.UpdateListing(businessListingModel);

        //    return new Tuple<bool, ResultModel<int>>(true, resultModel);
        //}

        /*public async Task<bool> GetListing(int Id)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            resultModel.Result=await _listingGetterService.GetListing(Id);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }
        */

        public async Task<bool> UpdateListing(BusinessListingModel businessListingModel)
        {
            //await _listUpdationService.UpdateListing(businessListingModel);

            //return businessListingModel;
          throw new NotImplementedException();
        }
    }
}
