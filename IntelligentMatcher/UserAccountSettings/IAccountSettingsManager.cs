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


        Task<bool> ChangePassword(string oldPassword, string newPassword, int UserID);
        Task<bool> ChangeEmail(string oldPassword, string email, int UserID);
        Task<bool> DeleteAccountByUserIDAsync(int UserID, string password);
        Task<bool> ChangeFontSize(int UserID, int FontSize);

        Task<bool> ChangeThemeColor(int UserID, string ThemeColor);


        Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model);
        Task<bool> CreateDefaultUserAccountSettings(int UserId, int fontSize, string fontStyle, string themeColor);
        Task<bool> ChangeFontStyleAsync(int UserID, string FontStyle);



    }
}
