using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using static Models.UserProfileModel;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class UserProfileRepositoryTests
    {
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

        [DataTestMethod]
        [DataRow("TestFirstName21", "TestSurname21", 21)]
        public async Task CreateUserProfile_UserProfileDoesNotExist_ReturnsCorrectId(string firstName, string surname,
            int id)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            IUserProfileRepository userProfileRepository = new UserProfileRepository(new DataGateway(), new ConnectionStringData());
            UserProfileModel userProfileModel = new UserProfileModel();
            userProfileModel.FirstName = firstName;
            userProfileModel.Surname = surname;
            userProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            userProfileModel.UserAccountId = id;

            //Act

            await userProfileModel.CreateUserProfile(userProfileModel);
            var actualAccount = await userProfile.GetUserProfileByAccountId(userAccountModel.Id);

            //Assert
            Assert.IsTrue(actualAccount.Surname == lastName);
        }

        [DataTestMethod]
        [DataRow(1, "TestSurname1")]
        [DataRow(2, "TestSurname2")]
        [DataRow(3, "TestSurname3")]
        [DataRow(4, "TestSurname4")]
        public async Task GetUserProfileByAccountId_UseProfileExists_ReturnCorrectUsername(int accountId, string expectedLastName)
        {
            // Arrange
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());



            // Act
            var userProfileModel = await userProfile.GetUserProfileByAccountId(accountId);
            var actualLastName = userProfileModel.Surname;

            // Assert
            Assert.IsTrue(actualLastName == expectedLastName);

        }

        //    [DataTestMethod]
        //    [DataRow(1, AccountType.Admin)]
        //    [DataRow(3, AccountType.Admin)]
        //    public async Task UpdateUserAccountType(int accountId, AccountType accountType)
        //    {
        //        // Arrange
        //        IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

        //        // Act
        //        var userProfileModel = await userProfile.UpdateUserAccountType(accountId, accountType.ToString());
        //        var retrievedAccount = await userProfile.GetUserProfileByAccountId(accountId);
        //        var actualAccountType = retrievedAccount.accountType;

        //        // Assert
        //        Assert.IsTrue(accountType.ToString() == actualAccountType);
        //    }

        //    [DataTestMethod]
        //    [DataRow(1, AccountStatus.Suspended)]
        //    [DataRow(2, AccountStatus.Disabled)]
        //    public async Task UpdateUserAccountStatus(int accountId, AccountStatus accountStatus)
        //    {
        //        // Arrange
        //        IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

        //        // Act
        //        var userProfileModel = await userProfile.UpdateUserAccountStatus(accountId, accountStatus.ToString());
        //        var retrievedAccount = await userProfile.GetUserProfileByAccountId(accountId);
        //        var actualAccountStatus = retrievedAccount.accountStatus;

        //        // Assert
        //        Assert.IsTrue(accountStatus.ToString() == actualAccountStatus);
        //    }

        //    [DataTestMethod]
        //    [DataRow(1)]
        //    [DataRow(2)]
        //    [DataRow(3)]
        //    [DataRow(4)]
        //    public async Task DeleteUserProfileById(int accountId)
        //    {
        //        // Arrange
        //        IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

        //        // Act
        //        var userProfileModel = await userProfile.DeleteUserProfile(accountId);
        //        var retrievedAccount = await userProfile.GetUserProfileByAccountId(accountId);

        //        // Assert
        //        Assert.IsNull(retrievedAccount);
        //    }
    }


}
