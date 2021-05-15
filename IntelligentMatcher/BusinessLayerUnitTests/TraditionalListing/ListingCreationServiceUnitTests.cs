
using BusinessModels.ListingModels;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services;
using Services.ListingServices;
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
        [TestInitialize()]
        public async Task Init()
        {
            var numTestRows = 20;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel();
                userAccountModel.Id = i;
                userAccountModel.Username = "TestUser" + i;
                userAccountModel.Password = "TestPassword" + i;
                userAccountModel.Salt = "TestSalt" + i;
                userAccountModel.EmailAddress = "TestEmailAddress" + i;
                userAccountModel.AccountType = "TestAccountType" + i;
                userAccountModel.AccountStatus = "TestAccountStatus" + i;
                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

                await userAccountRepository.CreateAccount(userAccountModel);
            }
        }

        //Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();
            // var listings = await listingRepository.GetAllListing();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);

            /*foreach (var list in listings)
            {
                await listingRepository.DeleteListing(list.Id);
            }
            */
            await DataAccessTestHelper.ReseedAsync("Listing", 0, connectionString, dataGateway);
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
                new CollaborationRepository(new SQLServerGateway(), new ConnectionStringData()), null, null);
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

        //[DataTestMethod]
        //[DataRow(1, "RelationshipType", "TestDetails", "TestCity", "TestState", 5, "InPersonOrRemote", 124, "RelationshipType", 28, "Interest",
        //   "GenderPreference")]
        //public async Task CreateListing_ModelIsRelationship_ReturnsDALListingModel(int expectedID, string title, string detail, string city, string state,
        //  int numberOfPeople, string inperson, int acccountId, string relationship, int age, string interest, string gender)
        //{
        //    //arrange- setup variables 
        //    BusinessRelationshipModel newBusinessRelationshipModel = new BusinessRelationshipModel();
        //    BusinessListingModel newBusinessListingModel = new BusinessListingModel();
        //    ListingCreationService listingCreationService = new ListingCreationService(new ListingRepository(new SQLServerGateway(), new ConnectionStringData()),
        //        null, new RelationshipRepository(new SQLServerGateway(), new ConnectionStringData()), null);
        //    newBusinessRelationshipModel.Id = expectedID;
        //    newBusinessRelationshipModel.Title = title;
        //    newBusinessRelationshipModel.Details = detail;
        //    newBusinessRelationshipModel.City = city;
        //    newBusinessRelationshipModel.State = state;
        //    newBusinessRelationshipModel.NumberOfParticipants = numberOfPeople;
        //    newBusinessRelationshipModel.InPersonOrRemote = inperson;
        //    newBusinessRelationshipModel.UserAccountId = acccountId;
        //    newBusinessRelationshipModel.RelationshipType = relationship;
        //    newBusinessRelationshipModel.Age = age;
        //    newBusinessRelationshipModel.Interests = interest;
        //    newBusinessRelationshipModel.GenderPreference = gender;


        //    //act - run functions 
        //    var actualId = await listingCreationService.CreateListing(newBusinessRelationshipModel);

        //    //assert
        //    Assert.IsTrue(actualId == expectedID);
        //}

    }
}
