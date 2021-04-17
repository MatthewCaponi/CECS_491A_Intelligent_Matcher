using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class ResourceTests
    {
        #region Test Setup
        // Insert test rows before every test case
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IResourceRepository resourceRepository = new ResourceRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                ResourceModel resourceModel = new ResourceModel();
                resourceModel.Id = i;
                resourceModel.Name = "TestResource" + i;

                await resourceRepository.CreateResource(resourceModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            await TestCleaner.CleanDatabase();
        }

        #endregion
        #region Functional Tests
        [TestMethod]
        public async Task GetAllResources_ResourcesExist_ReturnsCorrectNumberRows()
        {
            // Arrange
            IResourceRepository resourceRepository = new ResourceRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var resources = await resourceRepository.GetAllResources();

            // Assert
            int i = 1;
            foreach (var resource in resources)
            {
                if (resource.Id == i)
                {
                    ++i;
                    continue;
                }

                Assert.IsTrue(false);
                return;
            }

            Assert.IsTrue(true);
        }

        [DataTestMethod]
        [DataRow(21, "TestResource21")]
        public async Task CreateResource_ResourceExists_ResourceNameCorrect(int expectedId, string expectedName)
        {
            //Arrange
            ResourceModel resourceModel = new ResourceModel();
            resourceModel.Name = expectedName;

            IResourceRepository resourceRepository = new ResourceRepository(new SQLServerGateway(), new ConnectionStringData());

            //Act
            var id = await resourceRepository.CreateResource(resourceModel);
            var resource = await resourceRepository.GetResourceById(id);
            var actualName = resource.Name;

            //Assert
            Assert.IsTrue(actualName == expectedName);
        }

        [DataTestMethod]
        [DataRow(1, "UpdatedResourceName")]
        public async Task UpdateResource_ResourceExists_NameChangeIsAccurate(int id, string expectedUpdatedName)
        {
            // Arrange
            IResourceRepository resourceRepository = new ResourceRepository(new SQLServerGateway(), new ConnectionStringData());
            ResourceModel resourceModel = new ResourceModel();
            resourceModel.Id = id;
            resourceModel.Name = expectedUpdatedName;

            // Act
            await resourceRepository.UpdateResource(resourceModel);
            var model = await resourceRepository.GetResourceById(id);
            var actualUpdatedName = model.Name;
            

            // Assert
            Assert.IsTrue(actualUpdatedName == expectedUpdatedName);
        }
        #endregion
    }

 
}
