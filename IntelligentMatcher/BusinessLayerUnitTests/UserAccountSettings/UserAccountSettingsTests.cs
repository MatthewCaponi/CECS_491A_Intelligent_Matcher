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
        public async Task newUserAccountSettingsTest(int UserId, int FontSize, string ThemeColor, string FontStyle)
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
            userAccountSettingsModel.Id = UserId;
            userAccountSettingsModel.UserId = UserId;
            userAccountSettingsModel.FontSize = FontSize;
            userAccountSettingsModel.FontStyle = FontStyle;
            userAccountSettingsModel.ThemeColor = ThemeColor;

            IAccountSettingsManager accountSettingsConroller = new IAccountSettingsController();

            await accountSettingsConroller.CreateUserAccountSettings(userAccountSettingsModel);

            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);





            IAccountSettingsManager accountSettingsController = new IAccountSettingsController();

            UserAccountSettingsModel model = await userAccountSettingsRepository.GetUserAccountSettingsByUserId(UserId);

            
            if(model.UserId == UserId && model.FontSize == FontSize && model.FontStyle == FontStyle && model.ThemeColor == ThemeColor)
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

            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);




            await userAccountSettingsRepository.UpdateFontSize(userId, FontSize);


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





    }
}
