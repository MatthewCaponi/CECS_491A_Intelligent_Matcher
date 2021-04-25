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
using Models.DALListingModels;

namespace TraditionalListings.Managers
{
    public class ListingsManager : IListingsManager
    {
        private IListingCreationService _listingCreationService;
        private IListingDeletionService _listingDeletionService;
        private IListingUpdationService _listUpdationService;


        public ListingsManager(ListingCreationService listingCreationService, ListingDeletionService listingDeletionService,
           ListingUpdationService listingUpdationService)
        {
            _listingCreationService = listingCreationService;
            _listingDeletionService = listingDeletionService;
            _listUpdationService = listingUpdationService;

        }

        public async Task<Result<int>> CreateListing(WebUserProfileModel webUserProfileModel, BusinessListingModel businessListingModels)
        {
            var result = new Result<int>();
            result.Success = false;
            var listingID = await _listingCreationService.CreateListing(businessListingModels);
            businessListingModels.UserAccountId = listingID;
            await _listingCreationService.CreateListing(businessListingModels);
            result.Success = true;
            result.SuccessValue = listingID;
            return result;


        }

        public async Task<Tuple<bool, ResultModel<int>>> DeleteListing(int Id)
        {
            ResultModel<int> resultModel = new ResultModel<int>();
            await _listingDeletionService.DeleteListing(Id);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool, ErrorMessage>> UpdateCollaborationListing(BusinessCollaborationModel businessCollaborationModel)
        {
            await _listUpdationService.UpdateCollaborationListing(businessCollaborationModel);
            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);

        }

        public async Task<Tuple<bool, ErrorMessage>> UpdateParentListing(BusinessListingModel businessListingModel)
        {
            await _listUpdationService.UpdateParentListing(businessListingModel);
            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }

        public async Task<Tuple<bool, ErrorMessage>> UpdateRelationshipListing(BusinessRelationshipModel businessRelationshipModel)
        {
            await _listUpdationService.UpdateRelationshipListing(businessRelationshipModel);
            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }

        public async Task<Tuple<bool, ErrorMessage>> UpdateTeamListing(BusinessTeamModel businessTeamModel)
        {
            await _listUpdationService.UpdateTeamListing(businessTeamModel);
            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }
    }
}
