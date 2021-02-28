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
using UserManagement.Models;
using UserManagement.Services;
using static Models.UserProfileModel;

namespace BusinessLayerUnitTests.User_Management
{
    [TestClass]
    public class UserCreationServiceTests
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

                Models.UserProfileModel model = new Models.UserProfileModel($"FirstName{i}", $"LastName{i}", DateTime.Now, DateTime.Today.Date,
                     Models.UserProfileModel.AccountType.User.ToString(), Models.UserProfileModel.AccountStatus.Active.ToString(), userAccountModel.Id);
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
        [DataRow("John014", "234John", "John", "Peterson", "2-05-94", AccountType.User, AccountStatus.Suspended, "john@gmail.com")]
        public async Task CreateAccount_NoAccountExists_AccountCreated(string userName, string password, string firstName, string lastName, string dateOfBirth, 
            AccountType accountType, AccountStatus accountStatus, string email)
        {
            // Arrange
            UserManagement.Models.UserProfileModel model = new UserManagement.Models.UserProfileModel(userName, password, firstName, lastName, dateOfBirth, accountType.ToString(), accountStatus.ToString(), email, DateTime.Today);
            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());



            // Act
            await UserCreationService.CreateAccount(model);
            var userAccount = await userAccountRepo.GetAccountByUsername(userName);

            //Assert
            Assert.IsTrue(userAccount.GetType() == typeof(UserAccountModel));

        }

        [DataTestMethod]
        [DataRow("John014", "234John", "John", "Peterson", "2-05-94", AccountType.User, AccountStatus.Active, "john@gmail.com")]
        public async Task CreateAccount_NoAccountExists_UsernameCorrect(string userName, string password, string firstName, string lastName, string dateOfBirth, AccountType accountType, AccountStatus accountStatus, string email)
        {
            // Arrange
            UserManagement.Models.UserProfileModel model = new UserManagement.Models.UserProfileModel(userName, password, firstName, lastName, dateOfBirth, accountType.ToString(), accountStatus.ToString(), email, DateTime.Today);
            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
        
            // Act
            await UserCreationService.CreateAccount(model);
            var userAccount = await userAccountRepo.GetAccountByUsername(userName);
            var actualUsername = userAccount.Username;

            //Assert
            Assert.IsTrue(userName == actualUsername);
        }

        [DataTestMethod]
        [DataRow("TestUser3", "234John", "John", "Peterson", "2-05-94", AccountType.User, AccountStatus.Banned, "john@gmail.com", "Username already exists")]
        public async Task CreateAccount_UsernameExists_ExceptionMessageProper(string userName, string password, string firstName, string lastName,
            string dateOfBirth, AccountType accountType, AccountStatus accountStatus, string email, string expectedMessage)
        {
            // Arrange
            UserManagement.Models.UserProfileModel model = new UserManagement.Models.UserProfileModel(userName, password, firstName, lastName, dateOfBirth, accountType.ToString(), accountStatus.ToString(), email, DateTime.Today );
            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            try
            {
                await UserCreationService.CreateAccount(model);
                Assert.IsTrue(false);
            }
            catch(Exception e)
            {
                if (e.Message == expectedMessage)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false);
                }                  
            }             
        }

        [DataTestMethod]
        [DataRow("JohnZulousky", "234John", "John", "Peterson", "2-05-94", AccountType.User, AccountStatus.Deleted, "TestEmail7", "Email already exists")]
        public async Task CreateAccount_EmailExists_ExceptionMessageProper(string userName, string password, string firstName, string lastName,
            string dateOfBirth, AccountType accountType, AccountStatus accountStatus, string email, string expectedMessage)
        {
            // Arrange
            UserManagement.Models.UserProfileModel model = new UserManagement.Models.UserProfileModel(userName, password, firstName, lastName, dateOfBirth, accountType.ToString(), accountStatus.ToString(), email, DateTime.Today);
            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            try
            {
                await UserCreationService.CreateAccount(model);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                if (e.Message == expectedMessage)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false);
                }
            }
        }

        [DataTestMethod]
        [DataRow("John015", "234John", "John", "Peterson", "2-05-94", AccountType.User, AccountStatus.Disabled, "john33@gmail.com")]
        public async Task CreateAccount_NoAccountExists_FirstNameCorrect(string userName, string password, string firstName, string lastName, string dateOfBirth, 
            AccountType accountType, AccountStatus accountStatus, string email)
        {
            // Arrange
            UserManagement.Models.UserProfileModel model = new UserManagement.Models.UserProfileModel(userName, password, firstName, lastName, dateOfBirth, accountType.ToString(), accountStatus.ToString(), email, DateTime.Today);
            var userProfileRepo = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var id = await UserCreationService.CreateAccount(model);
            var userProfile = await userProfileRepo.GetUserProfileByAccountId(id);
            var actualFirstName = userProfile.FirstName;

            //Assert
            Assert.IsTrue(actualFirstName == firstName);
        }
    }
}
