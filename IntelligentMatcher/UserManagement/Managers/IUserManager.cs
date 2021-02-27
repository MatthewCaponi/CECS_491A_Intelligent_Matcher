using System.Threading.Tasks;
using UserManagement.Models;

namespace IntelligentMatcher.UserManagement
{
    public interface IUserManager
    {
        Task<int> CreateUser(UserModel model);
        Task<bool> DeleteUser(int accountId);
        Task<bool> DisableUser(int accountId);
        Task<bool> EnableUser(int accountId);
        Task<bool> UpdateUsername(int accountId, string newUsername);
        Task<bool> UpdatePassword(int accountId, string newPassword);
        Task<bool> UpdateEmail(int accountId, string newEmail);
    }
}