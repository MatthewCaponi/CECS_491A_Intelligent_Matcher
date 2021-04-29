using BusinessModels;
using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using IntelligentMatcher.Services;
using Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Registration.Services;
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
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);
            IUserAccountCodeRepository userAccountCodeRepository = new UserAccountCodeRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);

            for (int i = 1; i <= numTestRows; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel();
                userAccountModel.Id = i;
                userAccountModel.Username = "TestUser" + i;
                userAccountModel.Password = "" + i;
                userAccountModel.Salt = "" + i;
                userAccountModel.EmailAddress = "TestEmailAddress" + i;
                userAccountModel.AccountType = "TestAccountType" + i;
                userAccountModel.AccountStatus = "TestAccountStatus" + i;
                userAccountModel.CreationDate = DateTimeOffset.Parse("3/28/2007 7:13:50 PM +00:00");
                userAccountModel.UpdationDate = DateTimeOffset.Parse("3/28/2007 7:13:50 PM +00:00");

                await userAccountRepository.CreateAccount(userAccountModel);
                await cryptographyService.newPasswordEncryptAsync("TestPassword" + i, i);

                UserProfileModel userProfileModel = new UserProfileModel();
                userProfileModel.Id = i;
                userProfileModel.FirstName = "TestFirstName" + i;
                userProfileModel.Surname = "TestSurname" + i;
                userProfileModel.DateOfBirth = DateTimeOffset.Parse("3/28/2007 7:13:50 PM +00:00");
                userProfileModel.UserAccountId = userAccountModel.Id;

                await userProfileRepository.CreateUserProfile(userProfileModel);

                LoginAttemptsModel loginAttemptsModel = new LoginAttemptsModel();
                loginAttemptsModel.Id = i;
                loginAttemptsModel.IpAddress = "127.0.0." + i;
                loginAttemptsModel.LoginCounter = i;
                loginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse("3/28/2014 7:13:50 PM +00:00").AddYears(i);

                await loginAttemptsRepository.CreateLoginAttempts(loginAttemptsModel);

                UserAccountCodeModel userAccountCodeModel = new UserAccountCodeModel();
                userAccountCodeModel.Id = i;
                userAccountCodeModel.Code = "ABC" + i;
                userAccountCodeModel.ExpirationTime = DateTimeOffset.Parse("3/28/2014 7:13:50 PM +00:00").AddYears(i);
                userAccountCodeModel.UserAccountId = i;

                await userAccountCodeRepository.CreateUserAccountCode(userAccountCodeModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);

            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }

            var loginAttempts = await loginAttemptsRepository.GetAllLoginAttempts();

            foreach (var loginAttempt in loginAttempts)
            {
                await loginAttemptsRepository.DeleteLoginAttemptsById(loginAttempt.Id);
            }

            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserProfile", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("LoginAttempts", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserAccountCode", 0, connectionString, dataGateway);
        }
        #endregion

        #region Integration Tests Login
        [DataTestMethod]
        [DataRow("TestUser1", "TestPassword1", "127.0.0.10", ErrorMessage.TooManyAttempts)]
        public async Task Login_TooManyLoginAttemptsBeforeSuspensionEndTime_ReturnTooManyAttempts(string username,
            string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("TestUser11", "TestPassword1", "127.0.0.1", ErrorMessage.NoMatch)]
        public async Task Login_IpAddressExistsButUsernameDoesntExist_ReturnNoMatch
            (string username, string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
            (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
            (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()))); ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            var oldBusinessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var newBusinessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage &&
                newBusinessLoginAttemptsModel.LoginCounter == (oldBusinessLoginAttemptsModel.LoginCounter + 1));
        }

        [DataTestMethod]
        [DataRow("TestUser11", "TestPassword1", "127.0.0.4", ErrorMessage.NoMatch)]
        public async Task Login_IpAddressExistsButUsernameDoesntExistAndLoginCounterIsFive_ReturnNoMatch
            (string username, string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
            (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
            (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            var oldBusinessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var newBusinessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage &&
                newBusinessLoginAttemptsModel.LoginCounter == (oldBusinessLoginAttemptsModel.LoginCounter + 1) &&
                newBusinessLoginAttemptsModel.SuspensionEndTime != oldBusinessLoginAttemptsModel.SuspensionEndTime);
        }

        [DataTestMethod]
        [DataRow("TestUser11", "TestPassword1", "127.0.0.5", ErrorMessage.NoMatch)]
        public async Task Login_TooManyLoginAttemptsAfterSuspensionEndTimeButUsernameDoesntExist_ReturnNoMatch
            (string username, string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var businessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage &&
                businessLoginAttemptsModel.LoginCounter == 1);
        }

        [DataTestMethod]
        [DataRow("TestUser11", "TestPassword1", "127.0.0.11", ErrorMessage.NoMatch)]
        public async Task Login_IpAddressDoesntExistAndUsernameDoesntExist_ReturnNoMatch
            (string username, string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var businessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage &&
                businessLoginAttemptsModel.LoginCounter == 1);
        }

        [DataTestMethod]
        [DataRow("TestUser1", "TestPassword11", "127.0.0.1", ErrorMessage.NoMatch)]
        public async Task Login_IpAddressAndUsernameExistsButPasswordDoesntMatch_ReturnNoMatch
            (string username, string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            var oldBusinessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var newBusinessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage &&
                newBusinessLoginAttemptsModel.LoginCounter == (oldBusinessLoginAttemptsModel.LoginCounter + 1));
        }

        [DataTestMethod]
        [DataRow("TestUser1", "TestPassword11", "127.0.0.4", ErrorMessage.NoMatch)]
        public async Task Login_IpAddressAndUsernameExistsButPasswordDoesntMatchAdnLoginCounterIsFive_ReturnNoMatch
            (string username, string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            var oldBusinessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var newBusinessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage &&
                newBusinessLoginAttemptsModel.LoginCounter == (oldBusinessLoginAttemptsModel.LoginCounter + 1) &&
                newBusinessLoginAttemptsModel.SuspensionEndTime != oldBusinessLoginAttemptsModel.SuspensionEndTime);
        }

        [DataTestMethod]
        [DataRow("TestUser1", "TestPassword11", "127.0.0.5", ErrorMessage.NoMatch)]
        public async Task Login_TooManyLoginAttemptsAfterSuspensionEndTimeButPasswordDoesntMatch_ReturnNoMatch
            (string username, string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var businessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage &&
                businessLoginAttemptsModel.LoginCounter == 1);
        }

        [DataTestMethod]
        [DataRow("TestUser1", "TestPassword11", "127.0.0.11", ErrorMessage.NoMatch)]
        public async Task Login_IpAddressDoesntExistAndPasswordDoesntMatch_ReturnNoMatch
            (string username, string password, string ipAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var businessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage &&
                businessLoginAttemptsModel.LoginCounter == 1);
        }

        [DataTestMethod]
        [DataRow("TestUser1", "TestPassword1", "127.0.0.1")]
        public async Task Login_LoginSuccess_ReturnWebUserAccountModel
            (string username, string password, string ipAddress)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = true;
            expectedResult.SuccessValue = await userAccountService.GetUserAccountByUsername(username);

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.Login(username, password, ipAddress);
            var businessLoginAttemptsModel = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue.Id == expectedResult.SuccessValue.Id &&
                actualResult.SuccessValue.Username == expectedResult.SuccessValue.Username &&
                businessLoginAttemptsModel.LoginCounter == 0);
        }
        #endregion

        #region Integration Tests ForgotUsername
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestEmailAddress11", ErrorMessage.NoMatch)]
        public async Task ForgotUsername_WrongEmail_ReturnNoMatch(string dateOfBirth,
            string emailAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(), 
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<string>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotUsername(emailAddress, DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2008 7:13:50 PM +00:00", "TestEmailAddress1", ErrorMessage.NoMatch)]
        public async Task ForgotUsername_WrongDateOfBirth_ReturnUserNoMatch(string dateOfBirth,
            string emailAddress, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<string>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotUsername(emailAddress, DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 12:00:00 AM -07:00")]
        public async Task ForgotUsername_InfoMatch_ReturnUsername(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate,
            string updationDate, string dateOfBirth)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
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

            var expectedResult = new Result<string>();
            expectedResult.Success = true;
            expectedResult.SuccessValue = username;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotUsername(emailAddress, DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue == expectedResult.SuccessValue);
        }
        #endregion

        #region Integration Tests ForgotPasswordValidation
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "TestEmailAddress11", "TestUser11", ErrorMessage.NoMatch)]
        public async Task ForgotPasswordValidation_WrongUsername_ReturnNoMatch(string dateOfBirth,
            string emailAddress, string username, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
            (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
            (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()))); 
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

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
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

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
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

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
        public async Task ForgotPasswordValidation_InfoMatch_ReturnBusinessUserAccountCodeModel(int accountId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate,
            string updationDate, string dateOfBirth)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
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
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordValidation(username, emailAddress,
                DateTimeOffset.Parse(dateOfBirth));

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue.Id == expectedResult.SuccessValue.Id);
        }
        #endregion

        #region Integration Tests ForgotPasswordCodeInput
        [DataTestMethod]
        [DataRow("ABC11", 11, ErrorMessage.Null)]
        public async Task ForgotPasswordCodeInput_AccountHasNoCode_ReturnNull(string code, int accountId, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordCodeInput(code, accountId);

            // Assert
            Assert.IsTrue
                (
                    actualResult.Success == expectedResult.Success &&
                    actualResult.ErrorMessage == expectedResult.ErrorMessage
                );
        }

        [DataTestMethod]
        [DataRow("ABC1", 1, ErrorMessage.CodeExpired)]
        public async Task ForgotPasswordCodeInput_TimeExpired_ReturnCodeExpired(string code, int accountId, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordCodeInput(code, accountId);

            // Assert
            Assert.IsTrue
                (
                    actualResult.Success == expectedResult.Success &&
                    actualResult.ErrorMessage == expectedResult.ErrorMessage
                );
        }

        [DataTestMethod]
        [DataRow("ABC11", 10, ErrorMessage.NoMatch)]
        public async Task ForgotPasswordCodeInput_CodeMisMatch_ReturnNoMatch(string code, int accountId, ErrorMessage error)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            // Initialize manager with the dependencies
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordCodeInput(code, accountId);

            // Assert
            Assert.IsTrue
                (
                    actualResult.Success == expectedResult.Success &&
                    actualResult.ErrorMessage == expectedResult.ErrorMessage
                );
        }

        [DataTestMethod]
        [DataRow("ABC10", 10, "TestUser10", "TestEmailAddress10", "TestAccountType10",
            "TestAccountStatus10", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task ForgotPasswordCodeInput_InputMatch_ReturnWebUserAccountModel(string code, int accountId,
            string username, string emailAddress, string accountType, string accountStatus,
            string creationDate, string updationDate)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
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
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            // Act
            var actualResult = await loginManager.ForgotPasswordCodeInput(code, accountId);

            // Assert
            Assert.IsTrue
                (
                    actualResult.Success == expectedResult.Success &&
                    actualResult.SuccessValue.Id == expectedResult.SuccessValue.Id
                );
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
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
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
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

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
