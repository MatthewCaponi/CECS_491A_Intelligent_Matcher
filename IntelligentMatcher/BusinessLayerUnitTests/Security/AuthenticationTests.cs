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
            userAccountModel.Username = "TestUser" + i;
            userAccountModel.Password = "" + i;
            userAccountModel.Salt = "" + i;
            userAccountModel.EmailAddress = "TestEmailAddress" + i;
            userAccountModel.AccountType = "TestAccountType" + i;
            userAccountModel.AccountStatus = "TestAccountStatus" + i;
            userAccountModel.CreationDate = DateTimeOffset.UtcNow;
            userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

            await userAccountRepository.CreateAccount(userAccountModel);
            ICryptographyService CryptographyService = new CryptographyService();

            await CryptographyService.newPasswordEncryptAsync("Password", 1);
        }



        [DataTestMethod]
        [DataRow("Password", 1)]
        public async Task testAuthenticationTrue(string password, int userId)
        {


            IAuthenticationService authenticationService = new AuthenticationService();
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
        public async Task testAuthenticationWithWrongpassword(string password, int userId)
        {


            IAuthenticationService authenticationService = new AuthenticationService();
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
