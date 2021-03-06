using System;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;
using Security;
using Models;
using Security;

namespace UserAccountSettings
{
    public class AccountSettingsController : IAccountSettingsController
    {
        private readonly IUserAccountSettingsRepository _userAccountSettingRepository;

        public AccountSettingsController(IUserAccountSettingsRepository userAccountSettingRepository)
        {
            _userAccountSettingRepository = userAccountSettingRepository;

        }
        public async Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model)
        {

            await _userAccountSettingRepository.CreateUserAccountSettings(model);

            return (true);

        }

        public async Task<bool> CreateDefaultUserAccountSettings(int UserId)
        {
            UserAccountSettingsModel model = new UserAccountSettingsModel();
            model.UserId = UserId;
            model.FontSize = 12;
            model.FontStyle = "Defualt Font Style";
            model.ThemeColor = "Default Theme Color";

            await _userAccountSettingRepository.CreateUserAccountSettings(model);

            return (true);

        }
    }
}
