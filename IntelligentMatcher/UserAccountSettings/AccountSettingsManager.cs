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
        public AccountSettingsManager(IUserAccountRepository userAccountRepository, IUserAccountSettingsRepository userAccountSettingRepository)
        {
            _userAccountRepository = userAccountRepository;
            _userAccountSettingRepository = userAccountSettingRepository;

        }
        public async Task<string> ChangePassword(string oldPassword, string newPassword, int UserID)
        {

            IAuthenticationService authenticationService = new AuthenticationService(_userAccountRepository);
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithUserId(oldPassword, UserID);

            if (AuthenticationToken == true)
            {


                try
                {
                    UserAccountRepository _userAccountRepository = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
                    ICryptographyService cryptographyService = new CryptographyService(_userAccountRepository);
                    await cryptographyService.newPasswordEncryptAsync(newPassword, UserID);

                    return "Update Succeeded";
                }
                catch
                {
                    return "Updated Failed";
                }
            }
            else
            {
                return "Authentication Failed";
            }
        }

        public async Task<string> ChangeEmail(string oldPassword, string email, int UserID)
        {

            IAuthenticationService authenticationService = new AuthenticationService(_userAccountRepository);
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithUserId(oldPassword, UserID);

            if (AuthenticationToken == true)
            {


                try
                {
                    await _userAccountRepository.UpdateAccountEmail(UserID, email);
                    return "Update Succeeded";
                }
                catch
                {
                    return "Updated Failed";
                }
            }
            else
            {
                return "Authentication Failed";
            }
        }
        public async Task<string> DeleteAccountByUserIDAsync(int UserID, string password)
        {

            IAuthenticationService authenticationService = new AuthenticationService(_userAccountRepository); 
            bool AuthenticationToken = await authenticationService.AuthenticatePasswordWithUserId(password, UserID);

            if (AuthenticationToken == true)
            {


                    await _userAccountRepository.UpdateAccountStatus(UserID, "Deleted");
                    return "Update Succeeded";
           
                
            }
            else
            {
                return "Authentication Failed";
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
