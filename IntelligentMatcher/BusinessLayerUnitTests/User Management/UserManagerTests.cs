using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UserManagement;
using UserManagement.Models;
using static Models.UserProfileModel;

namespace BusinessLayerUnitTests.User_Management
{
    [TestClass]
    public class UserManagerTests
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
            ITestRepo testRepo = new TestRepo(dataGateway,
                                              connectionString);

            await testRepo.DeleteUserAccountTestRows();
            await testRepo.DeleteUserProfileTestRows();
        }
        
        [DataTestMethod]
        [DataRow("John014", "234John", "John", "Peterson", "2-05-94", AccountType.User, "john@gmail.com")]
        public async Task CreateUser_UserDoesNotExist_UserCreated(string userName, string password, string firstName, string lastName, string dateOfBirth, AccountType accountType, string email)
        {
            //Arrange
            UserCreateModel model = new UserCreateModel(userName, password, firstName, lastName, dateOfBirth, accountType, email);
            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            UserManager userManager = new UserManager();
            string actualUsername = null;

            //Act
            try
            {
                await userManager.CreateUser(model);
                var userAccount = await userAccountRepo.GetAccountByUsername(userName);
                actualUsername = userAccount.Username;
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }

            //Assert
            Assert.IsTrue(userName == actualUsername);
        }

        [DataTestMethod]
        [DataRow(3)]
        public async Task DeleteUser_UserExists_ReturnTrue(int userId)
        {
            // Arrange
            var userManager = new UserManager();

            // Act
           var deleted = await userManager.DeleteUser(userId);

            //Assert
            Assert.IsTrue(deleted);
        }

        [DataTestMethod]
        [DataRow(15)]
        public async Task DeleteUser_UserDoesNotExist_ReturnFalse(int userId)
        {
            // Arrange
            var userManager = new UserManager();

            // Act
            var deleted = await userManager.DeleteUser(userId);

            //Assert
            Assert.IsTrue(deleted == false);
        }
    }
}
