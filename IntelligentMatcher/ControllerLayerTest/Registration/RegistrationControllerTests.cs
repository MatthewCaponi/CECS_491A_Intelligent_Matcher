﻿//using BusinessModels;
//using ControllerModels;
//using ControllerModels.RegistrationModels;
//using DataAccess;
//using DataAccess.Repositories;
//using DataAccessUnitTestes;
//using IntelligentMatcher.Services;
//using Microsoft.Extensions.Configuration;
//using Logging;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Models;
//using Moq;
//using Registration;
//using Registration.Services;
//using Security;
//using Services;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Text;
//using System.Threading.Tasks;
//using TestHelper;
//using UserManagement.Models;
//using UserManagement.Services;
//using WebApi.Controllers;

//namespace ControllerLayerTest.Registration
//{
//    [TestClass]
//    public class RegistrationControllerTests
//    {
//        #region Test Setup
//        // Insert test rows before every test case
//        [TestInitialize]
//        public async Task Init()
//        {
//            await TestCleaner.CleanDatabase();
//            var numTestRows = 10;

//            IDataGateway dataGateway = new SQLServerGateway();
//            IConnectionStringData connectionString = new ConnectionStringData();
//            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
//            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);

//            for (int i = 1; i <= numTestRows; ++i)
//            {
//                UserAccountModel userAccountModel = new UserAccountModel();
//                userAccountModel.Id = i;
//                userAccountModel.Username = "TestUser" + i;
//                userAccountModel.Password = "TestPassword" + i;
//                userAccountModel.Salt = "TestSalt" + i;
//                userAccountModel.EmailAddress = "TestEmailAddress" + i;
//                userAccountModel.AccountType = "TestAccountType" + i;
//                userAccountModel.AccountStatus = "TestAccountStatus" + i;
//                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
//                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;
//                await userAccountRepository.CreateAccount(userAccountModel);

//                UserProfileModel userProfileModel = new UserProfileModel();
//                userProfileModel.Id = i;
//                userProfileModel.FirstName = "TestFirstName" + i;
//                userProfileModel.Surname = "TestSurname" + i;
//                userProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
//                userProfileModel.UserAccountId = userAccountModel.Id;
//                await userProfileRepository.CreateUserProfile(userProfileModel);
//            }
//        }

//        // Remove test rows and reset id counter after every test case
//        [TestCleanup()]
//        public async Task CleanUp()
//        {
//            IDataGateway dataGateway = new SQLServerGateway();
//            IConnectionStringData connectionString = new ConnectionStringData();
//            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
//            var accounts = await userAccountRepository.GetAllAccounts();

//            foreach (var account in accounts)
//            {
//                await userAccountRepository.DeleteAccountById(account.Id);
//            }
//            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
//            await DataAccessTestHelper.ReseedAsync("UserProfile", 0, connectionString, dataGateway);
//        }
//        #endregion

//        #region Integration Tests
//        [DataTestMethod]
//        [DataRow("TestFirstName11", "TestSurname11", "TestPassword11", "matt@infinimuse.com",
//            "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", ErrorMessage.Null)]
//        public async Task RegisterUser_UsernameIsNull_ResultsAreAccurate(string firstName, string surname,
//            string password, string emailAddress, string dateOfBirth, string ipAddress, ErrorMessage error)
//        {
//            // Arrange
//            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//            _testConfigKeys.Add("Sender", "support@infinimuse.com");
//            _testConfigKeys.Add("TrackOpens", "true");
//            _testConfigKeys.Add("Subject", "Welcome!");
//            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//            _testConfigKeys.Add("MessageStream", "outbound");
//            _testConfigKeys.Add("Tag", "Welcome");
//            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//            IConfiguration configuration = new ConfigurationBuilder()
//                            .AddInMemoryCollection(_testConfigKeys)
//                            .Build();
//            EmailService emailService = new EmailService(new UserAccountRepository
//             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//                 (new SQLServerGateway(), new ConnectionStringData())), configuration);

//            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//                new ConnectionStringData()));

//            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

//            IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//                userProfileService, validationService, cryptographyService, new LogService());

//            var registrationModel = new RegistrationModel();

