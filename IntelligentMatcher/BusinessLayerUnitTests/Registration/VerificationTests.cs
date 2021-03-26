using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using IntelligentMatcher.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Registration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TestHelper;
using UserManagement.Services;

namespace BusinessLayerUnitTests.Registration
{
    [TestClass]
    public class VerificationTests
    {
        #region Test Setup
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
                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;
                await userAccountRepository.CreateAccount(userAccountModel);
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
        }
        #endregion

        #region Functional Tests
        [DataTestMethod]
        [DataRow(1, true)]
        public async Task LinkExpired_DeleteAccount_Success(int accountId, bool expectedResult)
        {
            //Arrange
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserAccessService userAccessService = new UserAccessService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            VerificationManager verificationManager = new VerificationManager(userAccountService, userProfileService,
                userAccessService);

            //Arrange
            var actualResult = await verificationManager.LinkExpired(accountId);

            //Assert
            Assert.IsTrue(actualResult == expectedResult);
        }

        [DataTestMethod]
        [DataRow(1, true)]
        public async Task VerifyAccount_ActivateAccount_Success(int accountId, bool expectedResult)
        {
            //Arrange
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserAccessService userAccessService = new UserAccessService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            VerificationManager verificationManager = new VerificationManager(userAccountService, userProfileService,
                userAccessService);

            //Arrange
            var actualResult = await verificationManager.VerifyEmail(accountId);

            //Assert
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow(1, 5000)]
        public async Task LinkExpired_DeleteAccount_LessThan5Seconds(int accountId, int expectedTime)
        {
            //Arrange
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserAccessService userAccessService = new UserAccessService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            VerificationManager verificationManager = new VerificationManager(userAccountService, userProfileService,
                userAccessService);

            //Arrange
            var timer = Stopwatch.StartNew();
            var actualResult = await verificationManager.LinkExpired(accountId);
            timer.Stop();

            var actualTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualTime);

            //Assert
            Assert.IsTrue(actualTime <= expectedTime);
        }

        [DataTestMethod]
        [DataRow(1, 5000)]
        public async Task VerifyAccount_ActivateAccount_LessThan5Seconds(int accountId, int expectedTime)
        {
            //Arrange
            UserAccountService userAccountService = new UserAccountService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserProfileService userProfileService = new UserProfileService(new UserProfileRepository
                (new SQLServerGateway(), new ConnectionStringData()));
            UserAccessService userAccessService = new UserAccessService(new UserAccountRepository
                (new SQLServerGateway(), new ConnectionStringData()));

            VerificationManager verificationManager = new VerificationManager(userAccountService, userProfileService,
                userAccessService);

            //Arrange
            var timer = Stopwatch.StartNew();
            var actualResult = await verificationManager.VerifyEmail(accountId);
            timer.Stop();

            var actualTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualTime);

            //Assert
            Assert.IsTrue(actualTime <= expectedTime);
        }
        #endregion
    }
}
