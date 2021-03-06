using System;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;
using Security;
using Models;
namespace UserAccountSettings
{
    public interface IAccountSettingsController

    {
        Task<bool> CreateUserAccountSettings(UserAccountSettingsModel model);
        Task<bool> CreateDefaultUserAccountSettings(int UserId);
    }
}
