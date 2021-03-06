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
    public class CyrptographyTests
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
            
        }

    

        [DataTestMethod]
        [DataRow("Password", 1)]
        public async Task newPasswordEncryptAsync_EncryptNewPassword_NewPasswordEncryptSuccessful(string password, int UserID)
        {


            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());


            // Act
            ICryptographyService CryptographyService = new CryptographyService(userAccountRepo);

            await CryptographyService.newPasswordEncryptAsync(password, UserID);


            var userAccount = await userAccountRepo.GetAccountById(UserID);
            var encryptedPassword = userAccount.Password;

            string salt = await userAccountRepo.GetSaltById(UserID);

            string SaltedPassword = password + salt;

            byte[] Data = System.Text.Encoding.ASCII.GetBytes(SaltedPassword);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            string testPassword = System.Text.Encoding.ASCII.GetString(Data).ToString();


            //Assert
            Assert.IsTrue(testPassword == encryptedPassword);



        }




        [DataTestMethod]
        [DataRow("Password", 1)]
        public async Task encryptPasswordAsync_EncryptPassword_EncryptPasswordSuccessful(string password, int UserID)
        {


            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());


            // Act
            ICryptographyService CryptographyService = new CryptographyService(userAccountRepo);

            string encryptedPassedPassword = await CryptographyService.encryptPasswordAsync(password, UserID);


            var userAccount = await userAccountRepo.GetAccountById(UserID);
            var encryptedPassword = userAccount.Password;

            string salt = await userAccountRepo.GetSaltById(UserID);

            string SaltedPassword = password + salt;

            byte[] Data = System.Text.Encoding.ASCII.GetBytes(SaltedPassword);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            string testPassword = System.Text.Encoding.ASCII.GetString(Data).ToString();


            //Assert
            Assert.IsTrue(testPassword == encryptedPassedPassword);



        }





    }
}
