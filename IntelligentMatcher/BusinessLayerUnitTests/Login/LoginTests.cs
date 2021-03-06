﻿//using BusinessModels;
//using IntelligentMatcher.Services;
//using Login;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Models;
//using Moq;
//using Security;
//using Services;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using UserManagement.Models;
//using UserManagement.Services;

//namespace BusinessLayerUnitTests.Login
//{
//    [TestClass]
//    public class LoginTests
//    {
//        #region Unit Tests Login
//        [DataTestMethod]
//        [DataRow("TestUser1", "TestPassword1", 1, "127.0.0.1", 5, "3/28/2027 7:13:50 PM +00:00", ErrorMessage.TooManyAttempts)]
//        public async Task Login_TooManyAttemptsTaken_ReturnTooManyAttempts(string username, string password,
//            int loginAttemptsId, string ipAddress, int loginCounter, string suspensionEndTime, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            var businessLoginAttemptsModel = new BusinessLoginAttemptsModel();

//            businessLoginAttemptsModel.Id = loginAttemptsId;
//            businessLoginAttemptsModel.IpAddress = ipAddress;
//            businessLoginAttemptsModel.LoginCounter = loginCounter;
//            businessLoginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndTime);

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockLoginAttemptsService.Setup(x => x.GetLoginAttemptsByIpAddress(ipAddress))
//                .Returns(Task.FromResult(businessLoginAttemptsModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.Login(username, password, ipAddress);

//            // Assert
//            Assert.IsTrue(actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow("TestUser1", "TestPassword1", 1, "127.0.0.1", 0, "3/28/2007 7:13:50 PM +00:00", ErrorMessage.NoMatch)]
//        [DataRow("TestUser1", "TestPassword1", 1, "127.0.0.1", 5, "3/28/2007 7:13:50 PM +00:00", ErrorMessage.NoMatch)]
//        public async Task Login_UsernameDoesntExistWithLoginAttempts_ReturnNoMatch(string username, string password,
//            int loginAttemptsId, string ipAddress, int loginCounter, string suspensionEndTime, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            var businessLoginAttemptsModel = new BusinessLoginAttemptsModel();

//            businessLoginAttemptsModel.Id = loginAttemptsId;
//            businessLoginAttemptsModel.IpAddress = ipAddress;
//            businessLoginAttemptsModel.LoginCounter = loginCounter;
//            businessLoginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndTime);

//            WebUserAccountModel webUserAccountModel = null;

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockLoginAttemptsService.Setup(x => x.GetLoginAttemptsByIpAddress(ipAddress))
//                .Returns(Task.FromResult(businessLoginAttemptsModel));
//            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
//                .Returns(Task.FromResult(webUserAccountModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.Login(username, password, ipAddress);

//            // Assert
//            Assert.IsTrue(actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestPassword1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
//            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "127.0.0.1", 0,
//            "3/28/2007 7:13:50 PM +00:00", ErrorMessage.NoMatch)]
//        [DataRow(1, "TestUser1", "TestPassword1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
//            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "127.0.0.1", 5,
//            "3/28/2007 7:13:50 PM +00:00", ErrorMessage.NoMatch)]
//        public async Task Login_UsernameExistsButPasswordDoesntMatch_ReturnNoMatch(int accountId, string username, 
//            string password, string emailAddress, string accountType, string accountStatus, string creationDate,
//            string updationDate, int loginAttemptsId, string ipAddress, int loginCounter, string suspensionEndTime,
//            ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            var businessLoginAttemptsModel = new BusinessLoginAttemptsModel();

//            businessLoginAttemptsModel.Id = loginAttemptsId;
//            businessLoginAttemptsModel.IpAddress = ipAddress;
//            businessLoginAttemptsModel.LoginCounter = loginCounter;
//            businessLoginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndTime);

