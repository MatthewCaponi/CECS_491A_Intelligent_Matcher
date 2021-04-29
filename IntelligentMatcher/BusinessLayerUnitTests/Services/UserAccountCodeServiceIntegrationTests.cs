using BusinessModels;
using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class UserAccountCodeServiceIntegrationTests
    {
        #region Test Setup
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 10;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountCodeRepository userAccountCodeRepository = new UserAccountCodeRepository(dataGateway, connectionString);

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

                UserAccountCodeModel userAccountCodeModel = new UserAccountCodeModel();

                userAccountCodeModel.Id = i;
                userAccountCodeModel.Code = "ABC" + i;
                userAccountCodeModel.ExpirationTime = DateTimeOffset.Parse("3/28/2007 7:13:50 PM +00:00");
                userAccountCodeModel.UserAccountId = i;

                await userAccountCodeRepository.CreateUserAccountCode(userAccountCodeModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }

            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserAccountCode", 0, connectionString, dataGateway);
        }
        #endregion

        #region Integration Tests AddCode
        [DataTestMethod]
        [DataRow("ABC11", "3/28/2007 7:13:50 PM +00:00", 11, "TestUser11", "TestPassword11", "TestSalt11",
            "TestEmailAddress11", "TestAccountType11", "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00",
            "3/28/2007 7:13:50 PM +00:00")]
        public async Task AddCode_AccountIdDoesntExists_ReturnTrue(string code,
            string expirationTime, int accountId, string username, string password, string salt, string emailAddress,
            string accountType, string accountStatus, string creationDate, string updationDate)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountCodeRepository userAccountCodeRepository = new UserAccountCodeRepository(dataGateway, connectionString);

            var userAccountModel = new UserAccountModel();

            userAccountModel.Id = accountId;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            await userAccountRepository.CreateAccount(userAccountModel);

            var expectedResult = true;

            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(userAccountCodeRepository);

            // Act
            var actualResult = await userAccountCodeService.AddCode(code, DateTimeOffset.Parse(expirationTime), accountId);

            // Assert
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion

        #region Integration Tests DeleteCodeByAccountId
        [DataTestMethod]
        [DataRow(1)]
        public async Task DeleteCodeByAccountId_AccountIdExists_ReturnTrue(int accountId)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountCodeRepository userAccountCodeRepository = new UserAccountCodeRepository(dataGateway, connectionString);

            var expectedResult = true;

            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(userAccountCodeRepository);

            // Act
            var actualResult = await userAccountCodeService.DeleteCodeByAccountId(accountId);

            // Assert
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion

        #region Integration Tests GetUserAccountCodeByAccountId
        [DataTestMethod]
        [DataRow(1, "ABC1", "3/28/2007 7:13:50 PM +00:00", 1)]
        public async Task GetUserAccountCodeByAccountId_AccountIdExists_ReturnBusinessUserAccountCodeModel(int id, string code,
            string expirationTime, int accountId)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountCodeRepository userAccountCodeRepository = new UserAccountCodeRepository(dataGateway, connectionString);

            var expectedResult = new BusinessUserAccountCodeModel();

            expectedResult.Id = id;
            expectedResult.Code = code;
            expectedResult.ExpirationTime = DateTimeOffset.Parse(expirationTime);
            expectedResult.UserAccountId = accountId;

            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(userAccountCodeRepository);

            // Act
            var actualResult = await userAccountCodeService.GetUserAccountCodeByAccountId(accountId);

            // Assert
            Assert.IsTrue
                (
                    actualResult.Id == expectedResult.Id &&
                    actualResult.Code == expectedResult.Code &&
                    actualResult.ExpirationTime == expectedResult.ExpirationTime &&
                    actualResult.UserAccountId == expectedResult.UserAccountId
                );
        }

        [DataTestMethod]
        [DataRow(15)]
        public async Task GetUserAccountCodeByAccountId_AccountIdDoesntExists_ReturnNull(int accountId)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountCodeRepository userAccountCodeRepository = new UserAccountCodeRepository(dataGateway, connectionString);

            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(userAccountCodeRepository);

            // Act
            var actualResult = await userAccountCodeService.GetUserAccountCodeByAccountId(accountId);

            // Assert
            Assert.IsNull(actualResult);
        }
        #endregion

        #region Integration Tests UpdateUserAccountCodeByAccountId
        [DataTestMethod]
        [DataRow("ABC11", "3/28/2007 7:13:50 PM +00:00", 1)]
        public async Task UpdateCode_AccountIdExists_ReturnTrue(string code, string expirationTime, int accountId)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountCodeRepository userAccountCodeRepository = new UserAccountCodeRepository(dataGateway, connectionString);

            var expectedResult = true;

            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(userAccountCodeRepository);

            // Act
            var actualResult = await userAccountCodeService.UpdateCodeByAccountId(code, DateTimeOffset.Parse(expirationTime),
                accountId);

            // Assert
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion

    }
}
