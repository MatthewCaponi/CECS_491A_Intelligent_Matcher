using DataAccess.Repositories;
using IntelligentMatcher.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class UserAccountServiceTests
    {
        #region Unit Tests GetUserAccountByUsername
        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestPassword1", "TestSalt1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task GetUserAccountByUsername_UserAccountFound_ReturnsWebUserAccountModel(int accountId,
            string username, string password, string salt, string emailAddress, string accountType,
            string accountStatus, string creationDate, string updationDate)
        {
            // Arrange
            // Setting up each dependency of UserAccountService as a Mock
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();

            var userAccount = new UserAccountModel();
            var expectedResult = new WebUserAccountModel();

            userAccount.Id = accountId;
            userAccount.Username = username;
            userAccount.Password = password;
            userAccount.Salt = salt;
            userAccount.EmailAddress = emailAddress;
            userAccount.AccountType = accountType;
            userAccount.AccountStatus = accountStatus;
            userAccount.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccount.UpdationDate = DateTimeOffset.Parse(updationDate);

            expectedResult.Id = accountId;
            expectedResult.Username = username;
            expectedResult.EmailAddress = emailAddress;
            expectedResult.AccountType = accountType;
            expectedResult.AccountStatus = accountStatus;
            expectedResult.CreationDate = DateTimeOffset.Parse(creationDate);
            expectedResult.UpdationDate = DateTimeOffset.Parse(updationDate);

            // This function reads as: If GetAccountByUsername is called, then return a UserAccountModel
            mockUserAccountRepository.Setup(x => x.GetAccountByUsername(username)).Returns(Task.FromResult(userAccount));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IUserAccountService userAccountService = new UserAccountService(mockUserAccountRepository.Object);

            // Arrange
            var actualResult = await userAccountService.GetUserAccountByUsername(username);

            // Act
            Assert.IsTrue
                (
                    actualResult.Id == expectedResult.Id &&
                    actualResult.Username == expectedResult.Username &&
                    actualResult.EmailAddress == expectedResult.EmailAddress &&
                    actualResult.AccountType == expectedResult.AccountType &&
                    actualResult.AccountStatus == expectedResult.AccountStatus &&
                    actualResult.CreationDate == expectedResult.CreationDate &&
                    actualResult.UpdationDate == expectedResult.UpdationDate
                );
        }

        [DataTestMethod]
        [DataRow("TestUser2")]
        public async Task GetUserAccountByUsername_UserAccountNotFound_ReturnsNull(string givenUsername)
        {
            // Arrange
            // Setting up each dependency of UserAccountService as a Mock
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();

            // Sets UserAccount to null
            UserAccountModel userAccount = null;

            // This function reads as: If GetAccountByUsername is called, then return a null UserAccountModel
            mockUserAccountRepository.Setup(x => x.GetAccountByUsername(givenUsername)).Returns(Task.FromResult(userAccount));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IUserAccountService userAccountService = new UserAccountService(mockUserAccountRepository.Object);

            // Arrange
            var actualResult = await userAccountService.GetUserAccountByUsername(givenUsername);

            // Act
            Assert.IsNull(actualResult);
        }
        #endregion

        #region Unit Tests GetUsernameByEmail
        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestPassword1", "TestSalt1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task GetUserAccountByEmail_UserAccountFound_ReturnsWebUserAccountModel(int accountId,
            string username, string password, string salt, string emailAddress, string accountType,
            string accountStatus, string creationDate, string updationDate)
        {
            // Arrange
            // Setting up each dependency of UserAccountService as a Mock
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();

            var userAccount = new UserAccountModel();
            var expectedResult = new WebUserAccountModel();

            userAccount.Id = accountId;
            userAccount.Username = username;
            userAccount.Password = password;
            userAccount.Salt = salt;
            userAccount.EmailAddress = emailAddress;
            userAccount.AccountType = accountType;
            userAccount.AccountStatus = accountStatus;
            userAccount.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccount.UpdationDate = DateTimeOffset.Parse(updationDate);

            expectedResult.Id = accountId;
            expectedResult.Username = username;
            expectedResult.EmailAddress = emailAddress;
            expectedResult.AccountType = accountType;
            expectedResult.AccountStatus = accountStatus;
            expectedResult.CreationDate = DateTimeOffset.Parse(creationDate);
            expectedResult.UpdationDate = DateTimeOffset.Parse(updationDate);

            // This function reads as: If GetAccountByEmail is called, then return a UserAccountModel
            mockUserAccountRepository.Setup(x => x.GetAccountByEmail(emailAddress)).Returns(Task.FromResult(userAccount));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IUserAccountService userAccountService = new UserAccountService(mockUserAccountRepository.Object);

            // Arrange
            var actualResult = await userAccountService.GetUserAccountByEmail(emailAddress);

            // Act
            Assert.IsTrue
                (
                    actualResult.Id == expectedResult.Id &&
                    actualResult.Username == expectedResult.Username &&
                    actualResult.EmailAddress == expectedResult.EmailAddress &&
                    actualResult.AccountType == expectedResult.AccountType &&
                    actualResult.AccountStatus == expectedResult.AccountStatus &&
                    actualResult.CreationDate == expectedResult.CreationDate &&
                    actualResult.UpdationDate == expectedResult.UpdationDate
                );
        }

        [DataTestMethod]
        [DataRow("TestEmailAddress2")]
        public async Task GetUserAccountByEmail_UserAccountNotFound_ReturnsNull(string givenEmail)
        {
            // Arrange
            // Setting up each dependency of UserAccountService as a Mock
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();

            // Sets UserAccount to null
            UserAccountModel userAccount = null;

            // This function reads as: If GetAccountByEmail is called, then return a null UserAccountModel
            mockUserAccountRepository.Setup(x => x.GetAccountByEmail(givenEmail)).Returns(Task.FromResult(userAccount));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IUserAccountService userAccountService = new UserAccountService(mockUserAccountRepository.Object);

            // Arrange
            var actualResult = await userAccountService.GetUserAccountByUsername(givenEmail);

            // Act
            Assert.IsNull(actualResult);
        }
        #endregion
    }
}
