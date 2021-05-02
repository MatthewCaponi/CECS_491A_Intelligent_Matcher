using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using TestHelper;
using Logging;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class UserAccountRepositoryTests
    {
        #region Test Setup
        private static readonly ILogService _logService = new LogService();
        private static IDataGateway dataGateway;
        private static IConnectionStringData connectionString;
        private static IUserAccountRepository userAccountRepository;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            dataGateway = new SQLServerGateway(_logService);
            connectionString = new ConnectionStringData();
            userAccountRepository = new UserAccountRepository(dataGateway, connectionString, _logService);
    }
        
        // Insert test rows before every test case
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;
            
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
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
        }

        #endregion

        #region Functional Tests
        [TestMethod]
        public async Task GetAllAccounts_AtLeastTwoAccountsExist_ReturnsCorrectIds()
        {
            // Act
            IEnumerable<UserAccountModel> userAccounts = await userAccountRepository.GetAllAccounts();

            // Assert
            int i = 1;
            foreach (UserAccountModel account in userAccounts)
            {
                if (account.Id == i)
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
        [DataRow(20)]
        public async Task GetAllAccounts_AtLeastTwoAccountsExist_ReturnsCorrectNumberOfAccounts(int numAccounts)
        {
            // Act
            IEnumerable<UserAccountModel> userAccounts = await userAccountRepository.GetAllAccounts();

            // Assert
            int i = 1;
            foreach (UserAccountModel account in userAccounts)
            {
                if (account.Id == i)
                {
                    ++i;
                    continue;
                }
            }

            if (i == numAccounts + 1)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(21, "TestUser21", "TestPassword11", "TestSalt11", "TestEmail21", "TestAccountType11", 
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        [DataRow(21, "TestUser22", "TestPassword12", "TestSalt12", "TestEmail22", "TestAccountType12",
            "TestAccountStatus12", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        [DataRow(21, "TestUser23", "TestPassword13", "TestSalt13", "TestEmail23", "TestAccountType13",
            "TestAccountStatus13", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task CreateAccount_UsernameAndEmailDontExist_AccountExistsId(int expectedId, string username, string password, string salt,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel();
            userAccountModel.Id = expectedId;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            //Act
            await userAccountRepository.CreateAccount(userAccountModel);
            var actualAccount = await userAccountRepository.GetAccountById(expectedId);

            //Assert
            Assert.IsTrue(actualAccount.Id == expectedId);
        }
        
        [DataTestMethod]
        [DataRow(21, "TestUser21", "TestPassword21", "TestSalt21", "TestEmail21", "TestAccountType21",
            "TestAccountStatus21", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task CreateAccount_UsernameAndEmailDontExist_DataIsAccurate(int expectedId, string expectedUsername, 
            string expectedPassword, string expectedSalt, string expectedEmailAddress, string expectedAccountType, 
            string expectedAccountStatus, string expectedCreationDate, string expectedUpdationDate)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel();
            userAccountModel.Id = expectedId;
            userAccountModel.Username = expectedUsername;
            userAccountModel.Password = expectedPassword;
            userAccountModel.Salt = expectedSalt;
            userAccountModel.EmailAddress = expectedEmailAddress;
            userAccountModel.AccountType = expectedAccountType;
            userAccountModel.AccountStatus = expectedAccountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(expectedCreationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(expectedUpdationDate);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            //Act
            await userAccountRepository.CreateAccount(userAccountModel);
            var actualAccount = await userAccountRepository.GetAccountById(expectedId);

            //Assert
            Assert.IsTrue
                (
                    actualAccount.Id == expectedId &&
                    actualAccount.Username == expectedUsername &&
                    actualAccount.Password == expectedPassword &&
                    actualAccount.Salt == expectedSalt &&
                    actualAccount.EmailAddress == expectedEmailAddress &&
                    actualAccount.AccountType == expectedAccountType &&
                    actualAccount.AccountStatus == expectedAccountStatus
                );
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetUserAccountById_UserAccountExists_ReturnsUserAccount(int expectedId)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountById(expectedId);
            var actualId = userAccountModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1")]
        [DataRow(2, "TestUser2")]
        [DataRow(3, "TestUser3")]
        public async Task GetUserAccountById_UserAccountExists_UsernameCorrect(int id, string expectedUsername)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountById(id);
            var actualUsername = userAccountModel.Username;

            // Assert
            Assert.IsTrue(actualUsername == expectedUsername);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1")]
        [DataRow(2, "TestUser2")]
        [DataRow(3, "TestUser3")]
        public async Task GetAccountByUsername_UserAccountExists_ReturnsUserAccount(int expectedId, string username)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountByUsername(username);
            var actualId = userAccountModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow("TestUser1")]
        [DataRow("TestUser2")]
        [DataRow("TestUser3")]
        public async Task GetAccountByUsername_UserAccountExists_UsernameCorrect(string expectedUserName)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountByUsername(expectedUserName);
            var actualUsername = userAccountModel.Username;

            // Assert
            Assert.IsTrue(actualUsername == expectedUserName);
        }

        [DataTestMethod]
        [DataRow(1, "TestEmailAddress1")]
        [DataRow(2, "TestEmailAddress2")]
        [DataRow(3, "TestEmailAddress3")]
        public async Task GetAccountByEmail_AccountExists_ReturnsAccount(int expectedId, string emailAddress)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountByEmail(emailAddress);
            var actualId = userAccountModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow("TestEmailAddress1")]
        [DataRow("TestEmailAddress2")]
        [DataRow("TestEmailAddress3")]
        public async Task GetAccountByEmail_AccountExists_EmailIsCorrect(string expectedEmailAddress)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            UserAccountModel userAccountModel = await userAccount.GetAccountByEmail(expectedEmailAddress);
            var actualEmail = userAccountModel.EmailAddress;

            // Assert
            Assert.IsTrue(actualEmail == expectedEmailAddress);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1")]
        [DataRow(2, "TestUser2")]
        [DataRow(3, "TestUser3")]
        public async Task UpdateAccountUsername(int id, string expectedUsername)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccount.UpdateAccountUsername(id, expectedUsername);
            var retrievedAccount = await userAccount.GetAccountById(id);
            var actualUsername = retrievedAccount.Username;

            // Assert
            Assert.IsTrue(expectedUsername == actualUsername);
        }

        [DataTestMethod]
        [DataRow(1, "TestPass1")]
        [DataRow(2, "TestPass2")]
        [DataRow(3, "TestPass3")]
        public async Task UpdateAccountPassword(int id, string expectedPassword)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccount.UpdateAccountPassword(id, expectedPassword);
            var retrievedAccount = await userAccount.GetAccountById(id);
            var actualPassword = retrievedAccount.Password;

            // Assert
            Assert.IsTrue(expectedPassword == actualPassword);
        }

        [DataTestMethod]
        [DataRow(1, "E1F53135E559C253")]
        [DataRow(3, "84B03D034B409D4E")]
        [DataRow(5, "QxLUF1bgIAdeQXE7")]
        public async Task UpdateAccountSalt_AccountPasswordHashed_SaltIsAccurate(int id, string expectedSalt)
        {
            // Arrange
            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            //Act
            await userAccountRepository.UpdateAccountSalt(id, expectedSalt);
            var retrievedAccount = await userAccountRepository.GetAccountById(id);
            var actualSalt = retrievedAccount.Salt;

            //Assert
            Assert.IsTrue(actualSalt == expectedSalt);
        }

        [DataTestMethod]
        [DataRow(1, "TestEmailAddressUpdate1")]
        [DataRow(2, "TestEmailAddressUpdate2")]
        [DataRow(3, "TestEmailAddressUpdate3")]
        public async Task UpdateAccountEmail(int id, string expectedEmail)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccount.UpdateAccountEmail(id, expectedEmail);
            var retrievedAccount = await userAccount.GetAccountById(id);
            var actualEmail = retrievedAccount.EmailAddress;

            // Assert
            Assert.IsTrue(expectedEmail == actualEmail);
        }

        [DataTestMethod]
        [DataRow(1, "SuperAdmin")]
        [DataRow(3, "Admin")]
        [DataRow(5, "User")]
        public async Task UpdateAccountType_AccountExists_AccountTypeAccurate(int accountId, string expectedAccountType)
        {
            // Arrange
            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccountRepository.UpdateAccountType(accountId, expectedAccountType);
            var retrievedAccount = await userAccountRepository.GetAccountById(accountId);
            var actualAccountType = retrievedAccount.AccountType;

            // Assert
            Assert.IsTrue(actualAccountType == expectedAccountType);
        }

        [DataTestMethod]
        [DataRow(1, "Active")]
        [DataRow(2, "Inactive")]
        [DataRow(3, "Disabled")]
        [DataRow(4, "Banned")]
        [DataRow(5, "Suspended")]
        [DataRow(6, "Deleted")]
        public async Task UpdateAccountStatus_AccountExists_AccountStatusAccurate(int accountId, string expectedAccountStatus)
        {
            // Arrange
            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccountRepository.UpdateAccountStatus(accountId, expectedAccountStatus);
            var retrievedAccount = await userAccountRepository.GetAccountById(accountId);
            var actualAccountStatus = retrievedAccount.AccountStatus;

            // Assert
            Assert.IsTrue(actualAccountStatus == expectedAccountStatus);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task DeleteUserAccountById_AccountExists_AccountIsNull(int id)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccount.DeleteAccountById(id);
            var retrievedAccount = await userAccount.GetAccountById(id);

            // Assert
            Assert.IsNull(retrievedAccount);
        }
        #endregion

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow(400)]
        public async Task GetAllAccounts_AtLeastTwoAccountsExist_ExecutiionTimeLessThan400Milliseconds(long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await userAccountRepository.GetAllAccounts();
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(21, "TestUser21", "TestPassword21", "TestSalt21", "TestEmail21", "TestAccountType21",
            "TestAccountStatus21", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 400)]
        public async Task CreateAccount_ExecutionTimeLessThan400Milliseconds(int expectedId, string expectedUsername,
            string expectedPassword, string expectedSalt, string expectedEmailAddress, string expectedAccountType,
            string expectedAccountStatus, string expectedCreationDate, string expectedUpdationDate, long expectedMaxExecutionTime)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel();
            userAccountModel.Id = expectedId;
            userAccountModel.Username = expectedUsername;
            userAccountModel.Password = expectedPassword;
            userAccountModel.Salt = expectedSalt;
            userAccountModel.EmailAddress = expectedEmailAddress;
            userAccountModel.AccountType = expectedAccountType;
            userAccountModel.AccountStatus = expectedAccountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(expectedCreationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(expectedUpdationDate);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            //Act
            var timer = Stopwatch.StartNew();
            await userAccountRepository.CreateAccount(userAccountModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + timer.Elapsed);

            //Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, "E1F53135E559C253", 400)]
        public async Task UpdateAccountSalt_AccountPasswordHashed_ExecutiionTimeLessThan400Milliseconds(int id, string expectedSalt, long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            //Act
            var timer = Stopwatch.StartNew();
            await userAccountRepository.UpdateAccountSalt(id, expectedSalt);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, 400)]
        public async Task DeleteUserAccountById_AccountExists_ExecutiionTimeLessThan400Milliseconds(int id, long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await userAccount.DeleteAccountById(id);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }
        #endregion
    }
}
