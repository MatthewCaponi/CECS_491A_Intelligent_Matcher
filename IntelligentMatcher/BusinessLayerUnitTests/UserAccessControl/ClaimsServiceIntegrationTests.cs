using AuthorizationServices;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;
using UserAccessControlServices;

namespace BusinessLayerUnitTests.UserAccessControl
{
    [TestClass]
    public class ClaimsServiceIntegrationTests
    {
        private readonly IClaimRepository claimRepository =
            new ClaimRepository(new SQLServerGateway(), new ConnectionStringData());
        private readonly IScopeRepository scopeRepository =
            new ScopeRepository(new SQLServerGateway(), new ConnectionStringData());
        private readonly IScopeClaimRepository scopeClaimRepository =
            new ScopeClaimRepository(new SQLServerGateway(), new ConnectionStringData());
        private readonly IUserScopeClaimRepository userScopeClaimRepository =
            new UserScopeClaimRepository(new SQLServerGateway(), new ConnectionStringData());

        #region Test Setup
        // Insert test rows before every test case
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);
            IClaimRepository claimRepository = new ClaimRepository(dataGateway, connectionString);
            IScopeClaimRepository scopeClaimRepository = new ScopeClaimRepository(dataGateway, connectionString);
            IUserScopeClaimRepository userScopeClaimRepository = new UserScopeClaimRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel();
                userAccountModel.Id = i;
                userAccountModel.Username = "TestUser" + i;
                userAccountModel.Password = "TestPassword" + i;
                userAccountModel.Salt = "TestSalt" + i;
                userAccountModel.EmailAddress = "TestEmailAddress" + i;
                userAccountModel.AccountType = "TestAccountType" + i;
                userAccountModel.AccountStatus = "TestAccountStatus" + i;
                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

                await userAccountRepository.CreateAccount(userAccountModel);

                Models.User_Access_Control.ScopeModel scopeModel = new Models.User_Access_Control.ScopeModel();
                scopeModel.Id = i;
                scopeModel.Name = "TestScope" + i;
                scopeModel.Description = "TestDescription" + i;
                scopeModel.IsDefault = true;

                await scopeRepository.CreateScope(scopeModel);

                Models.User_Access_Control.ClaimModel claimModel = new Models.User_Access_Control.ClaimModel();
                claimModel.Id = i;
                claimModel.Name = "TestClaim" + i;
                claimModel.Description = "TestDescription" + i;
                claimModel.IsDefault = true;

                await claimRepository.CreateClaim(claimModel);

                Models.User_Access_Control.ScopeClaimModel scopeClaimModel =
                    new Models.User_Access_Control.ScopeClaimModel();
                scopeClaimModel.Id = i;
                scopeClaimModel.ScopeId = i;
                scopeClaimModel.ClaimId = i;

                await scopeClaimRepository.CreateScopeClaim(scopeClaimModel);

                Models.User_Access_Control.UserScopeClaimModel userScopeClaimModel =
                    new Models.User_Access_Control.UserScopeClaimModel();
                userScopeClaimModel.Id = i;
                userScopeClaimModel.UserAccountId = i;
                userScopeClaimModel.ScopeClaimId = i;

                await userScopeClaimRepository.CreateUserScopeClaim(userScopeClaimModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            await TestCleaner.CleanDatabase();
        }
        #endregion

        #region Integration Tests
        [DataTestMethod]
        [DataRow(1)]
        public async Task DeleteClaim_ClaimIdExists_ClaimIsNull(int id)
        {
            // Arrange
            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            await claimsService.DeleteClaim(id);
            var actualResult = await claimRepository.GetClaimById(id);

            // Assert
            Assert.IsNull(actualResult);
        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task DeleteClaim_ClaimIdExists_ReturnTrue(int id)
        {
            // Arrange
            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var actualResult = await claimsService.DeleteClaim(id);

            // Assert
            Assert.IsTrue(actualResult);
        }
        #endregion
    }
}
