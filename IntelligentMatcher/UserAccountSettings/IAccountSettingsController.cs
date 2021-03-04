using System;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;
using Security;
using Models;
namespace UserAccountSettings
{

    public class IAccountSettingsController : IAccountSettingsManager
    {
        public async Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);





            await userAccountSettingsRepository.CreateUserAccountSettings(model);

            return (true);
            
        }

        public async Task<bool> CreateDefaultUserAccountSettings(int UserId)
        {
            UserAccountSettingsModel model = new UserAccountSettingsModel();
            model.UserId = UserId;
            model.FontSize = 12;
            model.FontStyle = "Defualt Font Style";
            model.ThemeColor = "Default Theme Color";
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);





            await userAccountSettingsRepository.CreateUserAccountSettings(model);

            return (true);

        }
        public async Task<string> ChangePassword(string oldPassword, string newPassword, int UserID)
        {
            bool authenticated = true;//add here when I get autheniticator done

            if (authenticated == true)
            {
                IDataGateway dataGateway = new DataGateway();
                IConnectionStringData connectionString = new ConnectionStringData();
                IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

                try
                {
                    ICryptographyService cryptographyService = new CryptographyService();
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
            bool authenticated = true;//add here when I get autheniticator done

            if (authenticated == true)
            {
                IDataGateway dataGateway = new DataGateway();
                IConnectionStringData connectionString = new ConnectionStringData();
                IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

                try
                {
                    await userAccountRepository.UpdateAccountEmail(UserID, email);
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
            bool authenticated = true;//add here when I get autheniticator done

            if (authenticated == true)
            {
                IDataGateway dataGateway = new DataGateway();
                IConnectionStringData connectionString = new ConnectionStringData();
                IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

                try
                {
                    await userAccountRepository.UpdateAccountStatus(UserID, "Deleted");
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
        public async Task<bool> ChangeFontSize(int UserID, int FontSize)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);

        
                await userAccountSettingsRepository.UpdateFontSize(UserID, FontSize);
                return true;
       
        }

        public async Task<bool> ChangeThemeColor(int UserID, string ThemeColor)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);

       
                await userAccountSettingsRepository.UpdateThemeColor(UserID, ThemeColor);
                return true;
     
        }

        public async Task<bool> ChangeFontStyleAsync(int UserID, string FontStyle)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);

            try
            {
                await userAccountSettingsRepository.UpdateFontStyle(UserID, FontStyle);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
