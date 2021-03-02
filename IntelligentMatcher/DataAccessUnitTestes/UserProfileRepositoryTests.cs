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
            var numTestRows = 10;

            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel("TestUser" + i, "TestPass" + i, "TestEmail" + i);
                userAccountModel.Id = await testRepo.InsertUserAccountTestRows(userAccountModel);

                UserProfileModel model = new UserProfileModel($"FirstName{i}", $"LastName{i}", DateTime.Now, DateTime.Today.Date,
                     UserProfileModel.AccountType.User.ToString(), UserProfileModel.AccountStatus.Active.ToString(), userAccountModel.Id);
                await testRepo.InsertUserProfileTestRows(model);

            }
        }

        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

            await testRepo.DeleteUserAccountTestRows();
            await testRepo.DeleteUserProfileTestRows();
        }

        [DataTestMethod]
        [DataRow("John014", "532jfosho", "jfosho@gmail.com", "John", "Fosho", UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Banned)]
        [DataRow("Lisa32", "Lisababy73", "llawrence@gmail.com", "Lisa", "Baby", UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Banned)]
        [DataRow("superguy234", "ilovecats", "catlover@yahoo.com", "Super", "Guy", UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Banned)]
        [DataRow("seiwla", "12345", "sei@gmail.com", "Sei", "wa", UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Banned)]
        public async Task CreateUserProfile_UserProfileDoesNotExist_ReturnsCorrectId(string username, string password, string email, string firstName, string lastName, 
        UserProfileModel.AccountType accountType, UserProfileModel.AccountStatus accountStatus)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel(username, password, email);
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            userAccountModel.Id =  await userAccount.CreateAccount(userAccountModel);
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());
            UserProfileModel profileModel = new UserProfileModel(firstName, lastName, DateTime.Now, DateTime.Today, accountType.ToString(), accountStatus.ToString(), userAccountModel.Id);

            //Act

            await userProfile.CreateUserProfile(profileModel);
            var actualAccount = await userProfile.GetUserProfileByAccountId(userAccountModel.Id);

            //Assert
            Assert.IsTrue(actualAccount.Surname == lastName);
        }

        [DataTestMethod]
        [DataRow(1, "LastName1")]
        [DataRow(2, "LastName2")]
        [DataRow(3, "LastName3")]
        [DataRow(4, "LastName4")]
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

        [DataTestMethod]
        [DataRow(1, AccountType.Admin)]
        [DataRow(3, AccountType.Admin)]
        public async Task UpdateUserAccountType(int accountId, AccountType accountType)
        {
            // Arrange
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userProfileModel = await userProfile.UpdateUserAccountType(accountId, accountType.ToString());
            var retrievedAccount = await userProfile.GetUserProfileByAccountId(accountId);
            var actualAccountType = retrievedAccount.accountType;

            // Assert
            Assert.IsTrue(accountType.ToString() == actualAccountType);
        }

        [DataTestMethod]
        [DataRow(1, AccountStatus.Suspended)]
        [DataRow(2, AccountStatus.Disabled)]
        public async Task UpdateUserAccountStatus(int accountId, AccountStatus accountStatus)
        {
            // Arrange
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userProfileModel = await userProfile.UpdateUserAccountStatus(accountId, accountStatus.ToString());
            var retrievedAccount = await userProfile.GetUserProfileByAccountId(accountId);
            var actualAccountStatus = retrievedAccount.accountStatus;

            // Assert
            Assert.IsTrue(accountStatus.ToString() == actualAccountStatus);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public async Task DeleteUserProfileById(int accountId)
        {
            // Arrange
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userProfileModel = await userProfile.DeleteUserProfile(accountId);
            var retrievedAccount = await userProfile.GetUserProfileByAccountId(accountId);

            // Assert
            Assert.IsNull(retrievedAccount);
        }
    }

    
}
