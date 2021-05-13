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
    public class ScopeClaimTests
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
            IClaimRepository claimRepository = new ClaimRepository(dataGateway, connectionString);
            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);
            IScopeClaimRepository scopeClaimRepository = new ScopeClaimRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                ScopeClaimModel scopeClaimModel = new ScopeClaimModel();
                scopeClaimModel.Id = i;
                scopeClaimModel.ScopeId = i;
                scopeClaimModel.ClaimId = i;

                ScopeModel scopeModel = new ScopeModel();
                scopeModel.Id = i;
                scopeModel.Name = "TestScope" + i;
                scopeModel.Description = "TestDescription" + i;
                scopeModel.IsDefault = true;

                ClaimModel claimModel = new ClaimModel();
                claimModel.Id = i;
                claimModel.Type = "TestClaim" + i;
                claimModel.Value = "TestDescription" + i;
                claimModel.IsDefault = true;

                await scopeRepository.CreateScope(scopeModel);
                await claimRepository.CreateClaim(claimModel);
                await scopeClaimRepository.CreateScopeClaim(scopeClaimModel);
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
        [DataRow(1, 2, 2)]
        public async Task UpdateScopeClaim_ScopeClaimsExist_ScopeClaimIdsAccurate(int id, int expectedUpdatedScopeId, int expectedUpdatedClaimId)
        {
            // Arrange
            IScopeClaimRepository scopeClaimRepository = new ScopeClaimRepository(new SQLServerGateway(), new ConnectionStringData());
            ScopeClaimModel scopeClaimModel = new ScopeClaimModel();

            scopeClaimModel.Id = id;
            scopeClaimModel.ScopeId = expectedUpdatedScopeId;
            scopeClaimModel.ClaimId = expectedUpdatedClaimId;

            // Act
            await scopeClaimRepository.UpdateScopeClaim(scopeClaimModel);
            var actual = await scopeClaimRepository.GetScopeClaimById(id);

            var actualScopeId = actual.ScopeId;
            var actualClaimId = actual.ClaimId;
     
            // Assert
            Assert.IsTrue(actualScopeId == expectedUpdatedScopeId &&
                          actualClaimId == expectedUpdatedClaimId);
        }
        #endregion
    }
}
