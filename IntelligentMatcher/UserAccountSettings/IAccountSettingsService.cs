using System;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;
using Security;
using Models;
namespace UserAccountSettings
{
    public interface IAccountSettingsService
    {


        Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, int userID);
        Task<bool> ChangeEmailAsync(string oldPassword, string email, int userID);
        Task<bool> DeleteAccountByUserIDAsync(int userID, string password);
        Task<bool> ChangeFontSizeAsync(int userID, int fontSize);

        Task<bool> ChangeThemeColorAsync(int userID, string themeColor);


        Task<bool> CreateUserAccountSettingsAsync(UserAccountSettingsModel model);
        Task<bool> CreateDefaultUserAccountSettingsAsync(UserAccountSettingsModel model);
        Task<bool> ChangeFontStyleAsync(int userID, string fontStyle);



    }
}
