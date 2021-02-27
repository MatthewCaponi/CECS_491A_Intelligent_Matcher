using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Threading.Tasks;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class UserAccountRepositoryTests
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
                UserAccountModel model = new UserAccountModel("TestUser" + i, "TestPass" + i, "TestEmail" + i);
                await testRepo.InsertUserAccountTestRows(model);
            }
        }

        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

            await testRepo.DeleteUserAccountTestRows();
        }


        [DataTestMethod]
        [DataRow("John014", "532jfosho", "jfosho@gmail.com")]
        [DataRow("Lisa32", "Lisababy73", "llawrence@gmail.com")]
        [DataRow("superguy234", "ilovecats", "catlover@yahoo.com")]
        [DataRow("seiwla", "12345", "sei@gmail.com")]
        public async Task CreateUserAccount_UserAccountDoesNotExist_ReturnsCorrectId(string username, string password, string email)
        {
            //Arrange
            UserAccountModel model = new UserAccountModel(username, password, email);
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            //Act
            await userAccount.CreateAccount(model);
            var actualAccount = await userAccount.GetAccountByUsername(username);

            //Assert
            Assert.IsTrue(actualAccount.Username == username);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        [DataRow(9)]
        [DataRow(10)]
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
        [DataRow(4, "TestUser4")]
        [DataRow(5, "TestUser5")]
        [DataRow(6, "TestUser6")]
        [DataRow(7, "TestUser7")]
        [DataRow(8, "TestUser8")]
        [DataRow(9, "TestUser9")]
        [DataRow(10, "TestUser10")]
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
        [DataRow(4, "TestUser4")]
        [DataRow(5, "TestUser5")]
        [DataRow(6, "TestUser6")]
        [DataRow(7, "TestUser7")]
        [DataRow(8, "TestUser8")]
        [DataRow(9, "TestUser9")]
        [DataRow(10, "TestUser10")]
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
        [DataRow("TestUser4")]
        [DataRow("TestUser5")]
        [DataRow("TestUser6")]
        [DataRow("TestUser7")]
        [DataRow("TestUser8")]
        [DataRow("TestUser9")]
        [DataRow("TestUser10")]
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
        [DataRow(1, "TestEmail1")]
        [DataRow(2, "TestEmail2")]
        [DataRow(3, "TestEmail3")]
        [DataRow(4, "TestEmail4")]
        [DataRow(5, "TestEmail5")]
        [DataRow(6, "TestEmail6")]
        [DataRow(7, "TestEmail7")]
        [DataRow(8, "TestEmail8")]
        [DataRow(9, "TestEmail9")]
        [DataRow(10, "TestEmail10")]
        public async Task GetAccountByEmail_AccountExists_ReturnsAccount(int expectedId, string email)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountByEmail(email);
            var actualId = userAccountModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow("TestEmail1")]
        [DataRow("TestEmail2")]
        [DataRow("TestEmail3")]
        [DataRow("TestEmail4")]
        [DataRow("TestEmail5")]
        [DataRow("TestEmail6")]
        [DataRow("TestEmail7")]
        [DataRow("TestEmail8")]
        [DataRow("TestEmail9")]
        [DataRow("TestEmail10")]
        public async Task GetAccountByEmail_AccountExists_EmailIsCorrect(string expectedEmail)
        {
            // Arrange
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

            // Act
            var userAccountModel = await userAccount.GetAccountByEmail(expectedEmail);
            var actualEmail = userAccountModel.EmailAddress;
           
            // Assert
            Assert.IsTrue(actualEmail == expectedEmail); 
        }

        [DataTestMethod]
        [DataRow(1, "TestPass1")]
        [DataRow(2, "TestPass2")]
        [DataRow(3, "TestPass3")]
        [DataRow(4, "TestPass4")]
        [DataRow(5, "TestPass5")]
        [DataRow(6, "TestPass6")]
        [DataRow(7, "TestPass7")]
        [DataRow(8, "TestPass8")]
        [DataRow(9, "TestPass9")]
        [DataRow(10, "TestPass10")]
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
        [DataRow(4, "TestUser4")]
        [DataRow(5, "TestUser5")]
        [DataRow(6, "TestUser6")]
        [DataRow(7, "TestUser7")]
        [DataRow(8, "TestUser8")]
        [DataRow(9, "TestUser9")]
        [DataRow(10, "TestUser10")]
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
        [DataRow(1, "TestEmail1")]
        [DataRow(2, "TestEmail2")]
        [DataRow(3, "TestEmail3")]
        [DataRow(4, "TestEmail4")]
        [DataRow(5, "TestEmail5")]
        [DataRow(6, "TestEmail6")]
        [DataRow(7, "TestEmail7")]
        [DataRow(8, "TestEmail8")]
        [DataRow(9, "TestEmail9")]
        [DataRow(10, "TestEmail10")]
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
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        [DataRow(9)]
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
