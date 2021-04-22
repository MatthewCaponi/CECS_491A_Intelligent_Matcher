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

namespace DataAccessUnitTestes.UserAccessControl
{
    [TestClass]
    public class AccessPolicyPairingTests
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
            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);
            IResourceRepository resourceRepository = new ResourceRepository(dataGateway, connectionString);
            IClaimRepository claimRepository = new ClaimRepository(dataGateway, connectionString);
            IAccessPolicyRepository accessPolicyRepository = new AccessPolicyRepository(dataGateway, connectionString);
            IAccessPolicyPairingRepository accessPolicyPairingRepository = new AccessPolicyPairingRepository(dataGateway, connectionString);


            for (int i = 1; i <= numTestRows; ++i)
            {
                ResourceModel resourceModel = new ResourceModel();
                resourceModel.Id = i;
                resourceModel.Name = "TestResource" + i;

                ScopeModel scopeModel = new ScopeModel();
                scopeModel.Id = i;
                scopeModel.Name = "TestScope" + i;
                scopeModel.Description = "TestDescription" + i;

                ClaimModel claimModel = new ClaimModel();
                claimModel.Id = i;
                claimModel.Name = "TestClaim" + i;
                claimModel.Description = "TestDescription" + i;
                claimModel.IsDefault = true;

                var resourceId = await resourceRepository.CreateResource(resourceModel);
                var claimId = await claimRepository.CreateClaim(claimModel);
                var scopeId = await scopeRepository.CreateScope(scopeModel);

                AccessPolicyModel accessPolicyModel = new AccessPolicyModel();
                accessPolicyModel.Id = i;
                accessPolicyModel.Name = "TestAccessPolicy" + i;
                accessPolicyModel.ResourceId = resourceId;
                accessPolicyModel.Priority = i % 4;

                var accessPolicyId = await accessPolicyRepository.CreateAccessPolicy(accessPolicyModel);

                AccessPolicyPairingModel accessPolicyPairingModel = new AccessPolicyPairingModel();
                accessPolicyPairingModel.Id = i;
                accessPolicyPairingModel.ScopeId = scopeId;
                accessPolicyPairingModel.ClaimId = claimId;
                accessPolicyPairingModel.AccessPolicyId = accessPolicyId;

                await accessPolicyPairingRepository.CreateAccessPolicyPairing(accessPolicyPairingModel);
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
        [DataRow(3, 2, 5, 1)]
        public async Task UpdateAccessPolicyPairing_AccessPolicyPairingsExist_AccessPolicyPairingIdAccurate(int id, int expectedScopeId, int expectedClaimId, int expectedAccessPolicyId)
        {
            // Arrange

            IAccessPolicyPairingRepository accessPolicyPairingRepository = new AccessPolicyPairingRepository(new SQLServerGateway(), new ConnectionStringData());

            AccessPolicyPairingModel accessPolicyPairingModel = new AccessPolicyPairingModel();
            accessPolicyPairingModel.Id = id;
            accessPolicyPairingModel.ScopeId = expectedScopeId;
            accessPolicyPairingModel.ClaimId = expectedClaimId;
            accessPolicyPairingModel.AccessPolicyId = expectedAccessPolicyId;

            // Act
            await accessPolicyPairingRepository.UpdateAccessPolicyPairing(accessPolicyPairingModel);
            var actual = await accessPolicyPairingRepository.GetAccessPolicyPairingById(id);

            var actualScopeId = actual.ScopeId;
            var actualClaimId = actual.ClaimId;
            var actualAccessPolicyId = actual.AccessPolicyId;


            // Assert
            Assert.IsTrue(actualScopeId == expectedScopeId &&
                          actualClaimId == expectedClaimId &&
                          actualAccessPolicyId == expectedAccessPolicyId);
        }
        #endregion
    }
}
