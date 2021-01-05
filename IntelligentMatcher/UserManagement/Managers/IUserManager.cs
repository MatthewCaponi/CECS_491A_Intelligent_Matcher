using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement
{
    public interface IUserManager
    {
        Task<bool> BanUser(int accountId);
        Task<int> CreateUser(UserCreateModel model);
        Task<bool> DeleteUser(int accountId);
        Task<bool> DisableUser(int accountId);
        Task<bool> EnableUser(int accountId);
        Task<bool> SuspendUser(int accountId);
        Task<bool> UpdateUsername(int accountId, string newUsername);
        Task<bool> UpdatePassword(int accountId, string newPassword);
        Task<bool> UpdateEmail(int accountId, string newEmail);
        Task<UserInfoModel> GetUserInfo(int id);


    }
}