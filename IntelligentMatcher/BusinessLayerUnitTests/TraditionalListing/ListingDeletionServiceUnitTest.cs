using BusinessModels.ListingModels;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DALListingModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Services;

namespace BusinessLayerUnitTests.TraditionalListing
{
    [TestClass]
    public class ListingDeletionServiceUnitTest
    {
        [TestInitialize()]
        public async Task Init()
        {

            var numTestRows = 20;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway, connectionString);


            for (int i = 1; i <= numTestRows; ++i)
            {
                DALListingModel dalListingModel = new DALListingModel();
                dalListingModel.Id = i;
                dalListingModel.Title = "TestTitle" + i;
                dalListingModel.Details = "TestDetails" + i;
                dalListingModel.City = "TestCity" + i;
                dalListingModel.State = "TestState" + i;
                dalListingModel.NumberOfParticipants = 5;
                dalListingModel.InPersonOrRemote = "TestInpersonOrRemote" + i;

                await listingRepository.CreateListing(dalListingModel);
            }

             await DataAccessTestHelper.ReseedAsync("Listing", 0, connectionString, dataGateway);
        }



        [DataTestMethod]
        [DataRow(2)]
        public async Task DeleteListing_ListingExists_ReturnAllRows(int id)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway, connectionString);
            ListingDeletionService listingDeletionService = new ListingDeletionService(listingRepository);

            await listingDeletionService.DeleteListing(id);
            IEnumerable<DALListingModel> dalListingModels = await traditionalListingSearchRepository.GetAllListings();

            if (dalListingModels == null)
            {
                Assert.IsTrue(false);
            }
            if (dalListingModels.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            if (dalListingModels.Count() == 19)
            {
                Assert.IsTrue(true);
            }
        }
    }
         
                
            
            

            

            

            
 
}





