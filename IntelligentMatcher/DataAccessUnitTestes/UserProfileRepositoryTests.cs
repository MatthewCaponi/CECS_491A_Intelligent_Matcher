using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class UserProfileRepositoryTests
    {
        #region Test Setup
        // Insert test rows before every test case
        [TestInitialize()]
        public async Task Init()
        {
            var numTestRows = 20;

            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);

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

                UserProfileModel userProfileModel = new UserProfileModel();
                userProfileModel.Id = i;
                userProfileModel.FirstName = "TestFirstName" + i;
                userProfileModel.Surname = "TestSurname" + i;
                userProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
                userProfileModel.UserAccountId = userAccountModel.Id;
                await userProfileRepository.CreateUserProfile(userProfileModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserProfile", 0, connectionString, dataGateway);
        }
        #endregion

        #region Functional Tests
        [TestMethod]
        public async Task GetAllUserProfiles_AtLeastTwoProfilesExist_ReturnsCorrectUserAccountIds()
        {
            // Arrange
            IUserProfileRepository userProfileRepository = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            IEnumerable<UserProfileModel> userProfiles = await userProfileRepository.GetAllUserProfiles();

            // Assert
            int i = 1;
            foreach (UserProfileModel profile in userProfiles)
            {
                if (profile.UserAccountId == i)
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
        public async Task GetAllUserProfiles_AtLeastTwoProfilesExist_ReturnsCorrectNumberOfRows(int numRows)
        {
            // Arrange
            IUserProfileRepository userProfileRepository = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            IEnumerable<UserProfileModel> userProfiles = await userProfileRepository.GetAllUserProfiles();

            // Assert
            int i = 1;
            foreach (UserProfileModel profile in userProfiles)
            {
                if (profile.UserAccountId == i)
                {
                    ++i;
                    continue;
                }
            }

            if (i == numRows + 1)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow("TestFirstName21", "TestSurname21", 21, "TestUser21", "TestPassword11", "TestSalt11", "TestEmail21", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task CreateUserProfile_UserProfileDoesNotExist_ReturnsCorrectId(string firstName, string surname,
            int id, string username, string password, string salt,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel();
            userAccountModel.Id = id;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            UserProfileModel userProfileModel = new UserProfileModel();
            userProfileModel.FirstName = firstName;
            userProfileModel.Surname = surname;
            userProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            userProfileModel.UserAccountId = id;

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            IUserProfileRepository userProfileRepository = new UserProfileRepository(new DataGateway(), new ConnectionStringData());
            
            //Act
            await userAccountRepository.CreateAccount(userAccountModel);
            await userProfileRepository.CreateUserProfile(userProfileModel);
            var actualUserProfile = await userProfileRepository.GetUserProfileByAccountId(userAccountModel.Id);

            //Assert
            Assert.IsTrue(actualUserProfile.UserAccountId == id);
        }

        [DataTestMethod]
        [DataRow(1, "TestSurname1")]
        [DataRow(2, "TestSurname2")]
        [DataRow(3, "TestSurname3")]
        public async Task GetUserProfileByAccountId_UserProfileExists_ReturnCorrectUsername(int accountId, string expectedSurname)
        {
            // Arrange
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userProfileModel = await userProfile.GetUserProfileByAccountId(accountId);
            var actualSurname = userProfileModel.Surname;

            // Assert
            Assert.IsTrue(actualSurname == expectedSurname);
        }

        [DataTestMethod]
        [DataRow(1, "TestSurname1")]
        [DataRow(2, "TestSurname2")]
        [DataRow(3, "TestSurname3")]
        public async Task GetUserProfileById(int id, string expectedSurname)
        {
            // Arrange
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userProfileModel = await userProfile.GetUserProfileById(id);
            var actualSurname = userProfileModel.Surname;

            // Assert
            Assert.IsTrue(actualSurname == expectedSurname);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task DeleteUserProfileByAccountId_UserProfileExists_ReturnsNull(int accountId)
        {
            // Arrange
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            await userProfile.DeleteUserProfileByAccountId(accountId);
            var retrievedAccount = await userProfile.GetUserProfileByAccountId(accountId);

            // Assert
            Assert.IsNull(retrievedAccount);
        }
        #endregion

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow(400)]
        public async Task GetAllUserProfiles_AtLeastTwoProfilesExist_ExecutionTimeLessThan400Milliseconds(long expectedMaxExecutionTime)
        {
            // Arrange
            IUserProfileRepository userProfileRepository = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await userProfileRepository.GetAllUserProfiles();
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow("TestFirstName21", "TestSurname21", 21, "TestUser21", "TestPassword11", "TestSalt11", "TestEmail21", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 400)]
        public async Task CreateUserProfile_UserProfileDoesNotExist_ExecutionTimeLessThan400Milliseconds(string firstName, string surname,
            int id, string username, string password, string salt,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate, long expectedMaxExecutionTime)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel();
            userAccountModel.Id = id;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            UserProfileModel userProfileModel = new UserProfileModel();
            userProfileModel.FirstName = firstName;
            userProfileModel.Surname = surname;
            userProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            userProfileModel.UserAccountId = id;

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            IUserProfileRepository userProfileRepository = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            //Act
            await userAccountRepository.CreateAccount(userAccountModel);
            var timer = Stopwatch.StartNew();
            await userProfileRepository.CreateUserProfile(userProfileModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            //Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, 400)]
        public async Task DeleteUserProfileByAccountId_UserProfileExists_ExecutionTimeLessThan400Milliseconds(int accountId, long expectedMaxExecutionTime)
        {
            // Arrange
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await userProfile.DeleteUserProfileByAccountId(accountId);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }
        #endregion
    }
}
