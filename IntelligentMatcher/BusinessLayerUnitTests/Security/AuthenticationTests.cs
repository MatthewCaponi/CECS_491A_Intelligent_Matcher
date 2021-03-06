using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Security;
using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;
using Services;

using Moq;
namespace BusinessLayerUnitTests.Security
{
    [TestClass]

    public class AuthenticationTests
    {
        [TestInitialize()]
        public async Task Init()
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();

            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            var settings = await userAccountSettingsRepository.GetAllSettings();

            foreach (var setting in settings)
            {
                await userAccountSettingsRepository.DeleteUserAccountSettingsByUserId(setting.UserId);
            }

            await DataAccessTestHelper.ReseedAsync("UserAccountSettings", 0, connectionString, dataGateway);


            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);



            int i = 1;
            UserAccountModel userAccountModel = new UserAccountModel();
            userAccountModel.Id = i;
            userAccountModel.Username = "TestUser";
            userAccountModel.Password = "" + i;
            userAccountModel.Salt = "" + i;
            userAccountModel.EmailAddress = "TestEmailAddress";
            userAccountModel.AccountType = "TestAccountType";
            userAccountModel.AccountStatus = "TestAccountStatus";
            userAccountModel.CreationDate = DateTimeOffset.UtcNow;
            userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

            await userAccountRepository.CreateAccount(userAccountModel);
            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepo);
            await cryptographyService.newPasswordEncryptAsync("Password", 1);
        }

        [DataTestMethod]
        [DataRow("Password", "TestUser")]
        public async Task AuthenticatePasswordWithUsename_UserAuthentication_UsernameAndPasswordAuthenticateSuccesfully(string password, string username)
        {


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            IAuthenticationService authenticationService = new AuthenticationService(userAccount); 
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithUsename(password, username);

            if (AuthenticationToken == true)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }



        }
        [DataTestMethod]
        [DataRow("WrongPassword", "TestUser")]
        public async Task AuthenticatePasswordWithUsename_UserAuthentication_UsernameAndPasswordDoNotAuthenticateSuccesfully(string password, string username)
        {


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            IAuthenticationService authenticationService = new AuthenticationService(userAccount);
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithUsename(password, username);

            if (AuthenticationToken == true)
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }



        }
        [DataTestMethod]
        [DataRow("Password", "TestEmailAddress")]
        public async Task AuthenticatePasswordWithEmail_EmailAuthentication_EmailAndPasswordAuthenticateSuccesfully(string password, string email)
        {


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            IAuthenticationService authenticationService = new AuthenticationService(userAccount);
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithEmail(password, email);

            if (AuthenticationToken == true)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }



        }
        [DataTestMethod]
        [DataRow("WrongPassword", "TestEmailAddress")]
        public async Task AuthenticatePasswordWithEmail_EmailAuthentication_EmailAndPasswordDoNotAuthenticateSuccesfully(string password, string email)
        {


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            IAuthenticationService authenticationService = new AuthenticationService(userAccount);
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithEmail(password, email);

            if (AuthenticationToken == true)
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }



        }
        [DataTestMethod]
        [DataRow("Password", 1)]
        public async Task AuthenticatePasswordWithUserId_UserIDAuthentication_EmailAndPasswordAuthenticateSuccesfully(string password, int userId)
        {


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            IAuthenticationService authenticationService = new AuthenticationService(userAccount);
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithUserId(password, userId);

            if(AuthenticationToken == true)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }



        }
        [DataTestMethod]
        [DataRow("WrongPassword", 1)]
        public async Task AuthenticatePasswordWithUserId_UserIDAuthentication_EmailAndPasswordDoNotAuthenticateSuccesfully(string password, int userId)
        {


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            IAuthenticationService authenticationService = new AuthenticationService(userAccount); 
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithUserId(password, userId);

            if (AuthenticationToken == true)
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }



        }
    }
}