//            registrationModel.firstName = firstName;
//            registrationModel.surname = surname;
//            registrationModel.username = null;
//            registrationModel.password = password;
//            registrationModel.emailAddress = emailAddress;
//            registrationModel.dateOfBirth = dateOfBirth;
//            registrationModel.ipAddress = ipAddress;

//            RegistrationResultModel expectedResult = new RegistrationResultModel();
//            expectedResult.Success = false;
//            expectedResult.ErrorMessage = error.ToString();

//            RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//            // Act
//            var actualResult = await registrationController.RegisterUser(registrationModel);

//            // Assert
//            Assert.IsTrue(actualResult.Success == expectedResult.Success);
//            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        [DataTestMethod]
//        [DataRow("TestFirstName11", "TestSurname11", "TestUser11", "matt@infinimuse.com",
//            "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", ErrorMessage.Null)]
//        public async Task RegisterUser_PasswordIsNull_ResultsAreAccurate(string firstName, string surname,
//            string username, string emailAddress, string dateOfBirth, string ipAddress, ErrorMessage error)
//        {
//            // Arrange
//            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//            _testConfigKeys.Add("Sender", "support@infinimuse.com");
//            _testConfigKeys.Add("TrackOpens", "true");
//            _testConfigKeys.Add("Subject", "Welcome!");
//            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//            _testConfigKeys.Add("MessageStream", "outbound");
//            _testConfigKeys.Add("Tag", "Welcome");
//            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//            IConfiguration configuration = new ConfigurationBuilder()
//                            .AddInMemoryCollection(_testConfigKeys)
//                            .Build();
//            EmailService emailService = new EmailService(new UserAccountRepository
//             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
//            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//                new ConnectionStringData()));

//            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

//            IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//                userProfileService, validationService, cryptographyService, new LogService());

//            var registrationModel = new RegistrationModel();

//            registrationModel.firstName = firstName;
//            registrationModel.surname = surname;
//            registrationModel.username = username;
//            registrationModel.password = null;
//            registrationModel.emailAddress = emailAddress;
//            registrationModel.dateOfBirth = dateOfBirth;
//            registrationModel.ipAddress = ipAddress;

//            RegistrationResultModel expectedResult = new RegistrationResultModel();
//            expectedResult.Success = false;
//            expectedResult.ErrorMessage = error.ToString();

//            RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//            // Act
//            var actualResult = await registrationController.RegisterUser(registrationModel);

//            // Assert
//            Assert.IsTrue(actualResult.Success == expectedResult.Success);
//            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        }

//        //[DataTestMethod]
//        //[DataRow("TestFirstName11", "TestSurname11", "TestUser11", "Test1", "matt@infinimuse.com",
//        //    "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", ErrorMessage.InvalidPassword)]
//        //public async Task RegisterUser_PasswordIsNotLongEnough_ResultsAreAccurate(string firstName, string surname, string username,
//        //    string password, string emailAddress, string dateOfBirth, string ipAddress, ErrorMessage error)
//        //{
//        //    // Arrange
//        //    IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//        //    _testConfigKeys.Add("Sender", "support@infinimuse.com");
//        //    _testConfigKeys.Add("TrackOpens", "true");
//        //    _testConfigKeys.Add("Subject", "Welcome!");
//        //    _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//        //    _testConfigKeys.Add("MessageStream", "outbound");
//        //    _testConfigKeys.Add("Tag", "Welcome");
//        //    _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//        //    IConfiguration configuration = new ConfigurationBuilder()
//        //                    .AddInMemoryCollection(_testConfigKeys)
//        //                    .Build();
//        //    EmailService emailService = new EmailService(new UserAccountRepository
//        //     (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//        //     (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//        //         (new SQLServerGateway(), new ConnectionStringData())), configuration);



//        //    IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//        //        (new SQLServerGateway(), new ConnectionStringData()));
//        //    IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//        //        (new SQLServerGateway(), new ConnectionStringData()));
//        //    IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//        //    ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//        //        new ConnectionStringData()));

//        //    IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//        //    IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

//        //    IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//        //        userProfileService, validationService, cryptographyService, new LogService());
          
