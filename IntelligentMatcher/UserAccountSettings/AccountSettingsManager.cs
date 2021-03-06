using System;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;
using Security;
using Models;
using Security;
namespace UserAccountSettings
{

    public class AccountSettingsManager : IAccountSettingsManager
    {

        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserAccountSettingsRepository _userAccountSettingRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IAuthenticationService _authenticationService;

        public AccountSettingsManager(IUserAccountRepository userAccountRepository, IUserAccountSettingsRepository userAccountSettingRepository, ICryptographyService cryptographyService, IAuthenticationService authenticationService)
        {
            _userAccountRepository = userAccountRepository;
            _userAccountSettingRepository = userAccountSettingRepository;
            _cryptographyService = cryptographyService;
            _authenticationService = authenticationService;

        }
        public async Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model)
        {

            await _userAccountSettingRepository.CreateUserAccountSettings(model);

            return (true);

        }

        public async Task<bool> CreateDefaultUserAccountSettings(int UserId, int fontSize, string fontStyle, string themeColor)
        {
            UserAccountSettingsModel model = new UserAccountSettingsModel();
            model.UserId = UserId;
            model.FontSize = fontSize;
            model.FontStyle = fontStyle;
            model.ThemeColor = themeColor;

            await _userAccountSettingRepository.CreateUserAccountSettings(model);

            return (true);

        }

        public async Task<bool> ChangePassword(string oldPassword, string newPassword, int UserID)
        {
            bool AuthenticationToken = await _authenticationService.AuthenticatePasswordWithUserId(oldPassword, UserID);
            if (AuthenticationToken == true)
            {
                await _cryptographyService.newPasswordEncryptAsync(newPassword, UserID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangeEmail(string oldPassword, string email, int UserID)
        {
            bool AuthenticationToken = await _authenticationService.AuthenticatePasswordWithUserId(oldPassword, UserID);
            if (AuthenticationToken == true)
            {
                await _userAccountRepository.UpdateAccountEmail(UserID, email);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteAccountByUserIDAsync(int UserID, string password)
        {
            bool AuthenticationToken = await _authenticationService.AuthenticatePasswordWithUserId(password, UserID);
            if (AuthenticationToken == true)
            {
                await _userAccountRepository.UpdateAccountStatus(UserID, "Deleted");
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> ChangeFontSize(int UserID, int FontSize)
        {
                await _userAccountSettingRepository.UpdateFontSize(UserID, FontSize);
                return true;
        }

        public async Task<bool> ChangeThemeColor(int UserID, string ThemeColor)
        {
                await _userAccountSettingRepository.UpdateThemeColor(UserID, ThemeColor);
                return true;    
        }

        public async Task<bool> ChangeFontStyleAsync(int UserID, string FontStyle)
        {
            try
            {
                await _userAccountSettingRepository.UpdateFontStyle(UserID, FontStyle);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
