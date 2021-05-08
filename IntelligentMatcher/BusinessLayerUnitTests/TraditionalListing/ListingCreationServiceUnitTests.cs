using BusinessModels.ListingModels;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.DALListingModels;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Services;

namespace BusinessLayerUnitTests.TraditionalListing
{
    [TestClass]
    public class ListingCreationServiceUnitTests
    {
        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
           
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway,connectionString);
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ICollaborationRepository collaborationRepository = new CollaborationRepository(dataGateway, connectionString);
            IRelationshipRepository relationshipRepository = new RelationshipRepository(dataGateway, connectionString);
            ITeamModelRepository teamModelRepository = new TeamModelRepository(dataGateway, connectionString);
            IEnumerable<DALListingModel> models = await traditionalListingSearchRepository.GetAllListings();

            foreach(var model in models)
            {
                await listingRepository.DeleteListing(model.Id);
            }


            IEnumerable<DALListingModel> cmodels = await traditionalListingSearchRepository.GetAllCollaborationListings();
            foreach (var model in cmodels)
            {
                await collaborationRepository.DeleteCollabListing(model.Id);
            }

            //IEnumerable<DALListingModel> rmodels = await traditionalListingSearchRepository.GetAllRelationshipListings();
            //foreach (var model in rmodels)
            //{
            //    await relationshipRepository.DeleteRelationshipListing(model.Id);
            //}


            //IEnumerable<DALListingModel> tmodels = await traditionalListingSearchRepository.GetAllTeamListings();
            //foreach (var model in tmodels)
            //{
            //    await teamModelRepository.DeleteTeamListing(model.Id);
            //}


            await DataAccessTestHelper.ReseedAsync("Listing", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("Collaboration", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("Relationship", 0, connectionString, dataGateway);
        }




        [DataTestMethod]
        [DataRow(1, "TestTitle", "TestDetails", "TestCity", "TestState", 5, "InPersonOrRemote", 123, "CollaborationType", "InvolvementType", "Experience")]

        public async Task CreateListing_ModelIsCollaboration_ReturnsDALListingModel(int expectedID, string title, string detail, string city, string state,
            int numberOfPeople, string inperson, int acccountId, string collaborationType
           , string InvolvementType, string experience)
        {
            //arrange- setup variables 
           
            BusinessCollaborationModel newBusinessCollaborationModel = new BusinessCollaborationModel();
            BusinessListingModel newBusinessListingModel = new BusinessListingModel();
            ListingCreationService listingCreationService = new ListingCreationService(new ListingRepository(new SQLServerGateway(), new ConnectionStringData()),
                new CollaborationRepository(new SQLServerGateway(), new ConnectionStringData()), null, null, null);
            newBusinessCollaborationModel.Id = expectedID;
            newBusinessCollaborationModel.Title = title;
            newBusinessCollaborationModel.Details = detail;
            newBusinessCollaborationModel.City = city;
            newBusinessCollaborationModel.State = state;
            newBusinessCollaborationModel.NumberOfParticipants = numberOfPeople;
            newBusinessCollaborationModel.InPersonOrRemote = inperson;
            newBusinessCollaborationModel.UserAccountId = acccountId;
            newBusinessCollaborationModel.CollaborationType = collaborationType;
            newBusinessCollaborationModel.InvolvementType = InvolvementType;
            newBusinessCollaborationModel.Experience = experience;


            //act - run functions 
            var actualId = await listingCreationService.CreateListing(newBusinessCollaborationModel);

            //assert
            Assert.IsTrue(actualId == expectedID);
        }

        [DataTestMethod]
        [DataRow(2, "TestTitle", "TestDetails", "TestCity", "TestState", 5, "InPersonOrRemote", 124, "RelationshipType", 28, "Interest",
          "GenderPreference")]
        public async Task CreateListing_ModelIsRelationship_ReturnsDALListingModel(int expectedID, string title, string detail, string city, string state,
         int numberOfPeople, string inperson, int acccountId, string relationship, int age, string interest, string gender)
        {
            //arrange- setup variables 
          
            BusinessRelationshipModel newBusinessRelationshipModel = new BusinessRelationshipModel();
            BusinessListingModel newBusinessListingModel = new BusinessListingModel();
            ListingCreationService listingCreationService = new ListingCreationService(new ListingRepository(new SQLServerGateway(), new ConnectionStringData()),
                null, new RelationshipRepository(new SQLServerGateway(), new ConnectionStringData()), null, null);
            newBusinessRelationshipModel.Id = expectedID;
            newBusinessRelationshipModel.Title = title;
            newBusinessRelationshipModel.Details = detail;
            newBusinessRelationshipModel.City = city;
            newBusinessRelationshipModel.State = state;
            newBusinessRelationshipModel.NumberOfParticipants = numberOfPeople;
            newBusinessRelationshipModel.InPersonOrRemote = inperson;
            newBusinessRelationshipModel.UserAccountId = acccountId;
            newBusinessRelationshipModel.RelationshipType = relationship;
            newBusinessRelationshipModel.Age = age;
            newBusinessRelationshipModel.Interests = interest;
            newBusinessRelationshipModel.GenderPreference = gender;


            //act - run functions 
            var actualId = await listingCreationService.CreateListing(newBusinessRelationshipModel);

            //assert
            Assert.IsTrue(actualId == expectedID);
        }

        [DataTestMethod]
        [DataRow(3, "TestTitle", "TestDetails", "TestCity", "TestState", 5, "InPersonOrRemote", 124, "TestTeamType","TestGameType","TestPlatform",
            "TestExperience")]
        public async Task CreateListing_ModelIsTeam_ReturnsDALListingModel(int expectedID, string title, string detail, string city, string state,
        int numberOfPeople, string inperson, int acccountId, string teamType,string gameType,string platform,string experience )
        {
            //arrange- setup variables 
       
            BusinessTeamModel newBusinessTeamModel = new BusinessTeamModel();
            BusinessListingModel newBusinessListingModel = new BusinessListingModel();
            ListingCreationService listingCreationService = new ListingCreationService(new ListingRepository(new SQLServerGateway(), new ConnectionStringData()),
                null, null, new TeamModelRepository(new SQLServerGateway(), new ConnectionStringData()), null);
            newBusinessTeamModel.Id = expectedID;
            newBusinessTeamModel.Title = title;
            newBusinessTeamModel.Details = detail;
            newBusinessTeamModel.City = city;
            newBusinessTeamModel.State = state;
            newBusinessTeamModel.NumberOfParticipants = numberOfPeople;
            newBusinessTeamModel.InPersonOrRemote = inperson;
            newBusinessTeamModel.UserAccountId = acccountId;
            newBusinessTeamModel.TeamType = teamType;
            newBusinessTeamModel.GameType = gameType;
            newBusinessTeamModel.Platform = platform;
            newBusinessTeamModel.Experience = experience;


            //act - run functions 
            var actualId = await listingCreationService.CreateListing(newBusinessTeamModel);

            //assert
            Assert.IsTrue(actualId == expectedID);
        }

      




    }

}
