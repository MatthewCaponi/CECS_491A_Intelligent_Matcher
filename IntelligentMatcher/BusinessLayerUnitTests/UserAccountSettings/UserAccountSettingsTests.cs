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
using UserAccountSettings;
namespace BusinessLayerUnitTests.UserAccountSettings
{
    [TestClass]
    public class UserAccountSettingsTests
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

            UserAccountSettingsModel userAccountSettingsModel = new UserAccountSettingsModel();
            userAccountSettingsModel.Id = 0;
            userAccountSettingsModel.UserId = 1;
            userAccountSettingsModel.FontSize = 12;
            userAccountSettingsModel.FontStyle = "Time New Roman";
            userAccountSettingsModel.ThemeColor = "White";

            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            await accountSettingsConroller.CreateUserAccountSettings(userAccountSettingsModel);
        }




        [DataTestMethod]
        [DataRow(2, 12, "White", "Times-New-Roman")]
        public async Task newDefaultUserAccountSettingsTest(int UserId, int FontSize, string ThemeColor, string FontStyle)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            int i = 2;
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



            UserAccountSettingsModel userAccountSettingsModel = new UserAccountSettingsModel();


            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            await accountSettingsConroller.CreateDefaultUserAccountSettings(UserId);

            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);





            IAccountSettingsManager accountSettingsController = new IAccountSettingsController();

            UserAccountSettingsModel model = await userAccountSettingsRepository.GetUserAccountSettingsByUserId(UserId);

            
            if(model.UserId == UserId && model.FontSize == 12 && model.FontStyle == "Defualt Font Style" && model.ThemeColor == "Default Theme Color")
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }


            //Asser


        }

        [DataTestMethod]
        [DataRow(1, 15)]
        public async Task ChangeFontSize(int userId, int FontSize)
        {


            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            await accountSettingsConroller.ChangeFontSize(userId, FontSize);


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);


            IAccountSettingsManager accountSettingsController = new IAccountSettingsController();

            string newFontSize = await userAccountSettingsRepository.GetFontSizeByID(userId);

            if (FontSize.ToString() == newFontSize)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }

            //Asser


        }

        [DataTestMethod]
        [DataRow(1, "Black")]
        public async Task ChangeThemeColor(int userId, string ThemeColor)
        {


            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            await accountSettingsConroller.ChangeThemeColor(userId, ThemeColor);


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);


            IAccountSettingsManager accountSettingsController = new IAccountSettingsController();

            string newThemeColor = await userAccountSettingsRepository.GetThemeColorByID(userId);

            if (ThemeColor == newThemeColor)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }

            //Asser


        }

        [DataTestMethod]
        [DataRow(1, "Helvetica")]
        public async Task ChangeFontStyle(int userId, string fontStyle)
        {


            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            await accountSettingsConroller.ChangeFontStyleAsync(userId, fontStyle);


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);


            IAccountSettingsManager accountSettingsController = new IAccountSettingsController();

            string newFontStyle = await userAccountSettingsRepository.GetFontStyleByID(userId);

            if (fontStyle == newFontStyle)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }

            //Asser


        }

        [DataTestMethod]
        [DataRow(1, "Password")]
        public async Task DeleteAccountTest(int userId, string password)
        {



            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            string result = await accountSettingsConroller.DeleteAccountByUserIDAsync(userId, password);

            
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);


            UserAccountModel model = await userAccountRepository.GetAccountById(userId);



            if (model.AccountStatus == "Deleted")
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }

            //Asser


        }

        [DataTestMethod]
        [DataRow(1, "Password", "NewEmail")]
        public async Task ChangeEmailTest(int userId, string password, string email)
        {



            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            string result = await accountSettingsConroller.ChangeEmail(password, email, userId);


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);


            UserAccountModel model = await userAccountRepository.GetAccountById(userId);



            if (model.EmailAddress == email)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }

            //Asser


        }


        [DataTestMethod]
        [DataRow(1, "Password", "NewPassword")]
        public async Task ChangePasswordTest(int userId, string password, string newPassword)
        {



            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            string result = await accountSettingsConroller.ChangePassword(password, newPassword, userId);


            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);


            UserAccountModel model = await userAccountRepository.GetAccountById(userId);


            ICryptographyService cryptographyService = new CryptographyService();
            string encryptedNewPassword = await cryptographyService.encryptPasswordAsync(newPassword, userId);


            if (model.Password == encryptedNewPassword)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }

            //Asser


        }
    }
}
