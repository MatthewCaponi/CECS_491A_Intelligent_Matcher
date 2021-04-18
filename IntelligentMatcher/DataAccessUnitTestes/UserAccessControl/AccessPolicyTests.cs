using DataAccess;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace DataAccessUnitTestes.UserAccessControl
{
    [TestClass]
    public class AccessPolicyTests
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
            IAccessPolicyRepository accessPolicyRepository = new AccessPolicyRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                ResourceModel resourceModel = new ResourceModel();
                resourceModel.Id = i;
                resourceModel.Name = "TestResource" + i;

                AccessPolicyModel accessPolicyModel = new AccessPolicyModel();
                accessPolicyModel.Id = i;
                accessPolicyModel.Name = "TestAccessPolicy" + i;
                accessPolicyModel.ResourceId = i;
                accessPolicyModel.Priority = i % 4;

                await resourceRepository.CreateResource(resourceModel);
                await accessPolicyRepository.CreateAccessPolicy(accessPolicyModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            await TestCleaner.CleanDatabase();
        }

        #endregion

        #region Functional Tests
        [DataTestMethod]
        [DataRow(3, "TestUpdatedName", 5, 5)]
        public async Task UpdateAccessPolicy_AccessPolicyExists_AccessPolicyDataAccurate(int id, string expectedUpdatedName, int expectedResourceId,
            int expectedPriority)
        {
            // Arrange
            IAccessPolicyRepository accessPolicyRepository = new AccessPolicyRepository(new SQLServerGateway(), new ConnectionStringData());

            AccessPolicyModel accessPolicyModel = new AccessPolicyModel();
            accessPolicyModel.Id = id;
            accessPolicyModel.Name = expectedUpdatedName;
            accessPolicyModel.ResourceId = expectedResourceId;
            accessPolicyModel.Priority = expectedPriority;

            // Act
            await accessPolicyRepository.UpdateAccessPolicy(accessPolicyModel);
            var actual = await accessPolicyRepository.GetAccessPolicyById(id);

            var actualName = actual.Name;
            var actualResourceId = actual.ResourceId;
            var actualPriority = actual.Priority;


            // Assert
            Assert.IsTrue(actualName == expectedUpdatedName &&
                          actualResourceId == expectedResourceId &&
                          actualPriority == expectedPriority);
        }
        #endregion
    }
}