//        //    var registrationModel = new RegistrationModel();

//        //    registrationModel.firstName = firstName;
//        //    registrationModel.surname = surname;
//        //    registrationModel.username = username;
//        //    registrationModel.password = password;
//        //    registrationModel.emailAddress = emailAddress;
//        //    registrationModel.dateOfBirth = dateOfBirth;
//        //    registrationModel.ipAddress = ipAddress;

//        //    RegistrationResultModel expectedResult = new RegistrationResultModel();
//        //    expectedResult.Success = false;
//        //    expectedResult.ErrorMessage = error.ToString();

//        //    RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//        //    // Act
//        //    var actualResult = await registrationController.RegisterUser(registrationModel);

//        //    // Assert
//        //    Assert.IsTrue(actualResult.Success == expectedResult.Success);
//        //    Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        //}

//        //[DataTestMethod]
//        //[DataRow("TestFirstName11", "TestSurname11", "TestUser11", "TestPassword", "matt@infinimuse.com",
//        //    "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", ErrorMessage.InvalidPassword)]
//        //public async Task RegisterUser_PasswordHasNoNumbers_ResultsAreAccurate(string firstName, string surname, string username,
//        //    string password, string emailAddress, string dateOfBirth, string ipAddress, ErrorMessage error)
//        //{
//        //    // Arrange
//        //    IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//        //    _testConfigKeys.Add("Sender", "support@infinimuse.com");
//        //    _testConfigKeys.Add("TrackOpens", "true");
//        //    _testConfigKeys.Add("Subject", "Welcome!");
//        //    _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//        //    _testConfigKeys.Add("MessageStream", "outbound");
//        //    _testConfigKeys.Add("Tag", "Welcome");
//        //    _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//        //    IConfiguration configuration = new ConfigurationBuilder()
//        //                    .AddInMemoryCollection(_testConfigKeys)
//        //                    .Build();
//        //    EmailService emailService = new EmailService(new UserAccountRepository
//        //     (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//        //     (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//        //         (new SQLServerGateway(), new ConnectionStringData())), configuration);




//        //    IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//        //        (new SQLServerGateway(), new ConnectionStringData()));
//        //    IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//        //        (new SQLServerGateway(), new ConnectionStringData()));
//        //    IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//        //    ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//        //        new ConnectionStringData()));

//        //    IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//        //    IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

//        //    IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//        //        userProfileService, validationService, cryptographyService, new LogService());

//        //    var registrationModel = new RegistrationModel();

//        //    registrationModel.firstName = firstName;
//        //    registrationModel.surname = surname;
//        //    registrationModel.username = username;
//        //    registrationModel.password = password;
//        //    registrationModel.emailAddress = emailAddress;
//        //    registrationModel.dateOfBirth = dateOfBirth;
//        //    registrationModel.ipAddress = ipAddress;

//        //    RegistrationResultModel expectedResult = new RegistrationResultModel();
//        //    expectedResult.Success = false;
//        //    expectedResult.ErrorMessage = error.ToString();

//        //    RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//        //    // Act
//        //    var actualResult = await registrationController.RegisterUser(registrationModel);

//        //    // Assert
//        //    Assert.IsTrue(actualResult.Success == expectedResult.Success);
//        //    Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        //}

//        //[DataTestMethod]
//        //[DataRow("TestFirstName11", "TestSurname11", "TestUser11", "testpassword1", "matt@infinimuse.com",
//        //    "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", ErrorMessage.InvalidPassword)]
//        //public async Task RegisterUser_PasswordIsHasNoUpper_ResultsAreAccurate(string firstName, string surname, string username,
//        //    string password, string emailAddress, string dateOfBirth, string ipAddress, ErrorMessage error)
//        //{
//        //    // Arrange
//        //    IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//        //    _testConfigKeys.Add("Sender", "support@infinimuse.com");
//        //    _testConfigKeys.Add("TrackOpens", "true");
//        //    _testConfigKeys.Add("Subject", "Welcome!");
//        //    _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//        //    _testConfigKeys.Add("MessageStream", "outbound");
//        //    _testConfigKeys.Add("Tag", "Welcome");
//        //    _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//        //    IConfiguration configuration = new ConfigurationBuilder()
//        //                    .AddInMemoryCollection(_testConfigKeys)
//        //                    .Build();
//        //    EmailService emailService = new EmailService(new UserAccountRepository
//        //     (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//        //     (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//        //         (new SQLServerGateway(), new ConnectionStringData())), configuration);
//        //    IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//        //        (new SQLServerGateway(), new ConnectionStringData()));
//        //    IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//        //        (new SQLServerGateway(), new ConnectionStringData()));
//        //    IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//        //    ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//        //        new ConnectionStringData()));


