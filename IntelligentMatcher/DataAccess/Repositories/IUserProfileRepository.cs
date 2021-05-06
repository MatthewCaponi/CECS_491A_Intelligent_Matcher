using DataAccessLayer.CrossCuttingConcerns;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserProfileRepository
    {
        Task<Result<IEnumerable<UserProfileModel>>> GetAllUserProfiles();
        Task<Result<UserProfileModel>> GetUserProfileById(int id);
        Task<Result<UserProfileModel>> GetUserProfileByAccountId(int accountId);
        Task<Result<int>> CreateUserProfile(UserProfileModel model);
        Task<Result<bool>> DeleteUserProfileByAccountId(int id);         
    }
}