//            var webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockLoginAttemptsService.Setup(x => x.GetLoginAttemptsByIpAddress(ipAddress))
//                .Returns(Task.FromResult(businessLoginAttemptsModel));
//            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
//                .Returns(Task.FromResult(webUserAccountModel));
//            mockAuthenticationService.Setup(x => x.AuthenticatePasswordWithUsename(password, username))
//                .Returns(Task.FromResult(false));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.Login(username, password, ipAddress);

//            // Assert
//            Assert.IsTrue(actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestPassword1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
//            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "127.0.0.1", 0,
//            "3/28/2007 7:13:50 PM +00:00")]
//        [DataRow(1, "TestUser1", "TestPassword1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
//            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "127.0.0.1", 5,
//            "3/28/2007 7:13:50 PM +00:00")]
//        public async Task Login_LoginSuccess_ReturnWebUserAccountModel(int accountId, string username,
//            string password, string emailAddress, string accountType, string accountStatus, string creationDate,
//            string updationDate, int loginAttemptsId, string ipAddress, int loginCounter, string suspensionEndTime)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            var businessLoginAttemptsModel = new BusinessLoginAttemptsModel();

//            businessLoginAttemptsModel.Id = loginAttemptsId;
//            businessLoginAttemptsModel.IpAddress = ipAddress;
//            businessLoginAttemptsModel.LoginCounter = loginCounter;
//            businessLoginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndTime);

//            var webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = webUserAccountModel;

//            // Set conditional for the used mock object
//            mockLoginAttemptsService.Setup(x => x.GetLoginAttemptsByIpAddress(ipAddress))
//                .Returns(Task.FromResult(businessLoginAttemptsModel));
//            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
//                .Returns(Task.FromResult(webUserAccountModel));
//            mockAuthenticationService.Setup(x => x.AuthenticatePasswordWithUsename(password, username))
//                .Returns(Task.FromResult(true));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.Login(username, password, ipAddress);

//            // Assert
//            Assert.IsTrue(actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.SuccessValue == expectedResult.SuccessValue);
//        }
//        #endregion

//        #region Unit Tests ForgotUsername
//        [DataTestMethod]
//        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestEmailAddress2", ErrorMessage.NoMatch)]
//        public async Task ForgotUsername_UserAccountNotFound_ReturnNoMatch(string dateOfBirth,
//            string givenEmail, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            WebUserAccountModel webUserAccountModel = null;

//            var expectedResult = new Result<string>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockUserAccountService.Setup(x => x.GetUserAccountByEmail(givenEmail))
//                .Returns(Task.FromResult(webUserAccountModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotUsername(givenEmail, DateTimeOffset.Parse(dateOfBirth));

//            // Assert
//            Assert.IsTrue(actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
//            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
//            "TestSurname1", "3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoMatch)]
//        public async Task ForgotUsername_DateOfBirthNoMatch_ReturnNoMatch(int accountId, string username,
//            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
//            int profileId, string firstName, string surname, string dateOfBirth, string givenDateOfBirth, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

//            webUserProfileModel.Id = profileId;
//            webUserProfileModel.FirstName = firstName;
//            webUserProfileModel.Surname = surname;
//            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
//            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

//            var expectedResult = new Result<string>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock objects
//            mockUserAccountService.Setup(x => x.GetUserAccountByEmail(emailAddress))
//                .Returns(Task.FromResult(webUserAccountModel));
//            mockUserProfileService.Setup(x => x.GetUserProfileByAccountId(accountId))
//                .Returns(Task.FromResult(webUserProfileModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotUsername(emailAddress, DateTimeOffset.Parse(givenDateOfBirth));

//            // Assert
//            Assert.IsTrue(DateTimeOffset.Parse(givenDateOfBirth) != webUserProfileModel.DateOfBirth &&
//                actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
//            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
//            "TestSurname1", "3/28/2007 7:13:50 PM +00:00")]
//        public async Task ForgotUsername_Success_ReturnUsername(int accountId, string username,
//            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
//            int profileId, string firstName, string surname, string dateOfBirth)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