//        //    IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//        //    IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());
          
//        //    IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//        //        userProfileService, validationService, cryptographyService, new LogService());

//        //    var registrationModel = new RegistrationModel();

//        //    registrationModel.firstName = firstName;
//        //    registrationModel.surname = surname;
//        //    registrationModel.username = username;
//        //    registrationModel.password = password;
//        //    registrationModel.emailAddress = emailAddress;
//        //    registrationModel.dateOfBirth = dateOfBirth;
//        //    registrationModel.ipAddress = ipAddress;

//        //    RegistrationResultModel expectedResult = new RegistrationResultModel();
//        //    expectedResult.Success = false;
//        //    expectedResult.ErrorMessage = error.ToString();

//        //    RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//        //    // Act
//        //    var actualResult = await registrationController.RegisterUser(registrationModel);

//        //    // Assert
//        //    Assert.IsTrue(actualResult.Success == expectedResult.Success);
//        //    Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        //}

//        //[DataTestMethod]
//        //[DataRow("TestFirstName11", "TestSurname11", "TestUser11", "TESTPASSWORD1", "matt@infinimuse.com",
//        //    "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", ErrorMessage.InvalidPassword)]
//        //public async Task RegisterUser_PasswordHasNoLower_ResultsAreAccurate(string firstName, string surname, string username,
//        //    string password, string emailAddress, string dateOfBirth, string ipAddress, ErrorMessage error)
//        //{
//        //    // Arrange
//        //    IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//        //    _testConfigKeys.Add("Sender", "support@infinimuse.com");
//        //    _testConfigKeys.Add("TrackOpens", "true");
//        //    _testConfigKeys.Add("Subject", "Welcome!");
//        //    _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//        //    _testConfigKeys.Add("MessageStream", "outbound");
//        //    _testConfigKeys.Add("Tag", "Welcome");
//        //    _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//        //    IConfiguration configuration = new ConfigurationBuilder()
//        //                    .AddInMemoryCollection(_testConfigKeys)
//        //                    .Build();
//        //    EmailService emailService = new EmailService(new UserAccountRepository
//        //     (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//        //     (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//        //         (new SQLServerGateway(), new ConnectionStringData())), configuration);
//        //    IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//        //        (new SQLServerGateway(), new ConnectionStringData()));
//        //    IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//        //        (new SQLServerGateway(), new ConnectionStringData()));
//        //    IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//        //    ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//        //        new ConnectionStringData()));

//        //    IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//        //    IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

//        //    IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//        //        userProfileService, validationService, cryptographyService, new LogService());
          
//        //    var registrationModel = new RegistrationModel();

//        //    registrationModel.firstName = firstName;
//        //    registrationModel.surname = surname;
//        //    registrationModel.username = username;
//        //    registrationModel.password = password;
//        //    registrationModel.emailAddress = emailAddress;
//        //    registrationModel.dateOfBirth = dateOfBirth;
//        //    registrationModel.ipAddress = ipAddress;

//        //    RegistrationResultModel expectedResult = new RegistrationResultModel();
//        //    expectedResult.Success = false;
//        //    expectedResult.ErrorMessage = error.ToString();

//        //    RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//        //    // Act
//        //    var actualResult = await registrationController.RegisterUser(registrationModel);

//        //    // Assert
//        //    Assert.IsTrue(actualResult.Success == expectedResult.Success);
//        //    Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
//        //}

