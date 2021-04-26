using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Security;
using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;
using Services;
using TraditionalListings;
using TraditionalListings.Services;
using TraditionalListings.Managers;
using Moq;
using DataAccess.Repositories.ListingRepositories;
using TestHelper;
using BusinessModels.ListingModels;
namespace BusinessLayerUnitTests.TraditionalListing
{

    [TestClass]
    public class ListingManangerIntegrationTests
    {

        [TestInitialize()]
        [DataTestMethod]
        public async Task test()
        {


            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();

            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ICollaborationRepository collaborationRepository = new CollaborationRepository(dataGateway, connectionString);
            IRelationshipRepository relationshipRepository = new RelationshipRepository(dataGateway, connectionString);
            IDatingRepository datingRepository = new DatingRepository(dataGateway, connectionString) ;
            ITeamModelRepository teamModelRepository = new TeamModelRepository(dataGateway, connectionString);

            IListingCreationService listingCreationService = new ListingCreationService(listingRepository, collaborationRepository, relationshipRepository, teamModelRepository, datingRepository);

            BusinessListingModel model = new BusinessListingModel();
            model.Title = "Test";
            
            await listingCreationService.CreateListing(model);
            Assert.IsTrue(true);

        }

    }
}
