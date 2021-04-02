﻿using BusinessModels;
using IntelligentMatcher.Services;
using Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;

namespace BusinessLayerUnitTests.Login
{
    [TestClass]
    public class LoginTests
    {
        #region Unit Tests ForgotUsernameValidation
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestEmailAddress2", ErrorMessage.UserDoesNotExist)]
        public async Task ForgotUsernameValidation_UserAccountNotFound_ReturnUserDoesNotExist(string dateOfBirth,
            string givenEmail, ErrorMessage error)
        {
            // Arrange
            // Set mock objects for the dependencies LoginManager uses
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            WebUserAccountModel webUserAccountModel = null;

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Set conditional for the used mock object
            mockUserAccountService.Setup(x => x.GetUserAccountByEmail(givenEmail))
                .Returns(Task.FromResult(webUserAccountModel));

            // Initialize manager with the mock objects
            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object);

            // Act
            var actualResult = await loginManager.ForgotUsernameValidation(givenEmail, DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
            "TestSurname1", "3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoMatch)]
        public async Task ForgotUsernameValidation_DateOfBirthNoMatch_ReturnNoMatch(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            int profileId, string firstName, string surname, string dateOfBirth, string givenDateOfBirth, ErrorMessage error)
        {
            // Arrange
            // Set mock objects for the dependencies LoginManager uses
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

            webUserAccountModel.Id = accountId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

            webUserProfileModel.Id = profileId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = surname;
            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Set conditional for the used mock objects
            mockUserAccountService.Setup(x => x.GetUserAccountByEmail(emailAddress))
                .Returns(Task.FromResult(webUserAccountModel));
            mockUserProfileService.Setup(x => x.GetUserProfileByAccountId(accountId))
                .Returns(Task.FromResult(webUserProfileModel));

            // Initialize manager with the mock objects
            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object);

            // Act
            var actualResult = await loginManager.ForgotUsernameValidation(emailAddress, DateTimeOffset.Parse(givenDateOfBirth));

            // Assert
            Assert.IsTrue(DateTimeOffset.Parse(givenDateOfBirth) != webUserProfileModel.DateOfBirth &&
                actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
            "TestSurname1", "3/28/2007 7:13:50 PM +00:00")]
        public async Task ForgotUsernameValidation_Success_ReturnWebUserAccountModel(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            int profileId, string firstName, string surname, string dateOfBirth)
        {
            // Arrange
            // Set mock objects for the dependencies LoginManager uses
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

            webUserAccountModel.Id = accountId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

            webUserProfileModel.Id = profileId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = surname;
            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = true;
            expectedResult.SuccessValue = webUserAccountModel;

            // Set conditional for the used mock objects
            mockUserAccountService.Setup(x => x.GetUserAccountByEmail(emailAddress))
                .Returns(Task.FromResult(webUserAccountModel));
            mockUserProfileService.Setup(x => x.GetUserProfileByAccountId(accountId))
                .Returns(Task.FromResult(webUserProfileModel));

            // Initialize manager with the mock objects
            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object);

            // Act
            var actualResult = await loginManager.ForgotUsernameValidation(emailAddress, DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(DateTimeOffset.Parse(dateOfBirth) == webUserProfileModel.DateOfBirth &&
                actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue == expectedResult.SuccessValue);
        }
        #endregion

        #region Unit Tests ForgotPasswordValidation
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestUser2", "TestEmailAddress2", ErrorMessage.UserDoesNotExist)]
        public async Task ForgotPasswordValidation_UserAccountNotFound_ReturnUserDoesNotExist(string dateOfBirth, 
            string givenUsername, string givenEmail, ErrorMessage error)
        {
            // Arrange
            // Set mock objects for the dependencies LoginManager uses
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            WebUserAccountModel webUserAccountModel = null;

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Set conditional for the used mock object
            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(givenUsername))
                .Returns(Task.FromResult(webUserAccountModel));

