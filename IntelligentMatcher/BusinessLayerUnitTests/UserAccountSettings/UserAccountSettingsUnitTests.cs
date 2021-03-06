using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Security;
using System;
using System.Threading.Tasks;
using UserAccountSettings;

namespace BusinessLayerUnitTests.UserAccountSettings
{
    [TestClass]
    public class UserAccountSettingsTestsUnitTests
    {




        [DataTestMethod]
        [DataRow(2, 12, "White", "Times-New-Roman")]
        public async Task CreateDefaultUserAccountSettings_DefaultUserIsCreated_DefaultUserIsSuccessfulyCreated(int UserId, int FontSize, string ThemeColor, string FontStyle)
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

            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserAccountSettingsRepository> mockUserAccountSettingsRepository = new Mock<IUserAccountSettingsRepository>();
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(mockUserAccountRepository.Object, mockUserAccountSettingsRepository.Object, mockCryptographyService.Object, mockAuthenticationService.Object);
            UserAccountSettingsModel model = new UserAccountSettingsModel();
            userAccountSettingsModel.Id = 0;
            userAccountSettingsModel.UserId = UserId;
            userAccountSettingsModel.FontSize = FontSize;
            userAccountSettingsModel.FontStyle = FontStyle;
            userAccountSettingsModel.ThemeColor = ThemeColor;
            bool result = await userAccountSettingsManager.CreateDefaultUserAccountSettings(userAccountSettingsModel);

            if (result == true)
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



            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserAccountSettingsRepository> mockUserAccountSettingsRepository = new Mock<IUserAccountSettingsRepository>();
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();


            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(mockUserAccountRepository.Object, mockUserAccountSettingsRepository.Object, mockCryptographyService.Object, mockAuthenticationService.Object);

            bool result = await userAccountSettingsManager.ChangeFontSize(userId, FontSize);

            if (result == true)
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



            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserAccountSettingsRepository> mockUserAccountSettingsRepository = new Mock<IUserAccountSettingsRepository>();
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(mockUserAccountRepository.Object, mockUserAccountSettingsRepository.Object, mockCryptographyService.Object, mockAuthenticationService.Object);

            bool result = await userAccountSettingsManager.ChangeThemeColor(userId, ThemeColor);


            if (result == true)
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

            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserAccountSettingsRepository> mockUserAccountSettingsRepository = new Mock<IUserAccountSettingsRepository>();
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(mockUserAccountRepository.Object, mockUserAccountSettingsRepository.Object, mockCryptographyService.Object, mockAuthenticationService.Object);

            bool result = await userAccountSettingsManager.ChangeFontStyleAsync(userId, fontStyle);

            if (result == true)
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
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserAccountSettingsRepository> mockUserAccountSettingsRepository = new Mock<IUserAccountSettingsRepository>();
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            mockAuthenticationService.Setup(x => x.AuthenticatePasswordWithUserId(password, userId)).Returns(Task.FromResult(true));
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(mockUserAccountRepository.Object, mockUserAccountSettingsRepository.Object, mockCryptographyService.Object, mockAuthenticationService.Object);

            bool result = await userAccountSettingsManager.DeleteAccountByUserIDAsync(userId, password);


            if (result == true)
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

            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserAccountSettingsRepository> mockUserAccountSettingsRepository = new Mock<IUserAccountSettingsRepository>();
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            mockUserAccountRepository.Setup(x => x.UpdateAccountEmail(userId, email));
            mockAuthenticationService.Setup(x => x.AuthenticatePasswordWithUserId(password, userId)).Returns(Task.FromResult(true));
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(mockUserAccountRepository.Object, mockUserAccountSettingsRepository.Object, mockCryptographyService.Object, mockAuthenticationService.Object);

            bool result = await userAccountSettingsManager.ChangeEmail(password, email, userId);


            if (result == true)
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
            Mock<IUserAccountRepository> mockUserAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserAccountSettingsRepository> mockUserAccountSettingsRepository = new Mock<IUserAccountSettingsRepository>();
            Mock<IAuthenticationService> mockAuthenticationService = new Mock<IAuthenticationService>();
            Mock<ICryptographyService> mockCryptographyService = new Mock<ICryptographyService>();
            mockAuthenticationService.Setup(x => x.AuthenticatePasswordWithUserId(password, userId)).Returns(Task.FromResult(true));
            mockCryptographyService.Setup(x => x.newPasswordEncryptAsync(newPassword, userId)).Returns(Task.FromResult(true));

            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(mockUserAccountRepository.Object, mockUserAccountSettingsRepository.Object, mockCryptographyService.Object, mockAuthenticationService.Object);

            bool result = await userAccountSettingsManager.ChangePassword(password, newPassword, userId);


            if (result == true)
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