//            webUserProfileModel.Id = profileId;
//            webUserProfileModel.FirstName = firstName;
//            webUserProfileModel.Surname = surname;
//            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
//            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

//            var expectedResult = new Result<string>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = username;

//            // Set conditional for the used mock objects
//            mockUserAccountService.Setup(x => x.GetUserAccountByEmail(emailAddress))
//                .Returns(Task.FromResult(webUserAccountModel));
//            mockUserProfileService.Setup(x => x.GetUserProfileByAccountId(accountId))
//                .Returns(Task.FromResult(webUserProfileModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotUsername(emailAddress, DateTimeOffset.Parse(dateOfBirth));

//            // Assert
//            Assert.IsTrue(DateTimeOffset.Parse(dateOfBirth) == webUserProfileModel.DateOfBirth &&
//                actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.SuccessValue == expectedResult.SuccessValue);
//        }
//        #endregion

//        #region Unit Tests ForgotPasswordValidation
//        [DataTestMethod]
//        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestUser2", "TestEmailAddress2", ErrorMessage.NoMatch)]
//        public async Task ForgotPasswordValidation_UserAccountNotFound_ReturnNoMatch(string dateOfBirth, 
//            string givenUsername, string givenEmail, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            WebUserAccountModel webUserAccountModel = null;

//            var expectedResult = new Result<BusinessUserAccountCodeModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(givenUsername))
//                .Returns(Task.FromResult(webUserAccountModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotPasswordValidation(givenUsername, givenEmail,
//                DateTimeOffset.Parse(dateOfBirth));

//            // Assert
//            Assert.IsTrue(actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
//            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
//            "TestSurname1", "3/28/2007 7:13:50 PM +00:00", "TestEmailAddress2", ErrorMessage.NoMatch)]
//        public async Task ForgotPasswordValidation_EmailNoMatch_ReturnNoMatch(int accountId, string username,
//            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
//            int profileId, string firstName, string surname, string dateOfBirth, string givenEmail, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

//            webUserProfileModel.Id = profileId;
//            webUserProfileModel.FirstName = firstName;
//            webUserProfileModel.Surname = surname;
//            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
//            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

//            var expectedResult = new Result<BusinessUserAccountCodeModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
//                .Returns(Task.FromResult(webUserAccountModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotPasswordValidation(username, givenEmail,
//                DateTimeOffset.Parse(dateOfBirth));

//            // Assert
//            Assert.IsTrue(givenEmail != webUserAccountModel.EmailAddress &&
//                actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
//            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
//            "TestSurname1", "3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoMatch)]
//        public async Task ForgotPasswordValidation_DateOfBirthNoMatch_ReturnNoMatch(int accountId, string username,
//            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
//            int profileId, string firstName, string surname, string dateOfBirth, string givenDateOfBirth, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

//            webUserProfileModel.Id = profileId;
//            webUserProfileModel.FirstName = firstName;
//            webUserProfileModel.Surname = surname;
//            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
//            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

//            var expectedResult = new Result<BusinessUserAccountCodeModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
//                .Returns(Task.FromResult(webUserAccountModel));
//            mockUserProfileService.Setup(x => x.GetUserProfileByAccountId(accountId))
//                .Returns(Task.FromResult(webUserProfileModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress,
//                DateTimeOffset.Parse(givenDateOfBirth));

//            // Assert
//            Assert.IsTrue(emailAddress == webUserAccountModel.EmailAddress &&
//                DateTimeOffset.Parse(givenDateOfBirth) != webUserProfileModel.DateOfBirth &&
//                actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1",
//            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", 1, "TestFirstName1",
//            "TestSurname1", "3/28/2007 7:13:50 PM +00:00")]
//        public async Task ForgotPasswordValidation_Success_ReturnBusinessUserAccountCodeModel(int accountId, string username,
//            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
//            int profileId, string firstName, string surname, string dateOfBirth)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();

