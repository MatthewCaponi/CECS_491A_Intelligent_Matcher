using BusinessModels.ListingModels;
using DataAccess.Repositories;
using Models.DALListingModels;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public class ListingUpdationService : IListingUpdationService
    {
        private IListingRepository _listingRepository;
        private ICollaborationRepository _collaborationRepository;
        private IRelationshipRepository _relationshipRepository;
        private ITeamModelRepository _teamModelRepository;

        public ListingUpdationService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<int> UpdateParentListing(BusinessListingModel businessListingModel)
        {
            var dallistingModel = ModelConverterService.ConvertTo(businessListingModel, new DALListingModel());
            return await _listingRepository.UpdateListing(dallistingModel);

        }

        public async Task<int> UpdateCollaborationListing(BusinessCollaborationModel businesscollaborationModel)
        {
            var dalCollaborationModel = ModelConverterService.ConvertTo(businesscollaborationModel, new DALCollaborationModel());
            return await _collaborationRepository.UpdateListing(dalCollaborationModel);

        }

        public async Task<int> UpdateRelationshipListing(BusinessRelationshipModel businessrelationshipModel)
        {
            var dalRelationshipModel = ModelConverterService.ConvertTo(businessrelationshipModel, new DALRelationshipModel());
            return await _relationshipRepository.UpdateListing(dalRelationshipModel);

        }

        public async Task<int> UpdateTeamListing(BusinessTeamModel businessTeamModel)
        {
            var dalTeamModel = ModelConverterService.ConvertTo(businessTeamModel, new DALTeamModel());
            return await _teamModelRepository.UpdateListing(dalTeamModel);


        }


    }
}
