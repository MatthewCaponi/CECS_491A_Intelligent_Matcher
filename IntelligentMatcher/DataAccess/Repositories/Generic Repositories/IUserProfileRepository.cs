using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfileModel>> GetAllUserProfiles();
        Task<UserProfileModel> GetUserProfileById(int id);
        Task<UserProfileModel> GetUserProfileByAccountId(int accountId);
        Task<int> CreateUserProfile(UserProfileModel model);
        Task<int> DeleteUserProfile(int id);         
    }
}