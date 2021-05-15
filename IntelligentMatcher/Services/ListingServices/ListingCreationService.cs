using DataAccess.Repositories;
using System.Threading.Tasks;
using Models.DALListingModels;
using BusinessModels.ListingModels;
using Services;
using TraditionalListings.Services;

namespace Services.ListingServices
{
    public class ListingCreationService : IListingCreationService
    {
        private IListingRepository _listingRepository;
        private ICollaborationRepository _collaborationRepository;
        private IRelationshipRepository _relationshipRepository;
        private IDatingRepository _datingRepository;
        private ITeamModelRepository _teamModelRepository;

        public ListingCreationService(IListingRepository listingRepository, ICollaborationRepository collaborationRepository, IRelationshipRepository relationshipRepository,
           ITeamModelRepository teamModelRepository)//IDatingRepository datingRepository)
        {
            _listingRepository = listingRepository;
            _collaborationRepository = collaborationRepository;
            _relationshipRepository = relationshipRepository;
           // _datingRepository = datingRepository;
            _teamModelRepository = teamModelRepository;
        }

        public async Task<int> CreateListing(BusinessListingModel businessListingmodel)
        {

            if (businessListingmodel is BusinessCollaborationModel)
            {
                // Step 1: Create businessListingModel
                BusinessCollaborationModel newBusinessCollaborationModel = (BusinessCollaborationModel)businessListingmodel;
                BusinessListingModel newBusinessListingModel = new BusinessListingModel();

                // Step 2: Access parent props of collaboration Model to the step 1 model 
                newBusinessListingModel.Title = newBusinessCollaborationModel.Title;
                newBusinessListingModel.Details = newBusinessCollaborationModel.Details;
                newBusinessListingModel.City = newBusinessCollaborationModel.City;
                newBusinessListingModel.State = newBusinessCollaborationModel.State;
                newBusinessListingModel.NumberOfParticipants = newBusinessCollaborationModel.NumberOfParticipants;
                newBusinessListingModel.InPersonOrRemote = newBusinessCollaborationModel.InPersonOrRemote;
                newBusinessListingModel.UserAccountId = newBusinessCollaborationModel.UserAccountId;


                DALListingModel dALListingModel = new DALListingModel();
                DALCollaborationModel dALCollaborationModel = new DALCollaborationModel();

                // Step 3 : Convert both originalBusinessListingModel and step 1 created model to DALListingModel and DALCollaboration model 
                var dalListingModel = ModelConverterService.ConvertTo(newBusinessListingModel, dALListingModel);
                var dalCollaborationModel = ModelConverterService.ConvertTo(newBusinessCollaborationModel, dALCollaborationModel);

                // Step 4 : Call ListingRepo pass in DalListing Model, call CollaborationRepo pass in CollaborationModel to both create Functions in the repo 
                var result = await _listingRepository.CreateListing(dalListingModel);
                dalCollaborationModel.ListingId = result;

                await _collaborationRepository.CreateListing(dalCollaborationModel); //collaboration repo instead of listing repo 

                return result;
            }



            else if (businessListingmodel is BusinessRelationshipModel)
            {
                // Same steps as is BusinessCollaborationModel 
                // Step 1: Create businessListingModel
                BusinessRelationshipModel newBusinessRelationshipModel = (BusinessRelationshipModel)businessListingmodel;
                BusinessListingModel newBusinessListingModel = new BusinessListingModel();

                // Step 2: Access parent props of collaboration Model to the step 1 model 
                newBusinessListingModel.Title = newBusinessRelationshipModel.Title;
                newBusinessListingModel.Details = newBusinessRelationshipModel.Details;
                newBusinessListingModel.City = newBusinessRelationshipModel.City;
                newBusinessListingModel.State = newBusinessRelationshipModel.State;
                newBusinessListingModel.NumberOfParticipants = newBusinessRelationshipModel.NumberOfParticipants;
                newBusinessListingModel.InPersonOrRemote = newBusinessRelationshipModel.InPersonOrRemote;
                newBusinessListingModel.UserAccountId = newBusinessRelationshipModel.UserAccountId;

                DALListingModel dALListingModel = new DALListingModel();
                DALRelationshipModel dALRelationshipModel = new DALRelationshipModel();

                //Step 3: Convert both originalBusinessListingModel and step 1 created model to DALListingModel and DALCollaboration model 
                var dalListingModel = ModelConverterService.ConvertTo(newBusinessListingModel, dALListingModel);
                var dalRelationshipModel = ModelConverterService.ConvertTo(newBusinessRelationshipModel, dALRelationshipModel);

                // Step 4 : Call ListingRepo pass in DalListing Model, call CollaborationRepo pass in CollaborationModel to both create Functions in the repo 
                var result = await _listingRepository.CreateListing(dalListingModel);
                dalRelationshipModel.ListingId = result;

                await _relationshipRepository.CreateListing(dalRelationshipModel); //collaboration repo instead of listing repo 

                return result;




            }
            else if (businessListingmodel is BusinessDatingModel)
            {
                // Create ListingModel, RelationshipModel, DatingModel 
                // Same steps but instead of 2 models to be created, i need to create 3 
                BusinessRelationshipModel newBusinessRelationshipModel = (BusinessRelationshipModel)businessListingmodel;
                BusinessListingModel newBusinessListingModel = new BusinessListingModel();
                BusinessDatingModel newBusinessDatingModel = (BusinessDatingModel)businessListingmodel;

                newBusinessListingModel.Title = newBusinessRelationshipModel.Title;
                newBusinessListingModel.Details = newBusinessRelationshipModel.Details;
                newBusinessListingModel.City = newBusinessRelationshipModel.City;
                newBusinessListingModel.State = newBusinessRelationshipModel.State;
                newBusinessListingModel.NumberOfParticipants = newBusinessRelationshipModel.NumberOfParticipants;
                newBusinessListingModel.InPersonOrRemote = newBusinessRelationshipModel.InPersonOrRemote;
                newBusinessListingModel.UserAccountId = newBusinessRelationshipModel.UserAccountId;


                DALListingModel dALListingModel = new DALListingModel();
                DALRelationshipModel dALRelationshipModel = new DALRelationshipModel();




            }
            else if (businessListingmodel is BusinessTeamModel)
            {

                BusinessTeamModel newBusinessTeamModel = (BusinessTeamModel)businessListingmodel;
                BusinessListingModel newBusinessListingModel = new BusinessListingModel();
                newBusinessListingModel.Title = newBusinessTeamModel.Title;
                newBusinessListingModel.Details = newBusinessTeamModel.Details;
                newBusinessListingModel.City = newBusinessTeamModel.City;
                newBusinessListingModel.State = newBusinessTeamModel.State;
                newBusinessListingModel.NumberOfParticipants = newBusinessTeamModel.NumberOfParticipants;
                newBusinessListingModel.InPersonOrRemote = newBusinessTeamModel.InPersonOrRemote;
                newBusinessListingModel.UserAccountId = newBusinessTeamModel.UserAccountId;



                DALListingModel dALListingModel = new DALListingModel();
                DALTeamModel dALTeamModel = new DALTeamModel();

                var dalListingModel = ModelConverterService.ConvertTo(newBusinessListingModel, dALListingModel);
                var dalTeamModel = ModelConverterService.ConvertTo(newBusinessTeamModel, dALTeamModel);

                var result = await _listingRepository.CreateListing(dalListingModel);
                dalTeamModel.ListingId = result;

                await _teamModelRepository.CreateListing(dalTeamModel); //collaboration repo instead of listing repo 

                return result;


            }

            return 0;


        }

    }






}
