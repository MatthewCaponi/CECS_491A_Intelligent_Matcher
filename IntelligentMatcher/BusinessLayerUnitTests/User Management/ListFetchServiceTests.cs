using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Services;
using static Models.UserProfileModel;

namespace BusinessLayerUnitTests.User_Management
{
    [TestClass]
    public class ListFetchServiceTests
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

        [TestMethod]
        public async Task FetchUsers_UsersExist_UsersFetched()
        {
            // Arrange
            var userRepo = new UserRepository(new DataGateway(), new ConnectionStringData());
            List<UserListTransferModel> users = null;
            // Act
            try
            {
                users = await UserProfileService.FetchUsers();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Assert.IsTrue(false);
            }

            //Assert
            for (int i = 0; i < users.Count; ++i)
            {
                if (users[i].Id != (i + 1))
                {
                    Assert.IsTrue(false);
                }

                Assert.IsTrue(true);
            }
        }

        [DataTestMethod]
        [DataRow(3)]
        public async Task FetchUserAccount_AccountExists_IdMatches(int id)
        {
            // Arrange
            UserAccountModel userAccount = new UserAccountModel();
            int actualId = 0; ;
            // Act
            try
            {
                userAccount = await UserProfileService.GetUser(id);
                actualId = userAccount.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Assert.IsTrue(false);
            }

            //Assert
            Assert.IsTrue(actualId == id);
        }

        [DataTestMethod]
        [DataRow(8)]
        public async Task FetchUserProfile_ProfileExists_IdMatches(int id)
        {
            // Arrange
            UserProfileModel userProfile = new UserProfileModel();
            int actualId = 0;
            // Act
            try
            {
                userProfile = await UserProfileService.GetUser(id);
                actualId = userProfile.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Assert.IsTrue(false);
            }

            //Assert
            Assert.IsTrue(actualId == id);
        }
    }
}
