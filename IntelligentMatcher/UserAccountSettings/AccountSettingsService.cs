using DataAccess.Repositories;
using System.Threading.Tasks;
using Security;
using Models;

namespace UserAccountSettings
{

    public class AccountSettingsService : IAccountSettingsService
    {

        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserAccountSettingsRepository _userAccountSettingRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IAuthenticationService _authenticationService;

        public AccountSettingsService(IUserAccountRepository userAccountRepository, IUserAccountSettingsRepository userAccountSettingRepository, ICryptographyService cryptographyService, IAuthenticationService authenticationService)
        {
            _userAccountRepository = userAccountRepository;
            _userAccountSettingRepository = userAccountSettingRepository;
            _cryptographyService = cryptographyService;
            _authenticationService = authenticationService;

        }
        public async Task<bool> CreateUserAccountSettingsAsync(UserAccountSettingsModel model)
        {
            try
            {
                return await _userAccountSettingRepository.CreateUserAccountSettings(model);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateDefaultUserAccountSettingsAsync(UserAccountSettingsModel model)
        {
         
                return await _userAccountSettingRepository.CreateUserAccountSettings(model);
     

        }

        public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, int UserID)
        {
            bool AuthenticationToken = await _authenticationService.AuthenticatePasswordWithUserId(oldPassword, UserID);
            if (AuthenticationToken == true)
            {
                return await _cryptographyService.newPasswordEncryptAsync(newPassword, UserID);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangeEmailAsync(string oldPassword, string email, int UserID)
        {
            bool AuthenticationToken = await _authenticationService.AuthenticatePasswordWithUserId(oldPassword, UserID);
            if (AuthenticationToken == true)
            {
                int result = await _userAccountRepository.UpdateAccountEmail(UserID, email);
         
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
                int result = await _userAccountRepository.UpdateAccountStatus(UserID, "Deleted");

                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> ChangeFontSizeAsync(int UserID, int FontSize)
        {
            try
            {
                int result = await _userAccountSettingRepository.UpdateFontSize(UserID, FontSize);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChangeThemeColorAsync(int UserID, string ThemeColor)
        {
            try
            {
                int result = await _userAccountSettingRepository.UpdateThemeColor(UserID, ThemeColor);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChangeFontStyleAsync(int UserID, string FontStyle)
        {
            try
            {
                int result = await _userAccountSettingRepository.UpdateFontStyle(UserID, FontStyle);

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
