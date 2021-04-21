using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using IntelligentMatcher.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;
using UserManagement.Models;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class UserAccountServiceIntegrationTests
    {
        #region Test Setup
        // Insert test rows before every test case
        [TestInitialize]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 10;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

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
        }
        #endregion

        #region Integration Tests GetUserAccountByUsername
        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task GetUserAccountByUsername_CorrectUsername_ReturnsWebUserAccountModel(int accountId,
            string username, string emailAddress, string accountType, string accountStatus, string creationDate,
            string updationDate)
        {
            // Arrange
            // Setting up each dependency of UserAccountService and dependencies of dependencies
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            var expectedResult = new WebUserAccountModel();

            expectedResult.Id = accountId;
            expectedResult.Username = username;
            expectedResult.EmailAddress = emailAddress;
            expectedResult.AccountType = accountType;
            expectedResult.AccountStatus = accountStatus;
            expectedResult.CreationDate = DateTimeOffset.Parse(creationDate);
            expectedResult.UpdationDate = DateTimeOffset.Parse(updationDate);

            // Finally, instantiate the actual class being tested and pass in the dependencies
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);

            // Arrange
            var actualResult = await userAccountService.GetUserAccountByUsername(username);

            // Act
            Assert.IsTrue(
                actualResult.Id == expectedResult.Id &&
                actualResult.Username == expectedResult.Username
            );
        }

        [DataTestMethod]
        [DataRow("TestUser11")]
        public async Task GetUserAccountByUsername_WrongUsername_ReturnsNull(string username)
        {
            // Arrange
            // Setting up each dependency of UserAccountService and dependencies of dependencies
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            // Finally, instantiate the actual class being tested and pass in the dependencies
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);

            // Arrange
            var actualResult = await userAccountService.GetUserAccountByUsername(username);

            // Act
            Assert.IsNull(actualResult);
        }
        #endregion

        #region Integration Tests GetUserAccountByEmail
        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task GetUserAccountByEmail_CorrectEmail_ReturnsWebUserAccountModel(int accountId,
            string username, string emailAddress, string accountType, string accountStatus, string creationDate,
            string updationDate)
        {
            // Arrange
            // Setting up each dependency of UserAccountService and dependencies of dependencies
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            var expectedResult = new WebUserAccountModel();

            expectedResult.Id = accountId;
            expectedResult.Username = username;
            expectedResult.EmailAddress = emailAddress;
            expectedResult.AccountType = accountType;
            expectedResult.AccountStatus = accountStatus;
            expectedResult.CreationDate = DateTimeOffset.Parse(creationDate);
            expectedResult.UpdationDate = DateTimeOffset.Parse(updationDate);

            // Finally, instantiate the actual class being tested and pass in the dependencies
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);

            // Arrange
            var actualResult = await userAccountService.GetUserAccountByEmail(emailAddress);

            // Act
            Assert.IsTrue(
                actualResult.Id == expectedResult.Id &&
                actualResult.EmailAddress == expectedResult.EmailAddress
            );
        }

        [DataTestMethod]
        [DataRow("TestEmailAddress11")]
        public async Task GetUserAccountByEmail_WrongEmail_ReturnsNull(string emailAddress)
        {
            // Arrange
            // Setting up each dependency of UserAccountService and dependencies of dependencies
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            // Finally, instantiate the actual class being tested and pass in the dependencies
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);

            // Arrange
            var actualResult = await userAccountService.GetUserAccountByEmail(emailAddress);

            // Act
            Assert.IsNull(actualResult);
        }
        #endregion
    }
}