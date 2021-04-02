﻿using BusinessModels;
using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using IntelligentMatcher.Services;
using Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Security;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;
using UserManagement.Models;
using UserManagement.Services;

namespace BusinessLayerUnitTests.Login
{
    [TestClass]
    public class LoginIntegrationTests
    {
        #region Test Setup
        // Insert test rows before every test case
        [TestInitialize]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 10;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);

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
                userAccountModel.CreationDate = DateTimeOffset.Parse("3/28/2007 7:13:50 PM +00:00");
                userAccountModel.UpdationDate = DateTimeOffset.Parse("3/28/2007 7:13:50 PM +00:00");
                await userAccountRepository.CreateAccount(userAccountModel);

                UserProfileModel userProfileModel = new UserProfileModel();
                userProfileModel.Id = i;
                userProfileModel.FirstName = "TestFirstName" + i;
                userProfileModel.Surname = "TestSurname" + i;
                userProfileModel.DateOfBirth = DateTimeOffset.Parse("3/28/2007 7:13:50 PM +00:00");
                userProfileModel.UserAccountId = userAccountModel.Id;
                await userProfileRepository.CreateUserProfile(userProfileModel);

            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserProfile", 0, connectionString, dataGateway);
        }
        #endregion

        #region Integration Tests ForgotUsernameValidation
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestEmailAddress11", ErrorMessage.UserDoesNotExist)]
        public async Task ForgotUsernameValidation_WrongEmail_ReturnUserDoesNotExist(string dateOfBirth,
            string emailAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(), 
                new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService,
                userAccountService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotUsernameValidation(emailAddress, DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2008 7:13:50 PM +00:00", "TestEmailAddress1", ErrorMessage.NoMatch)]
        public async Task ForgotUsernameValidation_WrongDateOfBirth_ReturnUserNoMatch(string dateOfBirth,
            string emailAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService,
                userAccountService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotUsernameValidation(emailAddress, DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 12:00:00 AM -07:00")]
        public async Task ForgotUsernameValidation_InfoMatch_ReturnWebUserAccountModel(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate,
            string updationDate, string dateOfBirth)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

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

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService,
                userAccountService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotUsernameValidation(emailAddress, DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue.Id == expectedResult.SuccessValue.Id &&
                actualResult.SuccessValue.EmailAddress == expectedResult.SuccessValue.EmailAddress);
        }
        #endregion

        #region Integration Tests ForgotPasswordValidation
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestEmailAddress11", "TestUser11", ErrorMessage.UserDoesNotExist)]
        public async Task ForgotPasswordValidation_WrongUsername_ReturnUserDoesNotExist(string dateOfBirth,
            string emailAddress, string username, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService,
                userAccountService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress, 
                DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestEmailAddress11", "TestUser1", ErrorMessage.NoMatch)]
        public async Task ForgotPasswordValidation_WrongEmail_ReturnNoMatch(string dateOfBirth,
            string emailAddress, string username, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService,
                userAccountService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress,
                DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestEmailAddress1", "TestUser1", ErrorMessage.NoMatch)]
        public async Task ForgotPasswordValidation_WrongDateOfBirth_ReturnNoMatch(string dateOfBirth,
            string emailAddress, string username, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService,
                userAccountService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress,
                DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 12:00:00 AM -07:00")]
        public async Task ForgotPasswordValidation_InfoMatch_ReturnWebUserAccountModel(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate,
            string updationDate, string dateOfBirth)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

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

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService,
                userAccountService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress,
                DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue.Id == expectedResult.SuccessValue.Id &&
                actualResult.SuccessValue.Username == expectedResult.SuccessValue.Username &&
                actualResult.SuccessValue.EmailAddress == expectedResult.SuccessValue.EmailAddress);
        }
        #endregion

        #region Integration Tests ResetPassword
        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestPassword11", "TestSalt11", "TestEmailAddress1", "TestAccountType1",
            "TestAccountStatus1", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task ResetPassword_NewSaltAndPassword_ReturnWebUserAccountModel(int accountId, string username,
            string password, string salt, string emailAddress, string accountType, string accountStatus,
            string creationDate, string updationDate)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            // Get set repository for a test
            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData());

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

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService,
                userAccountService, userProfileService);

            // Act
            var actualResult = await loginManager.ResetPassword(password, accountId);

            // Get account model to test
            var accountModel = await userAccountRepository.GetAccountById(accountId);

            // Assert
            Assert.IsTrue(password != accountModel.Password &&
                salt != accountModel.Salt &&
                actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue.Id == expectedResult.SuccessValue.Id);
        }
        #endregion
    }
}
