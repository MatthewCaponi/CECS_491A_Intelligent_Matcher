﻿using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;
using IntelligentMatcher.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services;
using UserManagement.Services;
using UserManagement.Models;
using Moq;
using Registration;
using Registration.Services;
using BusinessModels;
using Security;
using System.Diagnostics;
/*
namespace BusinessLayerUnitTests.Registration
{
    [TestClass]
    public class RegistrationTests
    {
        #region Test Setup
        // Insert test rows before every test case
        [TestInitialize]
        public async Task Init()
        {
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
                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;
                await userAccountRepository.CreateAccount(userAccountModel);

                UserProfileModel userProfileModel = new UserProfileModel();
                userProfileModel.Id = i;
                userProfileModel.FirstName = "TestFirstName" + i;
                userProfileModel.Surname = "TestSurname" + i;
                userProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
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

        #region Unit Tests
        [DataTestMethod]
        [DataRow(11, "TestUser1", "TestPassword11", "TestEmailAddress11", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1", ErrorMessage.UsernameExists)]
        public async Task RegisterUser_UserNameExists_ResultIsUserNameExists(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, string ipAddress, ErrorMessage error)
        {
            //Arrange
            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<IValidationService> mockValidationService = new Mock<IValidationService>();
            mockValidationService.Setup(x => x.UsernameExists(username)).Returns(Task.FromResult(true));
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();

            ResultModel<int> registry = new ResultModel<int>();
            registry.ErrorMessage = error;
            var expectedResult = new Tuple<bool, ResultModel<int>>(false, registry);

            RegistrationManager registrationManager = new RegistrationManager(mockEmailService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object, mockValidationService.Object,
                mockCryptographyService.Object);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress1", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1", ErrorMessage.EmailExists)]
        public async Task RegisterUser_EmailExists_ResultIsEmailExists(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName,string ipAddress, ErrorMessage error)
        {
            //Arrange
            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<IValidationService> mockValidationService = new Mock<IValidationService>();
            mockValidationService.Setup(x => x.EmailExists(emailAddress)).Returns(Task.FromResult(true));
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();

            ResultModel<int> registry = new ResultModel<int>();
            registry.ErrorMessage = error;
            var expectedResult = new Tuple<bool, ResultModel<int>>(false, registry);

            RegistrationManager registrationManager = new RegistrationManager(mockEmailService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object, mockValidationService.Object,
                mockCryptographyService.Object);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress11", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1", ErrorMessage.EmailNotSent)]
        public async Task RegisterUser_UserRegistered_ResultIsEmailNotSent(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, string ipAddress, ErrorMessage error)
        {
            //Arrange
            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.SendEmail(new EmailModel())).Returns(Task.FromResult(false));
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<IValidationService> mockValidationService = new Mock<IValidationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();

            ResultModel<int> registry = new ResultModel<int>();
            registry.ErrorMessage = error;
            var expectedResult = new Tuple<bool, ResultModel<int>>(true, registry);

            RegistrationManager registrationManager = new RegistrationManager(mockEmailService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object, mockValidationService.Object,
                mockCryptographyService.Object);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress11", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1")]
        public async Task RegisterUser_UserRegistered_ResultIsEmailSent(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, string ipAddress)
        {
            //Arrange
            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.SendEmail(new EmailModel())).Returns(Task.FromResult(true));
            Mock<IUserAccountService> mockUserAccountService = new Mock<IUserAccountService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<IValidationService> mockValidationService = new Mock<IValidationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();

            ResultModel<int> registry = new ResultModel<int>();
            var expectedResult = new Tuple<bool, ResultModel<int>>(true, registry);

            RegistrationManager registrationManager = new RegistrationManager(mockEmailService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object, mockValidationService.Object,
                mockCryptographyService.Object);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue(actualResult.Item1 == expectedResult.Item1);
        }
        #endregion

        #region Functional Tests
        [DataTestMethod]
        [DataRow(11, "TestUser1", "TestPassword11", "matt@infinimuse.com", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1", ErrorMessage.UsernameExists)]
        public async Task RegisterUser_UserNameExists(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, string ipAddress, ErrorMessage error)
        {
            //Arrange
            EmailService emailService = new EmailService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData()));

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            ResultModel<int> registry = new ResultModel<int>();
            registry.ErrorMessage = error;
            var expectedResult = new Tuple<bool, ResultModel<int>>(false, registry);

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress1", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1", ErrorMessage.EmailExists)]
        public async Task RegisterUser_EmailExists(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, string ipAddress, ErrorMessage error)
        {
            //Arrange
            EmailService emailService = new EmailService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData()));

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            ResultModel<int> registry = new ResultModel<int>();
            registry.ErrorMessage = error;
            var expectedResult = new Tuple<bool, ResultModel<int>>(false, registry);

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage));
        }

       [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress100", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1", ErrorMessage.EmailNotSent)]
        public async Task RegisterUser_EmailNotSent(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, string ipAddress, ErrorMessage error)
        {
            //Arrange
            EmailService emailService = new EmailService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData()));

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            ResultModel<int> registry = new ResultModel<int>();
            registry.Result = expectedId;
            registry.ErrorMessage = error;
            var expectedResult = new Tuple<bool, ResultModel<int>>(true, registry);

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage) &&
                (actualResult.Item2.Result == expectedResult.Item2.Result));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "matt@infinimuse.com", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1")]
        public async Task RegisterUser_Success(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, string ipAddress)
        {
            //Arrange
            EmailService emailService = new EmailService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData()));

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            ResultModel<int> registry = new ResultModel<int>();
            registry.Result = expectedId;
            var expectedResult = new Tuple<bool, ResultModel<int>>(true, registry);

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.Result == expectedResult.Item2.Result));
        }
        #endregion

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "shariffshaan@gmail.com", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1", 5000)]
        public async Task RegisterUser_LessThan5Seconds(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, string ipAddress, int expectedTime)
        {
            //Arrange
            EmailService emailService = new EmailService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData()));

            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = firstName;
            webUserProfileModel.Surname = lastName;
            webUserProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
            webUserProfileModel.UserAccountId = webUserAccountModel.Id;

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var timer = Stopwatch.StartNew();
            await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel, password, ipAddress);
            timer.Stop();

            var actualTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualTime);

            //Assert
            Assert.IsTrue(actualTime <= expectedTime);
        }
        #endregion
    }
}*/
