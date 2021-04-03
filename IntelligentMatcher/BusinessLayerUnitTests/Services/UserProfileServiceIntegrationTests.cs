using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;
using UserManagement.Models;
using UserManagement.Services;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class UserProfileServiceIntegrationTests
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
            IDataGateway dataGateway = new SQLServerGateway();
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

        #region Integration Tests GetUserProfileByAccountId
        [DataTestMethod]
        [DataRow(1, "TestFirstName1", "TestSurname1", "3/28/2007 7:13:50 PM +00:00", 1)]
        public async Task GetUserProfileByAccountId_CorrectAccountId_ReturnsWebUserProfileModel(int profileId,
            string firstName, string surname, string dateOfBirth, int accountId)
        {
            // Arrange
            // Setting up each dependency of UserProfileService
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);

            var expectedResult = new WebUserProfileModel();

            expectedResult.Id = profileId;
            expectedResult.FirstName = firstName;
            expectedResult.Surname = surname;
            expectedResult.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
            expectedResult.UserAccountId = accountId;

            // Finally, instantiate the actual class being tested and pass in the dependencies
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);

            // Arrange
            var actualResult = await userProfileService.GetUserProfileByAccountId(accountId);

            // Act
            Assert.IsTrue
                (
                    actualResult.Id == expectedResult.Id &&
                    actualResult.UserAccountId == expectedResult.UserAccountId
                );
        }

        [DataTestMethod]
        [DataRow(11)]
        public async Task GetUserProfileByAccountId_WrongAccountId_ReturnsNull(int accountId)
        {
            // Arrange
            // Setting up each dependency of UserProfileService
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);

            // Finally, instantiate the actual class being tested and pass in the dependencies
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);

            // Arrange
            var actualResult = await userProfileService.GetUserProfileByAccountId(accountId);

            // Act
            Assert.IsNull(actualResult);
        }
        #endregion
    }
}
