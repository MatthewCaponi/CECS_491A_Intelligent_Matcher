using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;
using UserAccessControlServices;

namespace BusinessLayerUnitTests.UserAccessControl
{
    [TestClass]
    public class ResourceServiceIntegrationTests
    {
        private readonly IResourceRepository resourceRepository =
            new ResourceRepository(new SQLServerGateway(), new ConnectionStringData());

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
                Models.User_Access_Control.ResourceModel resourceModel = new Models.User_Access_Control.ResourceModel();
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

        #region Integration Tests
        [TestMethod]
        public async Task GetAllResources_CorrectNumberOfResources()
        {
            // Arrange
            IResourceService resourceService = new ResourceService(resourceRepository);

            // Act
            var resourceList = await resourceService.GetAllResources();

            // Assert
            int i = 1;
            foreach (var resource in resourceList)
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
        [DataRow(1, "TestResource1")]
        public async Task GetResource_ResourceIdExists_ReturnResource(int id, string name)
        {
            // Arrange
            var expectedResult = new BusinessModels.UserAccessControl.ResourceModel();

            expectedResult.Id = id;
            expectedResult.Name = name;

            IResourceService resourceService = new ResourceService(resourceRepository);

            // Act
            var actualResult = await resourceService.GetResource(id);

            // Assert
            Assert.IsTrue(actualResult.Id == expectedResult.Id);
            Assert.IsTrue(actualResult.Name == expectedResult.Name);
        }

        [DataTestMethod]
        [DataRow(21, "TestResource21")]
        public async Task RegisterResource_ResourceIdDoesNotExists_ReturnResourceId(int id, string name)
        {
            // Arrange
            var resource = new BusinessModels.UserAccessControl.ResourceModel();

            resource.Id = id;
            resource.Name = name;

            var expectedResult = id;

            IResourceService resourceService = new ResourceService(resourceRepository);

            // Act
            var actualResult = await resourceService.RegisterResource(resource);

            // Assert
            Assert.IsTrue(actualResult == expectedResult);
        }

        [DataTestMethod]
        [DataRow(21, "TestResource21")]
        public async Task RegisterResource_ResourceIdDoesNotExists_InfoIsAccurate(int id, string name)
        {
            // Arrange
            var resource = new BusinessModels.UserAccessControl.ResourceModel();

            resource.Id = id;
            resource.Name = name;

            IResourceService resourceService = new ResourceService(resourceRepository);

            // Act
            var actualResult = await resourceService.RegisterResource(resource);
            var expectedResult = await resourceService.GetResource(id);

            // Assert
            Assert.IsTrue(expectedResult.Id == id);
            Assert.IsTrue(expectedResult.Name == name);
        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task RemoveResource_ResourceIdExists_ResourceIsNull(int id)
        {
            // Arrange
            IResourceService resourceService = new ResourceService(resourceRepository);

            // Act
            await resourceService.RemoveResource(id);
            var actualResult = await resourceRepository.GetResourceById(id);

            // Assert
            Assert.IsNull(actualResult);
        }
        #endregion
    }
}
