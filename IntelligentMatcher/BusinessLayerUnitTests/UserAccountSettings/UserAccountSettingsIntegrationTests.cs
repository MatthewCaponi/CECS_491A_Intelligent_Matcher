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
using Moq;
using TestHelper;

namespace BusinessLayerUnitTests.UserAccountSettings
{
    [TestClass]
    public class UserAccountSettingsIntegrationTests
    {       
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            IDataGateway dataGateway = new SQLServerGateway();
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

            UserAccountRepository userAccountRepo = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepo);

            await cryptographyService.NewPasswordEncryptAsync("Password", 1);

            UserAccountSettingsModel userAccountSettingsModel = new UserAccountSettingsModel();
            userAccountSettingsModel.Id = 0;
            userAccountSettingsModel.UserId = 1;
            userAccountSettingsModel.FontSize = 12;
            userAccountSettingsModel.FontStyle = "Time New Roman";
            userAccountSettingsModel.ThemeColor = "White";


            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);


            await userAccountSettingsManager.CreateUserAccountSettingsAsync(userAccountSettingsModel);
        }


        [TestCleanup()]
        public async Task CleanUp()
        {

            IDataGateway dataGateway = new SQLServerGateway();
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

        }

        [DataTestMethod]
        [DataRow(2, 12, "Default Theme Color", "Defualt Font Style")]
        public async Task CreateDefaultUserAccountSettings_DefaultUserIsCreated_DefaultUserIsSuccessfulyCreated(int UserId, int FontSize, string ThemeColor, string FontStyle)
        {
            IDataGateway dataGateway = new SQLServerGateway();
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

            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);
            UserAccountSettingsModel model = new UserAccountSettingsModel();
            userAccountSettingsModel.Id = 0;
            userAccountSettingsModel.UserId = UserId;
            userAccountSettingsModel.FontSize = FontSize;
            userAccountSettingsModel.FontStyle = FontStyle;
            userAccountSettingsModel.ThemeColor = ThemeColor;
<<<<<<< HEAD
            bool result = await userAccountSettingsManager.CreateDefaultUserAccountSettingsAsync(userAccountSettingsModel);
            if (!result)
            {
                Assert.IsTrue(false);
            }
=======
            await userAccountSettingsManager.CreateDefaultUserAccountSettingsAsync(userAccountSettingsModel);
>>>>>>> main
            model = await userAccountSettingsRepository.GetUserAccountSettingsByUserId(UserId);         
            if(model.UserId == UserId && model.FontSize == 12 && model.FontStyle == "Defualt Font Style" && model.ThemeColor == "Default Theme Color")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(1, 15)]
        public async Task ChangeFontSize_UsersFontSizeIsChanged_FontSizeSuccessfullyChanges(int userId, int FontSize)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

<<<<<<< HEAD
            bool result = await userAccountSettingsManager.ChangeFontSizeAsync(userId, FontSize);
            if (!result)
            {
                Assert.IsTrue(false);
            }
=======
            await userAccountSettingsManager.ChangeFontSizeAsync(userId, FontSize);

>>>>>>> main
            string newFontSize = await userAccountSettingsRepository.GetFontSizeByID(userId);
            if (FontSize.ToString() == newFontSize)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, "Black")]
        public async Task ChangeThemeColor_UsersThemeColorChanges_UsersThemeColorIsChangedSuccessfully(int userId, string ThemeColor)
        {


            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

<<<<<<< HEAD
            bool result = await userAccountSettingsManager.ChangeThemeColorAsync(userId, ThemeColor);
            if (!result)
            {
                Assert.IsTrue(false);
            }
=======
            await userAccountSettingsManager.ChangeThemeColorAsync(userId, ThemeColor);

>>>>>>> main
            string newThemeColor = await userAccountSettingsRepository.GetThemeColorByID(userId);

            if (ThemeColor == newThemeColor)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(1, "Helvetica")]
        public async Task ChangeFontStyleAsync_UserFontStyleChange_UsersFontStyleChanges(int userId, string fontStyle)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.ChangeFontStyleAsync(userId, fontStyle);
            if (!result)
            {
                Assert.IsTrue(false);
            }
            string newFontStyle = await userAccountSettingsRepository.GetFontStyleByID(userId);

            if (fontStyle == newFontStyle)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }

        }

        [DataTestMethod]
        [DataRow(1, "Password")]
        public async Task DeleteAccountByUserIDAsync_UserAccountIsDelted_UserAccountSuccessfulyDeletes(int userId, string password)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.DeleteAccountByUserIDAsync(userId, password);
            if (!result)
            {
                Assert.IsTrue(false);
            }
            UserAccountModel model = await userAccountRepository.GetAccountById(userId);

            if (model.AccountStatus == "Deleted")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(1, "Password", "NewEmail")]
        public async Task ChangeEmail_UserEmailChanges_EmailChangeCompletes(int userId, string password, string email)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.ChangeEmailAsync(password, email, userId);
<<<<<<< HEAD
            if (!result)
            {
                Assert.IsTrue(false);
            }
=======

>>>>>>> main
            UserAccountModel model = await userAccountRepository.GetAccountById(userId);

            if (model.EmailAddress == email)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);

            }
        }


        [DataTestMethod]
        [DataRow(1, "Password", "NewPassword")]
        public async Task ChangePasswordTest_UserPasswordChanges_PasswordChangeCompletes(int userId, string password, string newPassword)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            bool result = await userAccountSettingsManager.ChangePasswordAsync(password, newPassword, userId);
<<<<<<< HEAD
            if (!result)
            {
                Assert.IsTrue(false);
            }
=======

>>>>>>> main
            UserAccountModel model = await userAccountRepository.GetAccountById(userId);
            UserAccountRepository userAccountRepo = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
                       
            string encryptedNewPassword = await cryptographyService.EncryptPasswordAsync(newPassword, userId);

            if (model.Password == encryptedNewPassword)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }
    }
}
