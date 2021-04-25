using DataAccess;
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
using TestHelper;

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
            await TestCleaner.CleanDatabase();
            var numTestRows = 10;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(dataGateway, connectionString);

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

                await accountVerificationRepo.CreateAccountVerification(1);

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

            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(dataGateway, connectionString);
            IEnumerable< VerficationTokenModel>  verficationTokens = await accountVerificationRepo.GetAllAccountVerifications();

            foreach (VerficationTokenModel token in verficationTokens)
            {
                await accountVerificationRepo.DeleteAccountVerificationById(token.Id);
            }
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserProfile", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("AccountVerification", 0, connectionString, dataGateway);

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
            Mock<IAccountVerificationRepo> mockAccountVerificionRepo = new Mock<IAccountVerificationRepo>();
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();


            Result<int> expectedResult = new Result<int>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;
            IRegistrationManager registrationManager = new RegistrationManager(mockEmailService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object, mockValidationService.Object,
                mockCryptographyService.Object, mockAccountVerificionRepo.Object, mockUserAccountRepository.Object);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Success == expectedResult.Success) &&
                (actualResult.ErrorMessage == expectedResult.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress1", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1", ErrorMessage.EmailExists)]
        public async Task RegisterUser_EmailExists_ResultIsEmailExists(int expectedId, string username, string password,
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
            mockValidationService.Setup(x => x.EmailExists(emailAddress)).Returns(Task.FromResult(true));
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();

            Result<int> expectedResult = new Result<int>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            Mock<IAccountVerificationRepo> mockAccountVerificionRepo = new Mock<IAccountVerificationRepo>();
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            IRegistrationManager registrationManager = new RegistrationManager(mockEmailService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object, mockValidationService.Object,
                mockCryptographyService.Object, mockAccountVerificionRepo.Object, mockUserAccountRepository.Object);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Success == expectedResult.Success) &&
                (actualResult.ErrorMessage == expectedResult.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress11", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", "127.0.0.1")]
        public async Task RegisterUser_UserRegistered_ResultIsAccountIdReturned(int expectedId, string username, string password,
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
            mockUserAccountService.Setup(x => x.CreateAccount(webUserAccountModel)).Returns(Task.FromResult(expectedId));
            mockUserAccountService.Setup(x => x.GetUserAccount(expectedId)).Returns(Task.FromResult(webUserAccountModel));
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<IValidationService> mockValidationService = new Mock<IValidationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();

            Result<int> expectedResult = new Result<int>();
            expectedResult.Success = true;
            expectedResult.SuccessValue = expectedId;

            Mock<IAccountVerificationRepo> mockAccountVerificionRepo = new Mock<IAccountVerificationRepo>();
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            IRegistrationManager registrationManager = new RegistrationManager(mockEmailService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object, mockValidationService.Object,
                mockCryptographyService.Object, mockAccountVerificionRepo.Object, mockUserAccountRepository.Object);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success &&
                actualResult.SuccessValue == expectedResult.SuccessValue);
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress11", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11")]
        public async Task SendVerificationEmail_Failure_ReturnFalse(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName)
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
            mockUserAccountService.Setup(x => x.GetUserAccount(expectedId)).Returns(Task.FromResult(webUserAccountModel));
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<IValidationService> mockValidationService = new Mock<IValidationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();

            var expectedResult = false;

            Mock<IAccountVerificationRepo> mockAccountVerificionRepo = new Mock<IAccountVerificationRepo>();
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            IRegistrationManager registrationManager = new RegistrationManager(mockEmailService.Object,
                mockUserAccountService.Object, mockUserProfileService.Object, mockValidationService.Object,
                mockCryptographyService.Object, mockAccountVerificionRepo.Object, mockUserAccountRepository.Object);

            //Act
            var actualResult = await registrationManager.SendVerificationEmail(expectedId);

            //Assert
            Assert.IsTrue(actualResult == expectedResult);
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
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(),
                new ConnectionStringData()));

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

            Result<int> expectedResult = new Result<int>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService, accountVerificationRepo, userAccountRepository);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Success == expectedResult.Success) &&
                (actualResult.ErrorMessage == expectedResult.ErrorMessage));
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

            Result<int> expectedResult = new Result<int>();
            expectedResult.Success = false;
            expectedResult.ErrorMessage = error;

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService, accountVerificationRepo, userAccountRepository);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);


            expectedResult.ErrorMessage = error;


            //Assert
            Assert.IsTrue((actualResult.Success == expectedResult.Success) &&
                (actualResult.ErrorMessage == expectedResult.ErrorMessage));
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

            Result<int> expectedResult = new Result<int>();
            expectedResult.Success = true;
            expectedResult.SuccessValue = expectedId;

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService, accountVerificationRepo, userAccountRepository);

            //Act
            var actualResult = await registrationManager.RegisterAccount(webUserAccountModel, webUserProfileModel,
                password, ipAddress);

            //Assert
            Assert.IsTrue((actualResult.Success == expectedResult.Success) &&
                (actualResult.SuccessValue == expectedResult.SuccessValue));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "matt@infinimuse.com", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task SendVerificationEmail_GoodEmail_ReturnTrue(int expectedId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
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

            await userAccountService.CreateAccount(webUserAccountModel);

            var expectedResult = true;

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService, accountVerificationRepo, userAccountRepository);

            //Act
            var actualResult = await registrationManager.SendVerificationEmail(expectedId);

            //Assert
            Assert.IsTrue(actualResult == expectedResult);
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestEmailAddress11", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00")]
        public async Task SendVerificationEmail_BadEmail_ReturnFalse(int expectedId, string username,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate)
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

            await userAccountService.CreateAccount(webUserAccountModel);

            var expectedResult = false;

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService, accountVerificationRepo, userAccountRepository);

            //Act
            var actualResult = await registrationManager.SendVerificationEmail(expectedId);

            //Assert
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion






        #region Integration Tests DeleteIfNotActive
        [DataTestMethod]
        [DataRow(1)]
        public async Task ValidateToken_TokenValidataetd_TokenSuccess(int userId)
        {
            EmailService emailService = new EmailService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService(new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData()));




            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService, accountVerificationRepo, userAccountRepository);
           


            try
            {

                string token = await registrationManager.GetStatusToken(userId);
                bool result = await registrationManager.ValidateStatusToken(userId, token);
                if(result == true)
                {
                    string status = await userAccountRepository.GetStatusById(userId);
                    if(status == "Active")
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);

                    }
                }
                else
                {
                    Assert.IsTrue(false);
                }

            }
            catch
            {
                Assert.IsTrue(true);

            }
        }
        #endregion





        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "matt@infinimuse.com", "TestAccountType11",
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

            IUserAccountRepository userAccountRepository = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            IAccountVerificationRepo accountVerificationRepo = new AccountVerificationRepo(new SQLServerGateway(), new ConnectionStringData());

            RegistrationManager registrationManager = new RegistrationManager(emailService,
                userAccountService, userProfileService, validationService, cryptographyService, accountVerificationRepo, userAccountRepository);

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
}
