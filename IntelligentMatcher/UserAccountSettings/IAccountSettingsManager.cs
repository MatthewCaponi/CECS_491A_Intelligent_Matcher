using System;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;
using Security;
using Models;
namespace UserAccountSettings
{
    public interface IAccountSettingsManager
    {


        Task<bool> ChangePassword(string oldPassword, string newPassword, int userID);
        Task<bool> ChangeEmail(string oldPassword, string email, int userID);
        Task<bool> DeleteAccountByUserIDAsync(int userID, string password);
        Task<bool> ChangeFontSize(int userID, int fontSize);

        Task<bool> ChangeThemeColor(int userID, string themeColor);


        Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model);
        Task<bool> CreateDefaultUserAccountSettings(UserAccountSettingsModel model);
        Task<bool> ChangeFontStyleAsync(int userID, string fontStyle);



    }
}