//        [DataTestMethod]
//        [DataRow("TestFirstName11", "TestSurname11", "TestUser11", "TestPassword11", "matt@infinimuse.com",
//            "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", 11)]
//        public async Task RegisterUser_Success_ResultsAreAccurate(string firstName, string surname,
//            string username, string password, string emailAddress, string dateOfBirth, string ipAddress, int expectedId)
//        {
//            // Arrange
//            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//            _testConfigKeys.Add("Sender", "support@infinimuse.com");
//            _testConfigKeys.Add("TrackOpens", "true");
//            _testConfigKeys.Add("Subject", "Welcome!");
//            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//            _testConfigKeys.Add("MessageStream", "outbound");
//            _testConfigKeys.Add("Tag", "Welcome");
//            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//            IConfiguration configuration = new ConfigurationBuilder()
//                            .AddInMemoryCollection(_testConfigKeys)
//                            .Build();
//            EmailService emailService = new EmailService(new UserAccountRepository
//             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
//            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//                new ConnectionStringData()));

//            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

//            IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//                userProfileService, validationService, cryptographyService, new LogService());

//            var registrationModel = new RegistrationModel();

//            registrationModel.firstName = firstName;
//            registrationModel.surname = surname;
//            registrationModel.username = username;
//            registrationModel.password = password;
//            registrationModel.emailAddress = emailAddress;
//            registrationModel.dateOfBirth = dateOfBirth;
//            registrationModel.ipAddress = ipAddress;

//            RegistrationResultModel expectedResult = new RegistrationResultModel();
//            expectedResult.Success = true;
//            expectedResult.AccountId = expectedId;

//            RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//            // Act
//            var actualResult = await registrationController.RegisterUser(registrationModel);

//            // Assert
//            Assert.IsTrue(actualResult.Success == expectedResult.Success);
//            Assert.IsTrue(actualResult.AccountId == expectedResult.AccountId);
//        }

//        [DataTestMethod]
//        [DataRow(1)]
//        public async Task ResendEmail_BadEmail_ReturnFalse(int accountId)
//        {
//            // Arrange
//            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//            _testConfigKeys.Add("Sender", "support@infinimuse.com");
//            _testConfigKeys.Add("TrackOpens", "true");
//            _testConfigKeys.Add("Subject", "Welcome!");
//            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//            _testConfigKeys.Add("MessageStream", "outbound");
//            _testConfigKeys.Add("Tag", "Welcome");
//            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//            IConfiguration configuration = new ConfigurationBuilder()
//                            .AddInMemoryCollection(_testConfigKeys)
//                            .Build();
//            EmailService emailService = new EmailService(new UserAccountRepository
//             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
//            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//                new ConnectionStringData()));

//            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

//            IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//                userProfileService, validationService, cryptographyService, new LogService());

//            var expectedResult = false;

//            RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//            // Act
//            var actualResult = await registrationController.ResendEmail(accountId);

//            // Assert
//            Assert.IsTrue(actualResult == expectedResult);
//        }

//        [DataTestMethod]
//        [DataRow("TestFirstName11", "TestSurname11", "TestUser11", "TestPassword11", "matt@infinimuse.com",
//            "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", 11)]
//        public async Task RegisterUser_GoodEmail_ReturnTrue(string firstName, string surname,
//            string username, string password, string emailAddress, string dateOfBirth, string ipAddress, int accountId)
//        {
//            // Arrange
//            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//            _testConfigKeys.Add("Sender", "support@infinimuse.com");
//            _testConfigKeys.Add("TrackOpens", "true");
//            _testConfigKeys.Add("Subject", "Welcome!");
//            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//            _testConfigKeys.Add("MessageStream", "outbound");
//            _testConfigKeys.Add("Tag", "Welcome");
//            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//            IConfiguration configuration = new ConfigurationBuilder()
//                            .AddInMemoryCollection(_testConfigKeys)
//                            .Build();
//            EmailService emailService = new EmailService(new UserAccountRepository
//             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
//            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//                new ConnectionStringData()));

//            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());
          
//            IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//                userProfileService, validationService, cryptographyService, new LogService());
          
//            var registrationModel = new RegistrationModel();

//            registrationModel.firstName = firstName;
//            registrationModel.surname = surname;
//            registrationModel.username = username;
//            registrationModel.password = password;
//            registrationModel.emailAddress = emailAddress;
//            registrationModel.dateOfBirth = dateOfBirth;
//            registrationModel.ipAddress = ipAddress;