//            webUserProfileModel.Id = profileId;
//            webUserProfileModel.FirstName = firstName;
//            webUserProfileModel.Surname = surname;
//            webUserProfileModel.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
//            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

//            BusinessUserAccountCodeModel businessUserAccountCodeModel = new BusinessUserAccountCodeModel();

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = webUserAccountModel;

//            // Set conditional for the used mock object
//            mockUserAccountService.Setup(x => x.GetUserAccountByUsername(username))
//                .Returns(Task.FromResult(webUserAccountModel));
//            mockUserProfileService.Setup(x => x.GetUserProfileByAccountId(accountId))
//                .Returns(Task.FromResult(webUserProfileModel));
//            mockUserAccountCodeService.Setup(x => x.GetUserAccountCodeByAccountId(accountId))
//                .Returns(Task.FromResult(businessUserAccountCodeModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress,
//                DateTimeOffset.Parse(dateOfBirth));

//            // Assert
//            Assert.IsTrue(emailAddress == webUserAccountModel.EmailAddress &&
//                DateTimeOffset.Parse(dateOfBirth) == webUserProfileModel.DateOfBirth &&
//                actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.SuccessValue == expectedResult.SuccessValue);
//        }
//        #endregion

//        #region Unit Tests ForgotPasswordCodeInput
//        [DataTestMethod]
//        [DataRow("ABC1", 1, ErrorMessage.Null)]
//        public async Task ForgotPasswordCodeInput_CodeDoesntExist_ReturnNull(string code, int accountId, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            BusinessUserAccountCodeModel businessUserAccountCodeModel = null;

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockUserAccountCodeService.Setup(x => x.GetUserAccountCodeByAccountId(accountId))
//                .Returns(Task.FromResult(businessUserAccountCodeModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotPasswordCodeInput(code, accountId);

//            // Assert
//            Assert.IsTrue
//                (
//                    actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                    actualResult.ErrorMessage == expectedResult.ErrorMessage
//                );
//        }

//        [DataTestMethod]
//        [DataRow(1, "ABC1", "3/28/2007 7:13:50 PM +00:00", 1, ErrorMessage.CodeExpired)]
//        public async Task ForgotPasswordCodeInput_TimeExpired_ReturnCodeExpired(int codeId, string code,
//            string expirationTime, int accountId, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            var businessUserAccountCodeModel = new BusinessUserAccountCodeModel();

//            businessUserAccountCodeModel.Id = codeId;
//            businessUserAccountCodeModel.Code = code;
//            businessUserAccountCodeModel.ExpirationTime = DateTimeOffset.Parse(expirationTime);
//            businessUserAccountCodeModel.UserAccountId = accountId;

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockUserAccountCodeService.Setup(x => x.GetUserAccountCodeByAccountId(accountId))
//                .Returns(Task.FromResult(businessUserAccountCodeModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotPasswordCodeInput(code, accountId);

//            // Assert
//            Assert.IsTrue
//                (
//                    actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                    actualResult.ErrorMessage == expectedResult.ErrorMessage
//                );
//        }

//        [DataTestMethod]
//        [DataRow(1, "ABC1", "3/28/2027 7:13:50 PM +00:00", 1, "ABC2", ErrorMessage.NoMatch)]
//        public async Task ForgotPasswordCodeInput_CodeDoesntMatch_ReturnNoMatch(int codeId, string code,
//            string expirationTime, int accountId, string givenCode, ErrorMessage error)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            var businessUserAccountCodeModel = new BusinessUserAccountCodeModel();

//            businessUserAccountCodeModel.Id = codeId;
//            businessUserAccountCodeModel.Code = code;
//            businessUserAccountCodeModel.ExpirationTime = DateTimeOffset.Parse(expirationTime);
//            businessUserAccountCodeModel.UserAccountId = accountId;

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = false;
//            expectedResult.ErrorMessage = error;