            // Initialize manager with the mock objects
            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(givenUsername, givenEmail,
                DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
            "TestSurname1", "3/28/2007 7:13:50 PM +00:00", "TestEmailAddress2", ErrorMessage.NoMatch)]
        public async Task ForgotPasswordValidation_EmailNoMatch_ReturnNoMatch(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            int profileId, string firstName, string surname, string dateOfBirth, string givenEmail, ErrorMessage error)
        {
            // Arrange
            // Set mock objects for the dependencies LoginManager uses
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

            webUserAccountModel.Id = accountId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

            webUserProfileModel.Id = profileId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = surname;
            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Set conditional for the used mock object
            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
                .Returns(Task.FromResult(webUserAccountModel));

            // Initialize manager with the mock objects
            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(username, givenEmail,
                DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(givenEmail != webUserAccountModel.EmailAddress &&
                actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
            "TestSurname1", "3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoMatch)]
        public async Task ForgotPasswordValidation_DateOfBirthNoMatch_ReturnNoMatch(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            int profileId, string firstName, string surname, string dateOfBirth, string givenDateOfBirth, ErrorMessage error)
        {
            // Arrange
            // Set mock objects for the dependencies LoginManager uses
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

            webUserAccountModel.Id = accountId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

            webUserProfileModel.Id = profileId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = surname;
            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Set conditional for the used mock object
            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
                .Returns(Task.FromResult(webUserAccountModel));
            mockUserProfileService.Setup(x => x.GetUserProfileByAccountId(accountId))
                .Returns(Task.FromResult(webUserProfileModel));

            // Initialize manager with the mock objects
            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress,
                DateTimeOffset.Parse(givenDateOfBirth));

            // Assert
            Assert.IsTrue(emailAddress == webUserAccountModel.EmailAddress &&
                DateTimeOffset.Parse(givenDateOfBirth) != webUserProfileModel.DateOfBirth &&
                actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
            "TestSurname1", "3/28/2007 7:13:50 PM +00:00")]
        public async Task ForgotPasswordValidation_Success_ReturnWebUserAccountModel(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            int profileId, string firstName, string surname, string dateOfBirth)
        {
            // Arrange
            // Set mock objects for the dependencies LoginManager uses
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

            webUserAccountModel.Id = accountId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

            webUserProfileModel.Id = profileId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = surname;
            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = true;
            expectedResult.SuccessValue = webUserAccountModel;

            // Set conditional for the used mock object
            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
                .Returns(Task.FromResult(webUserAccountModel));
            mockUserProfileService.Setup(x => x.GetUserProfileByAccountId(accountId))
                .Returns(Task.FromResult(webUserProfileModel));

            // Initialize manager with the mock objects
            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress,
                DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(emailAddress == webUserAccountModel.EmailAddress &&
                DateTimeOffset.Parse(dateOfBirth) == webUserProfileModel.DateOfBirth &&
                actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue == expectedResult.SuccessValue);
        }
        #endregion

        #region Unit Tests ResetPassword
        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestPassword1", "TestSalt1", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task ResetPassword_Success_ReturnWebUserAccountModel(int accountId, string username, string password,
            string salt, string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
        {
            // Arrange
            // Set mock objects for the dependencies LoginManager uses
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            UserAccountModel userAccountModel = new UserAccountModel();

            userAccountModel.Id = accountId;
            userAccountModel.Username = username;
            userAccountModel.Password = password;
            userAccountModel.Salt = salt;
            userAccountModel.EmailAddress = emailAddress;
            userAccountModel.AccountType = accountType;
            userAccountModel.AccountStatus = accountStatus;
            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

            webUserAccountModel.Id = accountId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = true;
            expectedResult.SuccessValue = webUserAccountModel;

            // Set conditional for the used mock object
            mockCryptographyService.Setup(x => x.newPasswordEncryptAsync(password, accountId))
                .Returns(Task.FromResult(true));
            mockUserAccountService.Setup(x => x.GetUserAccount(accountId))
                .Returns(Task.FromResult(webUserAccountModel));

            // Initialize manager with the mock objects
            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object);

            // Act
            var actualResult = await loginManager.ResetPassword(password, accountId);

            // Assert
            Assert.IsTrue(password == userAccountModel.Password &&
                actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue == expectedResult.SuccessValue);
        }
        #endregion
    }
}