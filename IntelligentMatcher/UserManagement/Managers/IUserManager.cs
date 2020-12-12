using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement
{
    public interface IUserManager
    {
        Task<bool> BanUser(int accountId);
        Task<bool> CreateUser(UserCreateModel model);
        Task<bool> DeleteUser(int accountId);
        Task<bool> DisableUser(int accountId);
        Task<bool> EnableUser(int accountId);
        Task<bool> SuspendUser(int accountId);
    }
}