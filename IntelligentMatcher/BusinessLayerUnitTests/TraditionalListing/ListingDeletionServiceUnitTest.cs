using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.TraditionalListing
{
    [TestClass]
    public class ListingDeletionServiceUnitTest
    {
        [TestInitialize()]
        public async Task Init()

        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);

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
