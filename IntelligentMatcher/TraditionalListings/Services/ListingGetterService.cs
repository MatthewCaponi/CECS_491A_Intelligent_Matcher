using BusinessModels.ListingModels;
using DataAccess.Repositories;
using Models;
using Models.DALListingModels;
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
        private  ICollaborationRepository _collaborationRepository;
        private IDatingRepository _datingRepository;
        private IRelationshipRepository _relationshipRepository;
        private  ITeamModelRepository _teamModelRepository;

        public ListingGetterService(IListingRepository listingRepository, ICollaborationRepository collaborationRepository, IDatingRepository datingRepository, IRelationshipRepository relationshipRepository,
            ITeamModelRepository teamModelRepository)
        {
            _collaborationRepository = collaborationRepository;
            _datingRepository = datingRepository;
            _relationshipRepository = relationshipRepository;
            _teamModelRepository = teamModelRepository;
            _listingRepository = listingRepository;
        }

        
   


        // JUST ONE GETTER FUNCTION FOR THE ENTIRE LISTING 
        public async Task<BusinessListingModel> GetListing(int id, Type listingType)
        {
           if(listingType == typeof(BusinessCollaborationModel))
            {
                DALCollaborationModel daLCollaborationModel = new DALCollaborationModel();
                DALListingModel dalListingModel = new DALListingModel();

                daLCollaborationModel = await _collaborationRepository.GetListing(id);
                dalListingModel = await _collaborationRepository.GetListing(id);

                var businessCollaborationModel = ModelConverterService.ConvertTo(daLCollaborationModel, new BusinessCollaborationModel());
                var businessListingModel = ModelConverterService.ConvertTo(dalListingModel, new BusinessListingModel());

                businessCollaborationModel.Id = businessListingModel.Id;
                businessCollaborationModel.Title = businessListingModel.Title;
                businessCollaborationModel.Details = businessListingModel.Details;
                businessCollaborationModel.City = businessListingModel.City;
                businessCollaborationModel.State = businessListingModel.State;
                businessCollaborationModel.NumberOfParticipants = businessListingModel.NumberOfParticipants;
                businessCollaborationModel.InPersonOrRemote = businessListingModel.InPersonOrRemote;
                businessCollaborationModel.UserAccountId = businessListingModel.UserAccountId;

                return businessCollaborationModel;

            }
           else if(listingType == typeof(BusinessRelationshipModel))
            {

            }
           else if(listingType == typeof(BusinessTeamModel))
            {

            }
           else if(listingType == typeof(BusinessDatingModel){

            }
        }




    }
}
