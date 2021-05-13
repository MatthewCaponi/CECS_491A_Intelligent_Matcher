using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace DataAccessUnitTestes.UserAccessControl
{
    [TestClass]
    public class UserScopeClaimTests
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

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);
            IClaimRepository claimRepository = new ClaimRepository(dataGateway, connectionString);
            IScopeClaimRepository scopeClaimRepository = new ScopeClaimRepository(dataGateway, connectionString);
            IUserScopeClaimRepository userScopeClaimRepository = new UserScopeClaimRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {

                UserScopeClaimModel userScopeClaimModel = new UserScopeClaimModel();
                userScopeClaimModel.Id = i;
                userScopeClaimModel.UserAccountId = i;
                userScopeClaimModel.ScopeClaimId = i;

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
                await scopeRepository.CreateScope(scopeModel);
                await claimRepository.CreateClaim(claimModel);
                await scopeClaimRepository.CreateScopeClaim(scopeClaimModel);
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

        #region Functional Tests
        [DataTestMethod]
        [DataRow(7, 6, 3)]
        public async Task UpdateUserScopeClaims_UserScopeClaimsExist_UserScopeClaimsReturnsAccurateData(int id, int expectedUserAccountId, int expectedScopeClaimId)
        {
            // Arrange

            IUserScopeClaimRepository userScopeClaimRepository = new UserScopeClaimRepository(new SQLServerGateway(), new ConnectionStringData());

            UserScopeClaimModel userScopeClaimModel = new UserScopeClaimModel();
            userScopeClaimModel.Id = id;
            userScopeClaimModel.UserAccountId = expectedUserAccountId;
            userScopeClaimModel.ScopeClaimId = expectedScopeClaimId;

            // Act
            await userScopeClaimRepository.UpdateUserScopeClaim(userScopeClaimModel);
            var actual = await userScopeClaimRepository.GetUserScopeClaimById(id);

            var actualUserAccountId = actual.UserAccountId;
            var actualScopeClaimId = actual.ScopeClaimId;


            // Assert
            Assert.IsTrue(actualUserAccountId == expectedUserAccountId &&
                          actualScopeClaimId == expectedScopeClaimId);
        }
        #endregion
    }
}
