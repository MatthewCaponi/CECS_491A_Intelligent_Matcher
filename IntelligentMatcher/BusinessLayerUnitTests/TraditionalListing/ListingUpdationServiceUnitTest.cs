//using BusinessModels.ListingModels;
//using DataAccess;
//using DataAccess.Repositories;
//using DataAccess.Repositories.ListingRepositories;
//using DataAccessUnitTestes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Models.DALListingModels;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TraditionalListings.Services;

//namespace BusinessLayerUnitTests.TraditionalListing
//{
//    [TestClass]
//    public class ListingUpdationServiceUnitTest
//    {
//        [TestInitialize()]
//        public async Task Init()
//        {

//            var numTestRows = 20;
//            IDataGateway dataGateway = new SQLServerGateway();
//            IConnectionStringData connectionString = new ConnectionStringData();
//            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
//            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway, connectionString);
//            for (int i = 1; i <= numTestRows; ++i)
//            {
//                DALListingModel dalListingModel = new DALListingModel();
//                dalListingModel.Id = i;
//                dalListingModel.Title = "TestTitle" + i;
//                dalListingModel.Details = "TestDetails" + i;
//                dalListingModel.City = "TestCity" + i;
//                dalListingModel.State = "TestState" + i;
//                dalListingModel.NumberOfParticipants = 5;
//                dalListingModel.InPersonOrRemote = "TestInpersonOrRemote" + i;

//                await listingRepository.CreateListing(dalListingModel);
//            }


//        }

//        [TestCleanup()]
//        public async Task CleanUp()
//        {
//            IDataGateway dataGateway = new SQLServerGateway();
//            IConnectionStringData connectionString = new ConnectionStringData();
//            await DataAccessTestHelper.ReseedAsync("Listing", 0, connectionString, dataGateway);

//        }

//        [DataTestMethod]
//        [DataRow(1, "Test title123", "TestDetails123", "TestCity1", "Teststate1", 10, "InpersonTest1")]
//        public async Task UpdateListing_editParentListing_ParentListingEditSuccesful(int id, string title, string details, string city, string state, int numOfParticipants,
//            string inperson)
//        {
//            IDataGateway dataGateway = new SQLServerGateway();
//            IConnectionStringData connectionString = new ConnectionStringData();
//            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
//            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway, connectionString);
//            IListingUpdationService listingUpdationService = new ListingUpdationService(new ListingRepository(new SQLServerGateway(), new ConnectionStringData()),
//                null, null, null, null);

//            BusinessListingModel businessListingModel = new BusinessListingModel();
//            businessListingModel.Id = id;
//            businessListingModel.Title = title;
//            businessListingModel.Details = details;
//            businessListingModel.City = city;
//            businessListingModel.State = state;
//            businessListingModel.NumberOfParticipants = numOfParticipants;
//            businessListingModel.InPersonOrRemote = inperson;
//            await listingUpdationService.UpdateParentListing(businessListingModel);
//            DALListingModel dALListingModel = await traditionalListingSearchRepository.GetAllListingsById(id);
           

//            if(dALListingModel == null)
//            {
//                Assert.IsTrue(false);
//            }
//            if(dALListingModel.Title == title && dALListingModel.Details == details && dALListingModel.City==city && dALListingModel.State ==state &&
//                dALListingModel.NumberOfParticipants == numOfParticipants && dALListingModel.InPersonOrRemote == inperson)
//            {
//                Assert.IsTrue(true);
//            }
//            else
//            {
//                Assert.IsTrue(false);
//            }


            
           






//        }
//    }
//}
