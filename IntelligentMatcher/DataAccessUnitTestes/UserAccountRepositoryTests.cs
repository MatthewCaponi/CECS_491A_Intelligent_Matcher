using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class UserAccountRepositoryTests
    {
        [TestInitialize()]
        public async Task Init()
        {
            var numTestRows = 20;

            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

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
        }

        [DataTestMethod]
        [DataRow(21, "TestUser21", "TestPassword11", "TestSalt11", "TestEmail21", "TestAccountType11", 
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        [DataRow(21, "TestUser22", "TestPassword12", "TestSalt12", "TestEmail22", "TestAccountType12",
            "TestAccountStatus12", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        [DataRow(21, "TestUser23", "TestPassword13", "TestSalt13", "TestEmail23", "TestAccountType13",
            "TestAccountStatus13", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task CreateAccount_UsernameAndEmailDontExist_AccountExistsId(int expectedId, string username, string password, string salt,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel();
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
            var actualId = await userAccountRepository.CreateAccount(userAccountModel);
            var actualAccount = await userAccountRepository.GetAccountById(expectedId);

            //Assert
            Assert.IsTrue(actualAccount.Id == expectedId);
        }
        
        [DataTestMethod]
        [DataRow(21, "TestUser21", "TestPassword21", "TestSalt21", "TestEmail21", "TestAccountType21",
            "TestAccountStatus21", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task CreateAccount_UsernameAndEmailDontExist_DataIsAccurate(int expectedId, string expectedUsername, 
            string expectedPassword, string expectedSalt, string expectedEmailAddress, string expectedAccountType, 
            string expectedAccountStatus, string expectedCreationDate, string expectedUpdationDate)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel();
            userAccountModel.Id = expectedId;
            userAccountModel.Username = expectedUsername;
            userAccountModel.Password = expectedPassword;
            userAccountModel.Salt = expectedSalt;
            userAccountModel.EmailAddress = expectedEmailAddress;
            userAccountModel.AccountType = expectedAccountType;
            userAccountModel.AccountStatus = expectedAccountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(expectedCreationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(expectedUpdationDate);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            //Act
            var actualId = await userAccountRepository.CreateAccount(userAccountModel);
            var actualAccount = await userAccountRepository.GetAccountById(expectedId);

            //Assert
            Assert.IsTrue
                (
                    actualAccount.Id == expectedId &&
                    actualAccount.Username == expectedUsername &&
                    actualAccount.Password == expectedPassword &&
                    actualAccount.Salt == expectedSalt &&
                    actualAccount.EmailAddress == expectedEmailAddress &&
                    actualAccount.AccountType == expectedAccountType &&
                    actualAccount.AccountStatus == expectedAccountStatus
                );
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetUserAccountById_UserAccountExists_ReturnsUserAccount(int expectedId)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountById(expectedId);
            var actualId = userAccountModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1")]
        [DataRow(2, "TestUser2")]
        [DataRow(3, "TestUser3")]
        public async Task GetUserAccountById_UserAccountExists_UsernameCorrect(int id, string expectedUsername)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountById(id);
            var actualUsername = userAccountModel.Username;

            // Assert
            Assert.IsTrue(actualUsername == expectedUsername);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1")]
        [DataRow(2, "TestUser2")]
        [DataRow(3, "TestUser3")]
        public async Task GetAccountByUsername_UserAccountExists_ReturnsUserAccount(int expectedId, string username)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountByUsername(username);
            var actualId = userAccountModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow("TestUser1")]
        [DataRow("TestUser2")]
        [DataRow("TestUser3")]
        public async Task GetAccountByUsername_UserAccountExists_UsernameCorrect(string expectedUserName)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountByUsername(expectedUserName);
            var actualUsername = userAccountModel.Username;

            // Assert
            Assert.IsTrue(actualUsername == expectedUserName);
        }


        [DataTestMethod]
        [DataRow(1, "TestEmailAddress1")]
        [DataRow(2, "TestEmailAddress2")]
        [DataRow(3, "TestEmailAddress3")]
        public async Task GetAccountByEmail_AccountExists_ReturnsAccount(int expectedId, string emailAddress)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountByEmail(emailAddress);
            var actualId = userAccountModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow("TestEmailAddress1")]
        [DataRow("TestEmailAddress2")]
        [DataRow("TestEmailAddress3")]
        public async Task GetAccountByEmail_AccountExists_EmailIsCorrect(string expectedEmailAddress)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            UserAccountModel userAccountModel = await userAccount.GetAccountByEmail(expectedEmailAddress);
            var actualEmail = userAccountModel.EmailAddress;

            // Assert
            Assert.IsTrue(actualEmail == expectedEmailAddress);
        }

        [DataTestMethod]
        [DataRow(1, "TestPass1")]
        [DataRow(2, "TestPass2")]
        [DataRow(3, "TestPass3")]
        public async Task UpdateAccountPassword(int id, string expectedPassword)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.UpdateAccountPassword(id, expectedPassword);
            var retrievedAccount = await userAccount.GetAccountById(id);
            var actualPassword = retrievedAccount.Password;

            // Assert
            Assert.IsTrue(expectedPassword == actualPassword);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1")]
        [DataRow(2, "TestUser2")]
        [DataRow(3, "TestUser3")]
        public async Task UpdateAccountUsername(int id, string expectedUsername)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.UpdateAccountUsername(id, expectedUsername);
            var retrievedAccount = await userAccount.GetAccountById(id);
            var actualUsername = retrievedAccount.Username;

            // Assert
            Assert.IsTrue(expectedUsername == actualUsername);
        }

        [DataTestMethod]
        [DataRow(1, "TestEmailAddressUpdate1")]
        [DataRow(2, "TestEmailAddressUpdate2")]
        [DataRow(3, "TestEmailAddressUpdate3")]
        public async Task UpdateAccountEmail(int id, string expectedEmail)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.UpdateAccountEmail(id, expectedEmail);
            var retrievedAccount = await userAccount.GetAccountById(id);
            var actualEmail = retrievedAccount.EmailAddress;

            // Assert
            Assert.IsTrue(expectedEmail == actualEmail);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task DeleteUserAccountById(int id)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.DeleteAccountById(id);
            var retrievedAccount = await userAccount.GetAccountById(id);

            // Assert
            Assert.IsNull(retrievedAccount);
        }



    }
}
