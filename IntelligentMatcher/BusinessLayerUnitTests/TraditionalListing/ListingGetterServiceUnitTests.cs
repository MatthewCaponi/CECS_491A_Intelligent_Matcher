using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.TraditionalListing
{
    [TestClass]
    public class ListingGetterServiceUnitTests
    {
        [TestInitialize()]
        public async Task Init()
        {
            var numTestRows = 20;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ITraditionalListingSearchRepository traditionalListingSearch = new TraditionalListingSearchRepository(dataGateway, connectionString);

            for (int i = 1; i < numTestRows; i++)
            {
                DALListingModel dALListingModel = new DALListingModel();
                dALListingModel.Id = 1;
                dALListingModel.Title = "Test Title";
                dALListingModel.Details = "Test Details";
                dALListingModel.City = "Test city";
                dALListingModel.State = " Test state";
                dALListingModel.NumberOfParticipants = 5;
                dALListingModel.InPersonOrRemote = " Inperson ";
                dALListingModel.UserAccountId = 23;

                await listingRepository.CreateListing(dALListingModel);
            }

        }
    }
}
