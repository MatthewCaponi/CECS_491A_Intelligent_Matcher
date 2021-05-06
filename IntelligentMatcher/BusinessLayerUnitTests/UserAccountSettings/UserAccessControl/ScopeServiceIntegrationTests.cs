using AuthorizationServices;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Exceptions;
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
    public class ScopeServiceIntegrationTests
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
        [TestMethod]
        public async Task GetAllScopes_CorrectNumberOfScopes()
        {
            // Arrange
            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var scopeList = await scopeService.GetAllScopes();

            // Assert
            int i = 1;
            foreach (var scope in scopeList)
            {
                if (scope.Id == i)
                {
                    ++i;
                    continue;
                }

                Assert.IsTrue(false);
                return;
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task GetAllUserScopes_CorrectNumberOfUserScopes()
        {
            // Arrange
            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var userScopeList = await scopeService.GetAllUserScopes();

            // Assert
            int i = 1;
            foreach (var userScope in userScopeList)
            {
                var scopeModel = await scopeService.GetScope(i);
                if (userScope.Key == scopeModel.Name)
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
        [DataRow(1, "TestScope1", "TestDescription1", true)]
        public async Task GetScope_ScopeIdExists_ReturnScope(int id, string name, string description, bool isDefault)
        {
            // Arrange
            var expectedResult = new BusinessModels.UserAccessControl.ScopeModel();

            expectedResult.Id = id;
            expectedResult.Name = name;
            expectedResult.Description = description;
            expectedResult.IsDefault = isDefault;

            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var actualResult = await scopeService.GetScope(id);

            // Assert
            Assert.IsTrue(actualResult.Id == expectedResult.Id);
            Assert.IsTrue(actualResult.Name == expectedResult.Name);
            Assert.IsTrue(actualResult.Description == expectedResult.Description);
            Assert.IsTrue(actualResult.IsDefault == expectedResult.IsDefault);
        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task GetScope_ScopeIdExists_NumberOfClaimsIsAccurate(int id)
        {
            // Arrange
            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var actualResult = await scopeService.GetScope(id);

            // Assert
            int i = 1;
            foreach (var claim in actualResult.Claims)
            {
                if (claim.Id == i)
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
        [DataRow(21, "Scope was Null.")]
        public async Task GetScope_ScopeIdDoesNotExists_ReturnError(int id, string error)
        {
            // Arrange
            var expectedMessage = error;
            string actualMessage = "";
            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            try
            {
                var actualResult = await scopeService.GetScope(id);
            }
            catch (NullReferenceException e)
            {
                actualMessage = e.Message;
            }

            // Assert
            Assert.IsTrue(actualMessage == expectedMessage);
        }

        [DataTestMethod]
        [DataRow(21, "TestScope21", "TestDescription21", true)]
        public async Task CreateScope_Success_ReturnScopeId(int id, string name, string description, bool isDefault)
        {
            // Arrange
            var scopeModel = new BusinessModels.UserAccessControl.ScopeModel();

            scopeModel.Name = name;
            scopeModel.Description = description;
            scopeModel.IsDefault = isDefault;

            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var actualResult = await scopeService.CreateScope(scopeModel);

            // Assert
            Assert.IsTrue(actualResult == id);
        }

        [DataTestMethod]
        [DataRow("Scope could not be created.")]
        public async Task CreateScope_Failure_ReturnError(string error)
        {
            // Arrange
            var scopeModel = new BusinessModels.UserAccessControl.ScopeModel();

            var expectedMessage = error;
            string actualMessage = "";

            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            try
            {
                var actualResult = await scopeService.CreateScope(scopeModel);
            }
            catch (SqlCustomException e)
            {
                actualMessage = e.Message;
            }
            
            // Assert
            Assert.IsTrue(actualMessage == expectedMessage);
        }

        [DataTestMethod]
        [DataRow(21, "TestScope21", "TestDescription21", true)]
        public async Task CreateScope_Success_ReturnInfoIsAccurate(int id, string name, string description, bool isDefault)
        {
            // Arrange
            var scopeModel = new BusinessModels.UserAccessControl.ScopeModel();

            scopeModel.Name = name;
            scopeModel.Description = description;
            scopeModel.IsDefault = isDefault;

            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var actualResult = await scopeService.CreateScope(scopeModel);
            var newScopeModel = await scopeService.GetScope(id);

            // Assert
            Assert.IsTrue(newScopeModel.Id == id);
            Assert.IsTrue(newScopeModel.Name == name);
            Assert.IsTrue(newScopeModel.Description == description);
            Assert.IsTrue(newScopeModel.IsDefault == isDefault);
        }

        [DataTestMethod]
        [DataRow(1, "TestScope21", "TestDescription21", true)]
        public async Task UpdateScope_Success_ReturnTrue(int id, string name, string description, bool isDefault)
        {
            // Arrange
            var scopeModel = new BusinessModels.UserAccessControl.ScopeModel();

            scopeModel.Id = id;
            scopeModel.Name = name;
            scopeModel.Description = description;
            scopeModel.IsDefault = isDefault;

            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var actualResult = await scopeService.UpdateScope(scopeModel);

            // Assert
            Assert.IsTrue(actualResult);
        }

        [DataTestMethod]
        [DataRow(1, "TestScope21", "TestDescription21", true)]
        public async Task UpdateScope_Success_ReturnInfoIsAccurate(int id, string name, string description, bool isDefault)
        {
            // Arrange
            var scopeModel = new BusinessModels.UserAccessControl.ScopeModel();

            scopeModel.Id = id;
            scopeModel.Name = name;
            scopeModel.Description = description;
            scopeModel.IsDefault = isDefault;

            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var actualResult = await scopeService.UpdateScope(scopeModel);
            var newScopeModel = await scopeService.GetScope(id);

            // Assert
            Assert.IsTrue(newScopeModel.Id == id);
            Assert.IsTrue(newScopeModel.Name == name);
            Assert.IsTrue(newScopeModel.Description == description);
            Assert.IsTrue(newScopeModel.IsDefault == isDefault);
        }

        [DataTestMethod]
        [DataRow(1, "Scope could not be updated.")]
        public async Task UpdateScope_Failure_ReturnError(int id, string error)
        {
            // Arrange
            var scopeModel = new BusinessModels.UserAccessControl.ScopeModel();

            scopeModel.Id = id;

            var expectedMessage = error;
            string actualMessage = "";

            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            try
            {
                var actualResult = await scopeService.UpdateScope(scopeModel);
            }
            catch (SqlCustomException e)
            {
                actualMessage = e.Message;
            }
            
            // Assert
            Assert.IsTrue(actualMessage == expectedMessage);
        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task DeleteScope_ScopeIdExists_ScopeIsNull(int id)
        {
            // Arrange
            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            await scopeService.DeleteScope(id);
            var actualResult = await scopeRepository.GetScopeById(id);

            // Assert
            Assert.IsNull(actualResult);
        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task DeleteScope_ScopeIdExists_ReturnTrue(int id)
        {
            // Arrange
            IScopeService scopeService = new ScopeService(claimRepository, scopeRepository,
                scopeClaimRepository, userScopeClaimRepository);

            // Act
            var actualResult = await scopeService.DeleteScope(id);

            // Assert
            Assert.IsTrue(actualResult);
        }
        #endregion
    }
}
