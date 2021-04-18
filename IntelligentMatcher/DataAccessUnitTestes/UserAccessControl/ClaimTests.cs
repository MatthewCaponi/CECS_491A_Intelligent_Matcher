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
    public class ClaimTests
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
                ClaimModel claimModel = new ClaimModel();
                claimModel.Id = i;
                claimModel.Name = "TestClaim" + i;
                claimModel.Description = "TestDescription" + i;
                claimModel.IsDefault = true;

                await claimRepository.CreateClaim(claimModel);
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
        [DataRow(1, "UpdatedClaimName", "UpdatedDescriptionName", false)]
        public async Task UpdateClaim_ClaimExists_ClaimNameIsAccurate(int id, string expectedUpdatedName, string expectedDescriptionName, bool expectedDefaultValue)
        {
            // Arrange
            IClaimRepository claimRepository = new ClaimRepository(new SQLServerGateway(), new ConnectionStringData());
            ClaimModel claimModel = new ClaimModel();
            claimModel.Id = id;
            claimModel.Name = expectedUpdatedName;
            claimModel.Description = expectedDescriptionName;
            claimModel.IsDefault = expectedDefaultValue;

            // Act
            await claimRepository.UpdateClaim(claimModel);
            var actual = await claimRepository.GetClaimById(id);

            var actualName = actual.Name;
            var actualDescriptionName = actual.Description;
            var actualDefaultValue = actual.IsDefault;


            // Assert
            Assert.IsTrue(actualName == expectedUpdatedName &&
                          actualDescriptionName == expectedDescriptionName &&
                          actualDefaultValue == expectedDefaultValue);
        }
        #endregion
    }
}
