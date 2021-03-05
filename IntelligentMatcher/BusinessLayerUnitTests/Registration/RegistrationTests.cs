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
using IntelligentMatcher.UserManagement;
using Registration;
using Registration.Services;
using BusinessModels;
using Security;

namespace BusinessLayerUnitTests.Registration
{
    [TestClass]
    public class RegistrationTests
    {
        // Insert test rows before every test case
        [TestInitialize]
        public async Task Init()
        {
            var numTestRows = 10;

            IDataGateway dataGateway = new DataGateway();
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
            IDataGateway dataGateway = new DataGateway();
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

        [DataTestMethod]
        [DataRow(11, "TestUser1", "TestPassword11", "TestEmailAddress11", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", ErrorMessage.UsernameExists)]
        public async Task RegisterUser_UserNameExists(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, ErrorMessage error)
        {
            //Arrange
            EmailVerificationService emailVerificationService = new EmailVerificationService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new DataGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new DataGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService();

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

            RegistrationManager registrationManager = new RegistrationManager(emailVerificationService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var actualResult = await registrationManager.RegisterNewAccount(webUserAccountModel, webUserProfileModel, false, password);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress1", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", ErrorMessage.EmailExists)]
        public async Task RegisterUser_EmailExists(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, ErrorMessage error)
        {
            //Arrange
            EmailVerificationService emailVerificationService = new EmailVerificationService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new DataGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new DataGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService();

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

            RegistrationManager registrationManager = new RegistrationManager(emailVerificationService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var actualResult = await registrationManager.RegisterNewAccount(webUserAccountModel, webUserProfileModel, false, password);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "TestEmailAddress11", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11", ErrorMessage.EmailNotSent),]
        public async Task RegisterUser_EmailNotSent(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName, ErrorMessage error)
        {
            //Arrange
            EmailVerificationService emailVerificationService = new EmailVerificationService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new DataGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new DataGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService();

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
            var expectedResult = new Tuple<bool, ResultModel<int>>(false, registry);

            RegistrationManager registrationManager = new RegistrationManager(emailVerificationService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var actualResult = await registrationManager.RegisterNewAccount(webUserAccountModel, webUserProfileModel, false, password);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.ErrorMessage == expectedResult.Item2.ErrorMessage));
        }

        [DataTestMethod]
        [DataRow(11, "TestUser11", "TestPassword11", "shariffshaan@gmail.com", "TestAccountType11",
            "TestAccountStatus11", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestFirstName11",
            "TestSurname11"),]
        public async Task RegisterUser_Success(int expectedId, string username, string password,
            string emailAddress, string accountType, string accountStatus, string creationDate, string updationDate,
            string firstName, string lastName)
        {
            //Arrange
            EmailVerificationService emailVerificationService = new EmailVerificationService();
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new DataGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new DataGateway(), new ConnectionStringData()));
            ValidationService validationService = new ValidationService(userAccountService, userProfileService);
            ICryptographyService cryptographyService = new CryptographyService();

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

            RegistrationManager registrationManager = new RegistrationManager(emailVerificationService,
                userAccountService, userProfileService, validationService, cryptographyService);

            //Act
            var actualResult = await registrationManager.RegisterNewAccount(webUserAccountModel, webUserProfileModel, true, password);

            //Assert
            Assert.IsTrue((actualResult.Item1 == expectedResult.Item1) &&
                (actualResult.Item2.Result == expectedResult.Item2.Result));
        }

    }
}
