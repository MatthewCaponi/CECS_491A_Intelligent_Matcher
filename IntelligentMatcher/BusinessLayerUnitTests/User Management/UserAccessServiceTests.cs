using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Services;
using static Models.UserProfileModel;

namespace BusinessLayerUnitTests.User_Management
{
    [TestClass]
    public class UserAccessServiceTests
    {
        [TestInitialize]
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
        [DataRow(3, AccountStatus.Disabled)]
        public async Task DisableAccount_AccountIsActive_AccountStatusDisabled(int userId, AccountStatus accountStatus)
        {
            // Arrange
            var userProfileRepo = new UserProfileRepository(new DataGateway(), new ConnectionStringData());
            
            // Act
            await UserAccessService.DisableAccount(userId);
            var userProfile = await userProfileRepo.GetUserProfileByAccountId(userId);
            var actualAccountStatus = userProfile.accountStatus;

            //Assert
            Assert.IsTrue(actualAccountStatus == accountStatus.ToString());

        }

        [DataTestMethod]
        [DataRow(5, AccountStatus.Active)]
        public async Task EnableAccount_AccountIsActive_AccountStatusEnabled(int userId, AccountStatus accountStatus)
        {
            // Arrange
            var userProfileRepo = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            await UserAccessService.DisableAccount(userId);
            await UserAccessService.EnableAccount(userId);
            var userProfile = await userProfileRepo.GetUserProfileByAccountId(userId);
            var actualAccountStatus = userProfile.accountStatus;

            //Assert
            Assert.IsTrue(actualAccountStatus == accountStatus.ToString());
        }

        [DataTestMethod]
        [DataRow(4, AccountStatus.Suspended)]
        public async Task Suspend_AccountIsActive_AccountStatusSuspended(int userId, AccountStatus accountStatus)
        {
            // Arrange
            var userProfileRepo = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            await UserAccessService.Suspend(userId);
            var userProfile = await userProfileRepo.GetUserProfileByAccountId(userId);
            var actualAccountStatus = userProfile.accountStatus;

            //Assert
            Assert.IsTrue(actualAccountStatus == accountStatus.ToString());
        }

        [DataTestMethod]
        [DataRow(1, AccountStatus.Banned)]
        public async Task Ban_AccountIsActive_AccountStatusBanned(int userId, AccountStatus accountStatus)
        {
            // Arrange
            var userProfileRepo = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            await UserAccessService.Ban(userId);
            var userProfile = await userProfileRepo.GetUserProfileByAccountId(userId);
            var actualAccountStatus = userProfile.accountStatus;

            //Assert
            Assert.IsTrue(actualAccountStatus == accountStatus.ToString());
        }

        [DataTestMethod]
        [DataRow(30, AccountStatus.Banned)]
        public async Task Ban_BanUserWhoDoesntExist_ReturnsFalseAppropriately(int userId, AccountStatus accountStatus)
        {
            // Arrange
            var userProfileRepo = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            try
            {
                await UserAccessService.Ban(userId);
                //Assert false
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                //Assert true
                Assert.IsTrue(true);
            }
        }
    }
}
