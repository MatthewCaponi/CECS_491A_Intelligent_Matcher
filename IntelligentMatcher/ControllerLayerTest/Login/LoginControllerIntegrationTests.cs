using BusinessModels;
using ControllerModels;
using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using IntelligentMatcher.Services;
using Login;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Registration.Services;
using Security;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TestHelper;
using UserManagement.Models;
using UserManagement.Services;
using WebApi.Controllers;

namespace ControllerLayerTest.Login
{
    [TestClass]
    public class LoginControllerIntegrationTests
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

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow("TestUser1", "TestPassword1", "127.0.0.1", 5000)]
        public async Task Login_ExecuteLessThan5Seconds
            (string username, string password, string ipAddress, long expectedMaxExecutionTime)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

            _testConfigKeys.Add("Sender", "support@infinimuse.com");
            _testConfigKeys.Add("TrackOpens", "true");
            _testConfigKeys.Add("Subject", "Welcome!");
            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
            _testConfigKeys.Add("MessageStream", "outbound");
            _testConfigKeys.Add("Tag", "Welcome");
            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

            IConfiguration configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            var loginModel = new LoginModel();

            loginModel.username = username;
            loginModel.password = password;
            loginModel.ipAddress = ipAddress;

            // Initialize controller with the dependencies
            LoginController loginController = new LoginController(loginManager);

            // Act
            var timer = Stopwatch.StartNew();
            await loginController.Login(loginModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow("TestEmailAddress1", "3/28/2007 12:00:00 AM -07:00", 5000)]
        public async Task ForgotUsername_ExecuteLessThan5Seconds
            (string emailAddress, string dateOfBirth, long expectedMaxExecutionTime)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

            _testConfigKeys.Add("Sender", "support@infinimuse.com");
            _testConfigKeys.Add("TrackOpens", "true");
            _testConfigKeys.Add("Subject", "Welcome!");
            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
            _testConfigKeys.Add("MessageStream", "outbound");
            _testConfigKeys.Add("Tag", "Welcome");
            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

            IConfiguration configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            var forgotInformationModel = new ForgotInformationModel();

            forgotInformationModel.emailAddress = emailAddress;
            forgotInformationModel.dateOfBirth = dateOfBirth;

            // Initialize controller with the dependencies
            LoginController loginController = new LoginController(loginManager);

            // Act
            var timer = Stopwatch.StartNew();
            await loginController.ForgotUsername(forgotInformationModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow("TestUser1", "TestEmailAddress1", "3/28/2007 12:00:00 AM -07:00", 5000)]
        public async Task ForgotPasswordValidation_ExecuteLessThan5Seconds
            (string username, string emailAddress, string dateOfBirth, long expectedMaxExecutionTime)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

            _testConfigKeys.Add("Sender", "support@infinimuse.com");
            _testConfigKeys.Add("TrackOpens", "true");
            _testConfigKeys.Add("Subject", "Welcome!");
            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
            _testConfigKeys.Add("MessageStream", "outbound");
            _testConfigKeys.Add("Tag", "Welcome");
            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

            IConfiguration configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            var forgotInformationModel = new ForgotInformationModel();

            forgotInformationModel.username = username;
            forgotInformationModel.emailAddress = emailAddress;
            forgotInformationModel.dateOfBirth = dateOfBirth;

            // Initialize controller with the dependencies
            LoginController loginController = new LoginController(loginManager);

            // Act
            var timer = Stopwatch.StartNew();
            await loginController.ForgotPasswordValidation(forgotInformationModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow("ABC1", 1, 5000)]
        public async Task ForgotPasswordCodeInput_ExecuteLessThan5Seconds
            (string code, int accountId, long expectedMaxExecutionTime)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

            _testConfigKeys.Add("Sender", "support@infinimuse.com");
            _testConfigKeys.Add("TrackOpens", "true");
            _testConfigKeys.Add("Subject", "Welcome!");
            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
            _testConfigKeys.Add("MessageStream", "outbound");
            _testConfigKeys.Add("Tag", "Welcome");
            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

            IConfiguration configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
    (new SQLServerGateway(), new ConnectionStringData()));

            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            var forgotPasswordCodeInputModel = new ForgotPasswordCodeInputModel();

            forgotPasswordCodeInputModel.code = code;
            forgotPasswordCodeInputModel.accountId = accountId;

            // Initialize controller with the dependencies
            LoginController loginController = new LoginController(loginManager);

            // Act
            var timer = Stopwatch.StartNew();
            await loginController.ForgotPasswordCodeInput(forgotPasswordCodeInputModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow("TestPassword1", 1, 5000)]
        public async Task ResetPassword_ExecuteLessThan5Seconds
            (string password, int accountId, long expectedMaxExecutionTime)
        {
            // Arrange
            // Set the dependencies LoginManager uses
            IAuthenticationService authenticationService = new AuthenticationService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));
            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

            _testConfigKeys.Add("Sender", "support@infinimuse.com");
            _testConfigKeys.Add("TrackOpens", "true");
            _testConfigKeys.Add("Subject", "Welcome!");
            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
            _testConfigKeys.Add("MessageStream", "outbound");
            _testConfigKeys.Add("Tag", "Welcome");
            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

            IConfiguration configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())), configuration);

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(new UserAccountCodeRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ILoginManager loginManager = new LoginManager(authenticationService, cryptographyService, emailService,
                loginAttemptsService, userAccountService, userAccountCodeService, userProfileService);

            var resetPasswordModel = new ResetPasswordModel();

            resetPasswordModel.password = password;
            resetPasswordModel.accountId = accountId;

            // Initialize controller with the dependencies
            LoginController loginController = new LoginController(loginManager);

            // Act
            var timer = Stopwatch.StartNew();
            await loginController.ResetPassword(resetPasswordModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }
        #endregion
    }
}
