using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.DALListingModels;
using BusinessModels.ListingModels;
using Services;

namespace TraditionalListings.Services
{
    public class ListingCreationService
    {
        private IListingRepository _listingRepository;
        private ICollaborationRepository _collaborationRepository;
        private IRelationshipRepository _relationshipRepository;
        private IDatingRepository _datingRepository;
        private ITeamModelRepository _teamModelRepository;

        public ListingCreationService(IListingRepository listingRepository,ICollaborationRepository collaborationRepository,IRelationshipRepository relationshipRepository,
           ITeamModelRepository teamModelRepository, IDatingRepository datingRepository )
        {
            _listingRepository = listingRepository;
            _collaborationRepository = collaborationRepository;
            _relationshipRepository = relationshipRepository;
            _datingRepository = datingRepository;
            _teamModelRepository = teamModelRepository;
        }

        public async Task<int> CreateListing(BusinessListingModel businessListingmodel)
        {
          
           if(businessListingmodel is BusinessCollaborationModel)
            {
                //Step 1
                BusinessCollaborationModel newBusinessCollaborationModel = (BusinessCollaborationModel)businessListingmodel; 
                BusinessListingModel newBusinessListingModel = new BusinessListingModel();

                //Step 2
                /*newBusinessListingModel.Id = newBusinessCollaborationModel.Id;
                newBusinessListingModel.Title = newBusinessCollaborationModel.Title;
                newBusinessListingModel.Details = newBusinessCollaborationModel.Details;
                newBusinessListingModel.City= newBusinessCollaborationModel.City;
                newBusinessListingModel.State = newBusinessCollaborationModel.State;
                newBusinessListingModel.NumberOfParticipants = newBusinessCollaborationModel.NumberOfParticipants;
                newBusinessListingModel.InPersonOrRemote = newBusinessCollaborationModel.InPersonOrRemote;
                newBusinessListingModel.UserAccountId= newBusinessCollaborationModel.UserAccountId;
                */
                ModelConverterService.ConvertTo(newBusinessCollaborationModel, newBusinessListingModel);

                //Step 3
                var dalListingModel = ModelConverterService.ConvertTo(newBusinessListingModel, new DALListingModel());
                var dalCollaborationModel = ModelConverterService.ConvertTo(newBusinessCollaborationModel, new DALListingModel());

                //Step4
               var result = await _listingRepository.CreateListing(dalListingModel);
              
               await _collaborationRepository.CreateListing(dalCollaborationModel); //collaboration repo instead of listing repo 

                return result;
    
                //Step 1: create businessListingModel
                //Step 2: access parent props of collaboration Model to the step 1 model 
                //Step 3: convert both originalBusinessListingModel and step 1 created model to DALListingModel and DALCollaboration model 
                //Step 4: Call ListingRepo pass in DalListing Model, call CollaborationRepo pass in CollaborationModel to both create Functions in the repo 
            }
            else if(businessListingmodel is BusinessRelationshipModel)
            {
                //ditto 
            }
            else if(businessListingmodel is BusinessDatingModel)
            {
                //Create ListingModel, RelationshipModel, DatingModel 
                //
                
            }
            else if(businessListingmodel is BusinessTeamModel)
            {
                //ditto 
            }

            return 0;
          
        }
    }


   



}
