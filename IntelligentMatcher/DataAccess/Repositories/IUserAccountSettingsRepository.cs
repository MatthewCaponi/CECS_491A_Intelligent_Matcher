using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserAccountSettingsRepository
    {

        Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model);
        Task<UserAccountSettingsModel> GetUserAccountSettingsByUserId(int userId);
        Task<int> DeleteUserAccountSettingsByUserId(int userId);
        Task<IEnumerable<UserAccountSettingsModel>> GetAllSettings();
        Task<int> UpdateFontSize(int id, int fontSize);
        Task<int> UpdateFontStyle(int id, string fontStyle);

        Task<int> UpdateThemeColor(int id, string themeColor);

        Task<string> GetThemeColorByID(int id);
        Task<string> GetFontStyleByID(int id);

        Task<string> GetFontSizeByID(int id);
    }
}
