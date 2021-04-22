using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class UserAccountCodeRepositoryTests
    {
        #region Test Setup
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;

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

        #region Functional Tests
        [TestMethod]
        public async Task GetAllUserAccountCodes_AtLeastTwoUserAccountCodesExist_ReturnsCorrectIds()
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            IEnumerable<UserAccountCodeModel> userAccountCodes = await userAccountCodeRepository.GetAllUserAccountCodes();

            // Assert
            int i = 1;
            foreach (UserAccountCodeModel userAccountCode in userAccountCodes)
            {
                if (userAccountCode.Id == i)
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
        public async Task GetAllUserAccountCodes_AtLeastTwoUserAccountCodesExist_ReturnsCorrectNumberOfUserAccountCodes
            (int numUserAccountCodes)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            IEnumerable<UserAccountCodeModel> userAccountCodes = await userAccountCodeRepository.GetAllUserAccountCodes();

            // Assert
            int i = 1;
            foreach (UserAccountCodeModel userAccountCode in userAccountCodes)
            {
                if (userAccountCode.Id == i)
                {
                    ++i;
                    continue;
                }
            }

            if (i == numUserAccountCodes + 1)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(21, "ABC21", "3/28/2007 7:13:50 PM +00:00", 21, "TestUser21", "TestPassword11", "TestSalt11", "TestEmail21",
            "TestAccountType11", "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task CreateUserAccountCode_UserAccountIdExists_UserAccountCodeIdExists
            (int expectedId, string code, string expirationTime, int accountId, string username, string password, string salt,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
        {
            // Arrange
            IUserAccountRepository userAccountRepository =
                new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            UserAccountModel userAccountModel = new UserAccountModel();

            userAccountModel.Id = accountId;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            UserAccountCodeModel userAccountCodeModel = new UserAccountCodeModel();

            userAccountCodeModel.Id = expectedId;
            userAccountCodeModel.Code = code;
            userAccountCodeModel.ExpirationTime = DateTimeOffset.Parse(expirationTime);
            userAccountCodeModel.UserAccountId = accountId;

            // Act
            await userAccountRepository.CreateAccount(userAccountModel);
            await userAccountCodeRepository.CreateUserAccountCode(userAccountCodeModel);
            var actualUserAccountCode = await userAccountCodeRepository.GetUserAccountCodeById(expectedId);

            // Assert
            Assert.IsTrue(actualUserAccountCode.Id == expectedId);
        }

        [DataTestMethod]
        [DataRow(21, "ABC21", "3/28/2007 7:13:50 PM +00:00", 21, "TestUser21", "TestPassword11", "TestSalt11", "TestEmail21",
            "TestAccountType11", "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task CreateUserAccountCode_UserAccountIdDoesntExist_DataIsAccurate
            (int expectedId, string expectedCode, string expectedExpirationTime, int expectedAccountId,
            string username, string password, string salt, string emailAddress, string accountType, string accountStatus,
            string creationDate, string updationDate)
        {
            // Arrange
            IUserAccountRepository userAccountRepository =
                new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            UserAccountModel userAccountModel = new UserAccountModel();

            userAccountModel.Id = expectedAccountId;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            UserAccountCodeModel userAccountCodeModel = new UserAccountCodeModel();

            userAccountCodeModel.Id = expectedId;
            userAccountCodeModel.Code = expectedCode;
            userAccountCodeModel.ExpirationTime = DateTimeOffset.Parse(expectedExpirationTime);
            userAccountCodeModel.UserAccountId = expectedAccountId;

            // Act
            await userAccountRepository.CreateAccount(userAccountModel);
            await userAccountCodeRepository.CreateUserAccountCode(userAccountCodeModel);
            var actualUserAccountCode = await userAccountCodeRepository.GetUserAccountCodeById(expectedId);

            // Assert
            Assert.IsTrue
                (
                    actualUserAccountCode.Id == expectedId &&
                    actualUserAccountCode.Code == expectedCode &&
                    actualUserAccountCode.ExpirationTime == DateTimeOffset.Parse(expectedExpirationTime) &&
                    actualUserAccountCode.UserAccountId == expectedAccountId
                );
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetUserAccountCodeById_UserAccountCodeExists_ReturnsUserAccountCode(int expectedId)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountCodeModel = await userAccountCodeRepository.GetUserAccountCodeById(expectedId);
            var actualId = userAccountCodeModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        public async Task GetUserAccountCodeById_UserAccountCodeExists_UserAccountIdIsCorrect(int expectedId, int expectedAccountId)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountCodeModel = await userAccountCodeRepository.GetUserAccountCodeById(expectedId);
            var actualAccountId = userAccountCodeModel.UserAccountId;

            // Assert
            Assert.IsTrue(actualAccountId == expectedAccountId);
        }

        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        public async Task GetUserAccountCodeByAccountId_UserAccountCodeExists_ReturnsUserAccountCode(int accountId, int expectedId)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountCodeModel = await userAccountCodeRepository.GetUserAccountCodeByAccountId(accountId);
            var actualId = userAccountCodeModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetUserAccountCodeByAccountId_UserAccountCodeExists_UserAccountIdIsCorrect(int expectedAccountId)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var userAccountCodeModel = await userAccountCodeRepository.GetUserAccountCodeByAccountId(expectedAccountId);
            var actualAccountId = userAccountCodeModel.UserAccountId;

            // Assert
            Assert.IsTrue(actualAccountId == expectedAccountId);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task DeleteUserAccountCodeById_UserAccountCodeExists_UserAccountCodeIsNull(int id)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccountCodeRepository.DeleteUserAccountCodeById(id);
            var retrievedUserAccountCode = await userAccountCodeRepository.GetUserAccountCodeById(id);

            // Assert
            Assert.IsNull(retrievedUserAccountCode);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task DeleteUserAccountCodeByAccountId_UserAccountCodeExists_UserAccountCodeIsNull(int accountId)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccountCodeRepository.DeleteUserAccountCodeByAccountId(accountId);
            var retrievedUserAccountCode = await userAccountCodeRepository.GetUserAccountCodeByAccountId(accountId);

            // Assert
            Assert.IsNull(retrievedUserAccountCode);
        }

        [DataTestMethod]
        [DataRow(1, "ABC21", "3/28/2008 7:13:50 PM +00:00", 400)]
        public async Task UpdateAccountCodeById_UserAccountCodeExists_DataIsAccurate
            (int id, string code, string expirationTime, long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccountCodeRepository.UpdateUserAccountCodeById(code, DateTimeOffset.Parse(expirationTime), id);
            var newUserAccountCode = await userAccountCodeRepository.GetUserAccountCodeById(id);

            // Assert
            Assert.IsTrue(newUserAccountCode.Id == id);
            Assert.IsTrue(newUserAccountCode.Code == code);
            Assert.IsTrue(newUserAccountCode.ExpirationTime == DateTimeOffset.Parse(expirationTime));
        }

        [DataTestMethod]
        [DataRow(1, "ABC21", "3/28/2008 7:13:50 PM +00:00", 400)]
        public async Task UpdateAccountCodeByAccountId_UserAccountCodeExists_DataIsAccurate
            (int accountId, string code, string expirationTime, long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await userAccountCodeRepository.UpdateUserAccountCodeById(code, DateTimeOffset.Parse(expirationTime), accountId);
            var newUserAccountCode = await userAccountCodeRepository.GetUserAccountCodeByAccountId(accountId);

            // Assert
            Assert.IsTrue(newUserAccountCode.UserAccountId == accountId);
            Assert.IsTrue(newUserAccountCode.Code == code);
            Assert.IsTrue(newUserAccountCode.ExpirationTime == DateTimeOffset.Parse(expirationTime));
        }
        #endregion

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow(400)]
        public async Task GetAllUserAccountCodes_AtLeastTwoUserAccountCodesExist_ExecutionTimeLessThan400Milliseconds
            (long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await userAccountCodeRepository.GetAllUserAccountCodes();
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(21, "ABC21", "3/28/2007 7:13:50 PM +00:00", 21, "TestUser21", "TestPassword11", "TestSalt11", "TestEmail21",
            "TestAccountType11", "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 400)]
        public async Task CreateUserAccountCode_ExecutionTimeLessThan400Milliseconds
            (int id, string code, string expirationTime, int accountId, string username, string password, string salt,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());
            IUserAccountRepository userAccountRepository =
                new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());

            UserAccountModel userAccountModel = new UserAccountModel();

            userAccountModel.Id = accountId;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            UserAccountCodeModel userAccountCodeModel = new UserAccountCodeModel();

            await userAccountRepository.CreateAccount(userAccountModel);

            userAccountCodeModel.Id = id;
            userAccountCodeModel.Code = code;
            userAccountCodeModel.ExpirationTime = DateTimeOffset.Parse(expirationTime);
            userAccountCodeModel.UserAccountId = accountId;

            // Act
            var timer = Stopwatch.StartNew();
            await userAccountCodeRepository.CreateUserAccountCode(userAccountCodeModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, 400)]
        public async Task DeleteAccountCodeById_ExecutionTimeLessThan400Milliseconds
            (int id, long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await userAccountCodeRepository.DeleteUserAccountCodeById(id);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, "ABC21", "3/28/2007 7:13:50 PM +00:00", 400)]
        public async Task UpdateAccountCodeById_ExecutionTimeLessThan400Milliseconds
            (int id, string code, string expirationTime, long expectedMaxExecutionTime)
        {
            // Arrange
            IUserAccountCodeRepository userAccountCodeRepository =
                new UserAccountCodeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await userAccountCodeRepository.UpdateUserAccountCodeById(code, DateTimeOffset.Parse(expirationTime), id);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }
        #endregion
    }
}