//            RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);
//            await registrationController.RegisterUser(registrationModel);

//            var expectedResult = true;

//            // Act
//            var actualResult = await registrationController.ResendEmail(accountId);

//            // Assert
//            Assert.IsTrue(actualResult == expectedResult);
//        }
//        #endregion

//        #region Non-Functional Tests
//        [DataTestMethod]
//        [DataRow("TestFirstName11", "TestSurname11", "TestUser11", "TestPassword11", "matt@infinimuse.com",
//            "3/28/2007 7:13:50 PM +00:00", "127.0.0.1", 5000)]
//        public async Task RegisterUser_GotUserAccountId_ExecuteLessThan5Seconds(string firstName, string surname,
//            string username, string password, string emailAddress, string dateOfBirth, string ipAddress, int expectedTime)
//        {
//            // Arrange
//            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//            _testConfigKeys.Add("Sender", "support@infinimuse.com");
//            _testConfigKeys.Add("TrackOpens", "true");
//            _testConfigKeys.Add("Subject", "Welcome!");
//            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//            _testConfigKeys.Add("MessageStream", "outbound");
//            _testConfigKeys.Add("Tag", "Welcome");
//            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//            IConfiguration configuration = new ConfigurationBuilder()
//                            .AddInMemoryCollection(_testConfigKeys)
//                            .Build();
//            EmailService emailService = new EmailService(new UserAccountRepository
//             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
//            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//                new ConnectionStringData()));

//            IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//                userProfileService, validationService, cryptographyService, new LogService());

//            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());
          

//            var registrationModel = new RegistrationModel();
//            RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);
//            registrationModel.firstName = firstName;
//            registrationModel.surname = surname;
//            registrationModel.username = username;
//            registrationModel.password = password;
//            registrationModel.emailAddress = emailAddress;
//            registrationModel.dateOfBirth = dateOfBirth;
//            registrationModel.ipAddress = ipAddress;

//            // Act
//            var timer = Stopwatch.StartNew();
//            await registrationController.RegisterUser(registrationModel);
//            timer.Stop();

//            var actualTime = timer.ElapsedMilliseconds;
//            Debug.WriteLine("Actual Execution Time: " + actualTime);

//            // Assert
//            Assert.IsTrue(actualTime <= expectedTime);
//        }

//        [DataTestMethod]
//        [DataRow(1, 5000)]
//        public async Task ResendEmail_ExecuteLessThan5Seconds(int accountId, int expectedTime)
//        {
//            // Arrange
//            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

//            _testConfigKeys.Add("Sender", "support@infinimuse.com");
//            _testConfigKeys.Add("TrackOpens", "true");
//            _testConfigKeys.Add("Subject", "Welcome!");
//            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
//            _testConfigKeys.Add("MessageStream", "outbound");
//            _testConfigKeys.Add("Tag", "Welcome");
//            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

//            IConfiguration configuration = new ConfigurationBuilder()
//                            .AddInMemoryCollection(_testConfigKeys)
//                            .Build();
//            EmailService emailService = new EmailService(new UserAccountRepository
//             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
//             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
//                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
//            IUserAccountService userAccountService = new UserAccountService(new UserAccountRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IUserProfileService userProfileService = new UserProfileService(new UserProfileRepository
//                (new SQLServerGateway(), new ConnectionStringData()));
//            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
//            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
//                new ConnectionStringData()));

//            IRegistrationManager registrationManager = new RegistrationManager(emailService, userAccountService,
//                userProfileService, validationService, cryptographyService, new LogService());

//            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
//            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

//            RegistrationController registrationController = new RegistrationController(registrationManager, userAccountService, emailService);

//            // Act
//            var timer = Stopwatch.StartNew();
//            await registrationController.ResendEmail(accountId);
//            timer.Stop();

//            var actualTime = timer.ElapsedMilliseconds;
//            Debug.WriteLine("Actual Execution Time: " + actualTime);

//            // Assert
//            Assert.IsTrue(actualTime <= expectedTime);
//        }
//        #endregion
//    }
//}
