using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.User_Management
{
    [TestClass]
    public class UserUpdateServicetests
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
        [DataRow(3, "UserUpdate3")]
        public async Task UpdateUsername_UserExists_UsernameUpdateAccurate(int userId, string newUsername)
        {
            // Arrange
            var userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            await userAccountRepo.UpdateAccountUsername(userId, newUsername);
            var modifiedAccount = await userAccountRepo.GetAccountById(userId);
            var actualUsername = modifiedAccount.Username;

            //Assert
            Assert.IsTrue(newUsername == actualUsername);
        }

        [DataTestMethod]
        [DataRow(4, "PasswordUpdate4")]
        public async Task UpdatePassword_UserExists_PasswordUpdatedAccurately(int userId, string newPassword)
        {
            // Arrange
            var userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            await userAccountRepo.UpdateAccountPassword(userId, newPassword);
            var modifiedAccount = await userAccountRepo.GetAccountById(userId);
            var actualPassword = modifiedAccount.Password;

            //Assert
            Assert.IsTrue(actualPassword == newPassword);
        }

        [DataTestMethod]
        [DataRow(4, "EmailUpdate4")]
        public async Task UpdateEmail_UserExists_EmailUpdatedAccurately(int userId, string newEmail)
        {
            // Arrange
            var userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            await userAccountRepo.UpdateAccountEmail(userId, newEmail);
            var modifiedAccount = await userAccountRepo.GetAccountById(userId);
            var actualEmail = modifiedAccount.EmailAddress;

            //Assert
            Assert.IsTrue(actualEmail == newEmail);
        }
    }
}
