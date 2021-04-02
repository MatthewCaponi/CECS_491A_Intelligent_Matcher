using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IUserProfileService
    {
        Task<int> CreateUserProfile(WebUserProfileModel webUserProfileModel);
        Task<bool> DeleteProfile(int accountId);
        Task<List<WebUserProfileModel>> GetAllUsers();
        Task<WebUserProfileModel> GetUserProfile(int id);
        Task<WebUserProfileModel> GetUserProfileByAccountId(int accountId);
    }
}