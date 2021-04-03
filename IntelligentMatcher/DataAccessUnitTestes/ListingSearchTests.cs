using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class ListingSearchTests
    {
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IListingRepository listingRepository= new ListingRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                DALListingModel dalListingModel = new DALListingModel();
                dalListingModel.Id = i;
                dalListingModel.Title= "TestTitle" + i;
                dalListingModel.Details = "TestDetails" + i;
                dalListingModel.City = "TestCity" + i;
                dalListingModel.State = "TestState" + i;
                dalListingModel.NumberOfParticipants = 5;
                dalListingModel.InPersonOrRemote = "TestInpersonOrRemote" + i;

                await listingRepository.CreateListing(dalListingModel);
            }
        }
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(new SQLServerGateway(), new ConnectionStringData());
            var accounts = await traditionalListingSearchRepository.GetAllListings();

            // Delete Listing is not cooperating... place it here once its working. 

            await DataAccessTestHelper.ReseedAsync("Listing", 0, connectionString, dataGateway);
        }

        [TestMethod]
        public async Task GetAlllisting_ListingExists_ReturnAllRowsInExistingListings()
        {
            // Arrange
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            IEnumerable<DALListingModel> dAlListings = await traditionalListingSearchRepository.GetAllListings();

            // Assert
            int i = 1;
            foreach (DALListingModel listings in dAlListings)
            {
                if (listings.Id == i)
                {
                    ++i;
                    continue;
                }

                Assert.IsTrue(false);
                return;
            }

            Assert.IsTrue(true);
        }
    }
    

}
