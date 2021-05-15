//using AuthorizationServices;
//using DataAccess;
//using DataAccess.Repositories;
//using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
//using Exceptions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Models;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using TestHelper;
//using UserAccessControlServices;

//namespace BusinessLayerUnitTests.UserAccessControl
//{
//    [TestClass]
//    public class ClaimsServiceIntegrationTests
//    {
//        private readonly IClaimRepository claimRepository =
//            new ClaimRepository(new SQLServerGateway(), new ConnectionStringData());
//        private readonly IScopeRepository scopeRepository =
//            new ScopeRepository(new SQLServerGateway(), new ConnectionStringData());
//        private readonly IScopeClaimRepository scopeClaimRepository =
//            new ScopeClaimRepository(new SQLServerGateway(), new ConnectionStringData());
//        private readonly IUserScopeClaimRepository userScopeClaimRepository =
//            new UserScopeClaimRepository(new SQLServerGateway(), new ConnectionStringData());

//        #region Test Setup
//        // Insert test rows before every test case
//        [TestInitialize()]
//        public async Task Init()
//        {
//            await TestCleaner.CleanDatabase();
//            var numTestRows = 20;

//            IDataGateway dataGateway = new SQLServerGateway();
//            IConnectionStringData connectionString = new ConnectionStringData();

//            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
//            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);
//            IClaimRepository claimRepository = new ClaimRepository(dataGateway, connectionString);
//            IScopeClaimRepository scopeClaimRepository = new ScopeClaimRepository(dataGateway, connectionString);
//            IUserScopeClaimRepository userScopeClaimRepository = new UserScopeClaimRepository(dataGateway, connectionString);

//            for (int i = 1; i <= numTestRows; ++i)
//            {
//                UserAccountModel userAccountModel = new UserAccountModel();
//                userAccountModel.Id = i;
//                userAccountModel.Username = "TestUser" + i;
//                userAccountModel.Password = "TestPassword" + i;
//                userAccountModel.Salt = "TestSalt" + i;
//                userAccountModel.EmailAddress = "TestEmailAddress" + i;
//                userAccountModel.AccountType = "TestAccountType" + i;
//                userAccountModel.AccountStatus = "TestAccountStatus" + i;
//                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
//                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

//                await userAccountRepository.CreateAccount(userAccountModel);

//                Models.User_Access_Control.ScopeModel scopeModel = new Models.User_Access_Control.ScopeModel();
//                scopeModel.Id = i;
//                scopeModel.Type = "TestScope" + i;
//                scopeModel.Description = "TestDescription" + i;
//                scopeModel.IsDefault = true;

//                await scopeRepository.CreateScope(scopeModel);

//                Models.User_Access_Control.ClaimModel claimModel = new Models.User_Access_Control.ClaimModel();
//                claimModel.Id = i;
//                claimModel.Type = "TestClaim" + i;
//                claimModel.Value = "TestDescription" + i;
//                claimModel.IsDefault = true;

//                await claimRepository.CreateClaim(claimModel);

//                Models.User_Access_Control.ScopeClaimModel scopeClaimModel =
//                    new Models.User_Access_Control.ScopeClaimModel();
//                scopeClaimModel.Id = i;
//                scopeClaimModel.ScopeId = i;
//                scopeClaimModel.ClaimId = i;

//                await scopeClaimRepository.CreateScopeClaim(scopeClaimModel);

//                Models.User_Access_Control.UserScopeClaimModel userScopeClaimModel =
//                    new Models.User_Access_Control.UserScopeClaimModel();
//                userScopeClaimModel.Id = i;
//                userScopeClaimModel.UserAccountId = i;
//                userScopeClaimModel.UserScopeId = i;
//                userScopeClaimModel.UserClaimId = i;

//                await userScopeClaimRepository.CreateUserScopeClaim(userScopeClaimModel);
//            }
//        }

//        // Remove test rows and reset id counter after every test case
//        [TestCleanup()]
//        public async Task CleanUp()
//        {
//            await TestCleaner.CleanDatabase();
//        }
//        #endregion

//        #region Integration Tests
//        [TestMethod]
//        public async Task GetAllClaims_CorrectNumberOfClaims()
//        {
//            // Arrange
//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            var claimList = await claimsService.GetAllClaims();

//            // Assert
//            int i = 1;
//            foreach (var claim in claimList)
//            {
//                if (claim.Id == i)
//                {
//                    ++i;
//                    continue;
//                }

//                Assert.IsTrue(false);
//                return;
//            }

//            Assert.IsTrue(true);
//        }

//        [TestMethod]
//        public async Task GetAllUserClaims_CorrectNumberOfUserClaims()
//        {
//            // Arrange
//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            var userClaimList = await claimsService.GetAllUserClaims();

//            // Assert
//            int i = 1;
//            foreach (var userClaim in userClaimList)
//            {
//                var claimModel = await claimsService.GetClaim(i);
//                if (userClaim.Type == claimModel.Type)
//                {
//                    ++i;
//                    continue;
//                }

//                Assert.IsTrue(false);
//                return;
//            }

//            Assert.IsTrue(true);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestClaim1", "TestDescription1", true)]
//        public async Task GetClaim_ClaimIdExists_ReturnClaim(int id, string name, string description, bool isDefault)
//        {
//            // Arrange
//            var expectedResult = new BusinessModels.UserAccessControl.ClaimModel();

//            expectedResult.Id = id;
//            expectedResult.Type = name;
//            expectedResult.Value = description;
//            expectedResult.IsDefault = isDefault;

//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            var actualResult = await claimsService.GetClaim(id);