//            // Set conditional for the used mock object
//            mockUserAccountCodeService.Setup(x => x.GetUserAccountCodeByAccountId(accountId))
//                .Returns(Task.FromResult(businessUserAccountCodeModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotPasswordCodeInput(givenCode, accountId);

//            // Assert
//            Assert.IsTrue
//                (
//                    actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                    actualResult.ErrorMessage == expectedResult.ErrorMessage
//                );
//        }

//        [DataTestMethod]
//        [DataRow(1, "ABC1", "3/28/2027 7:13:50 PM +00:00", 1, "TestUser1", "TestEmailAddress1",
//            "TestAccountType1", "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
//        public async Task ForgotPasswordCodeInput_Success_ReturnWebUserAccountModel(int codeId, string code,
//            string expirationTime, int accountId, string username, string emailAddress,
//            string accountType, string accountStatus, string creationDate, string updationDate)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            var businessUserAccountCodeModel = new BusinessUserAccountCodeModel();

//            businessUserAccountCodeModel.Id = codeId;
//            businessUserAccountCodeModel.Code = code;
//            businessUserAccountCodeModel.ExpirationTime = DateTimeOffset.Parse(expirationTime);
//            businessUserAccountCodeModel.UserAccountId = accountId;

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = webUserAccountModel;

//            // Set conditional for the used mock object
//            mockUserAccountCodeService.Setup(x => x.GetUserAccountCodeByAccountId(accountId))
//                .Returns(Task.FromResult(businessUserAccountCodeModel));
//            mockUserAccountService.Setup(x => x.GetUserAccount(accountId))
//                .Returns(Task.FromResult(webUserAccountModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ForgotPasswordCodeInput(code, accountId);

//            // Assert
//            Assert.IsTrue
//                (
//                    code == businessUserAccountCodeModel.Code &&
//                    actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                    actualResult.SuccessValue == expectedResult.SuccessValue
//                );
//        }
//        #endregion

//        #region Unit Tests ResetPassword
//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestPassword1", "TestSalt1", "TestEmailAddress1", "TestAccountType1",
//            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
//        public async Task ResetPassword_Success_ReturnWebUserAccountModel(int accountId, string username, string password,
//            string salt, string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
//        {
//            // Arrange
//            // Set mock objects for the dependencies LoginManager uses
//            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
//            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
//            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
//            Mock<ILoginAttemptsService> mockLoginAttemptsService = new Mock<ILoginAttemptsService>();
//            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
//            Mock<IUserAccountCodeService> mockUserAccountCodeService = new Mock<IUserAccountCodeService>();
//            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

//            UserAccountModel userAccountModel = new UserAccountModel();

//            userAccountModel.Id = accountId;
//            userAccountModel.Username = username;
//            userAccountModel.Password = password;
//            userAccountModel.Salt = salt;
//            userAccountModel.EmailAddress = emailAddress;
//            userAccountModel.AccountType = accountType;
//            userAccountModel.AccountStatus = accountStatus;
//            userAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            userAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = webUserAccountModel;

//            // Set conditional for the used mock object
//            mockCryptographyService.Setup(x => x.newPasswordEncryptAsync(password, accountId))
//                .Returns(Task.FromResult(true));
//            mockUserAccountService.Setup(x => x.GetUserAccount(accountId))
//                .Returns(Task.FromResult(webUserAccountModel));

//            // Initialize manager with the mock objects
//            ILoginManager loginManager = new LoginManager(mockAuthenticationService.Object, mockCryptographyService.Object,
//                mockEmailService.Object, mockLoginAttemptsService.Object, mockUserAccountService.Object,
//                mockUserAccountCodeService.Object, mockUserProfileService.Object);

//            // Act
//            var actualResult = await loginManager.ResetPassword(password, accountId);

//            // Assert
//            Assert.IsTrue(password == userAccountModel.Password &&
//                actualResult.WasSuccessful == expectedResult.WasSuccessful &&
//                actualResult.SuccessValue == expectedResult.SuccessValue);
//        }
//        #endregion
//    }
//}
