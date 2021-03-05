using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;
using IntelligentMatcher.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services;
using UserManagement.Services;
using UserManagement.Models;
using IntelligentMatcher.UserManagement;
using Registration;

namespace BusinessLayerUnitTests.Registration
{
    [TestClass]
    public class RegistrationTests
    {
        // Insert test rows before every test case
        [TestInitialize]
        public async Task Init()
        {
            var numTestRows = 10;

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
                userProfileModel.FirstName = "TestName" + i;
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

        [TestMethod]
        [DataRow(21, "TestUser1", "TestPassword21", "TestSalt21", "TestEmailAddress21", "TestAccountType21", 
            "TestAccountStatus21", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task RegisterUser_UserNameExists(int expectedId, string username, string password, string salt,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
        {
            //Arrange
            WebUserAccountModel userAccountModel = new WebUserAccountModel();
            userAccountModel.Id = expectedId;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            //Act
            var result = await ValidationService.UsernameExists(userAccountModel);
            var actualAccount = await userAccountRepository.GetAccountByUsername(username);

            //Assert
            Assert.IsTrue(result);
        }

    }
}