//            // Assert
//            Assert.IsTrue(actualResult.Id == expectedResult.Id);
//            Assert.IsTrue(actualResult.Type == expectedResult.Type);
//            Assert.IsTrue(actualResult.Value == expectedResult.Value);
//            Assert.IsTrue(actualResult.IsDefault == expectedResult.IsDefault);
//        }

//        [DataTestMethod]
//        [DataRow(21, "Claim was Null.")]
//        public async Task GetClaim_ClaimIdDoesNotExists_ReturnError(int id, string error)
//        {
//            // Arrange
//            var expectedMessage = error;
//            string actualMessage = "";
//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            try
//            {
//                var actualResult = await claimsService.GetClaim(id);
//            }
//            catch (NullReferenceException e)
//            {
//                actualMessage = e.Message;
//            }

//            // Assert
//            Assert.IsTrue(actualMessage == expectedMessage);
//        }

//        [DataTestMethod]
//        [DataRow(21, "TestClaim21", "TestDescription21", true)]
//        public async Task CreateClaim_Success_ReturnClaimId(int id, string name, string description, bool isDefault)
//        {
//            // Arrange
//            var claimModel = new BusinessModels.UserAccessControl.ClaimModel();

//            claimModel.Type = name;
//            claimModel.Value = description;
//            claimModel.IsDefault = isDefault;

//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            var actualResult = await claimsService.CreateClaim(claimModel);

//            // Assert
//            Assert.IsTrue(actualResult == id);
//        }

//        [DataTestMethod]
//        [DataRow("Claim could not be created.")]
//        public async Task CreateClaim_Failure_ReturnError(string error)
//        {
//            // Arrange
//            var claimModel = new BusinessModels.UserAccessControl.ClaimModel();

//            var expectedMessage = error;
//            string actualMessage = "";

//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            try
//            {
//                var actualResult = await claimsService.CreateClaim(claimModel);
//            }
//            catch (SqlCustomException e)
//            {
//                actualMessage = e.Message;
//            }

//            // Assert
//            Assert.IsTrue(actualMessage == expectedMessage);
//        }

//        [DataTestMethod]
//        [DataRow(21, "TestClaim21", "TestDescription21", true)]
//        public async Task CreateClaim_Success_ReturnInfoIsAccurate(int id, string name, string description, bool isDefault)
//        {
//            // Arrange
//            var claimModel = new BusinessModels.UserAccessControl.ClaimModel();

//            claimModel.Type = name;
//            claimModel.Value = description;
//            claimModel.IsDefault = isDefault;

//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            var actualResult = await claimsService.CreateClaim(claimModel);
//            var newClaimModel = await claimsService.GetClaim(id);

//            // Assert
//            Assert.IsTrue(newClaimModel.Id == id);
//            Assert.IsTrue(newClaimModel.Type == name);
//            Assert.IsTrue(newClaimModel.Value == description);
//            Assert.IsTrue(newClaimModel.IsDefault == isDefault);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestClaim21", "TestDescription21", true)]
//        public async Task UpdateClaim_Success_ReturnTrue(int id, string name, string description, bool isDefault)
//        {
//            // Arrange
//            var claimModel = new BusinessModels.UserAccessControl.ClaimModel();

//            claimModel.Id = id;
//            claimModel.Type = name;
//            claimModel.Value = description;
//            claimModel.IsDefault = isDefault;

//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            var actualResult = await claimsService.UpdateClaim(claimModel);

//            // Assert
//            Assert.IsTrue(actualResult);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestClaim21", "TestDescription21", true)]
//        public async Task UpdateClaim_Success_ReturnInfoIsAccurate(int id, string name, string description, bool isDefault)
//        {
//            // Arrange
//            var claimModel = new BusinessModels.UserAccessControl.ClaimModel();

//            claimModel.Id = id;
//            claimModel.Type = name;
//            claimModel.Value = description;
//            claimModel.IsDefault = isDefault;

//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            var actualResult = await claimsService.UpdateClaim(claimModel);
//            var newClaimModel = await claimsService.GetClaim(id);

//            // Assert
//            Assert.IsTrue(newClaimModel.Id == id);
//            Assert.IsTrue(newClaimModel.Type == name);
//            Assert.IsTrue(newClaimModel.Value == description);
//            Assert.IsTrue(newClaimModel.IsDefault == isDefault);
//        }

//        [DataTestMethod]
//        [DataRow(1, "Claim could not be updated.")]
//        public async Task UpdateClaim_Failure_ReturnError(int id, string error)
//        {
//            // Arrange
//            var claimModel = new BusinessModels.UserAccessControl.ClaimModel();

//            claimModel.Id = id;

//            var expectedMessage = error;
//            string actualMessage = "";

//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            try
//            {
//                var actualResult = await claimsService.UpdateClaim(claimModel);
//            }
//            catch (SqlCustomException e)
//            {
//                actualMessage = e.Message;
//            }

//            // Assert
//            Assert.IsTrue(actualMessage == expectedMessage);
//        }

//        [DataTestMethod]
//        [DataRow(1)]
//        public async Task DeleteClaim_ClaimIdExists_ClaimIsNull(int id)
//        {
//            // Arrange
//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            await claimsService.DeleteClaim(id);
//            var actualResult = await claimRepository.GetClaimById(id);

//            // Assert
//            Assert.IsNull(actualResult);
//        }

//        [DataTestMethod]
//        [DataRow(1)]
//        public async Task DeleteClaim_ClaimIdExists_ReturnTrue(int id)
//        {
//            // Arrange
//            IClaimsService claimsService = new ClaimsService(claimRepository, scopeRepository,
//                scopeClaimRepository, userScopeClaimRepository);

//            // Act
//            var actualResult = await claimsService.DeleteClaim(id);

//            // Assert
//            Assert.IsTrue(actualResult);
//        }
//        #endregion
//    }
//}
