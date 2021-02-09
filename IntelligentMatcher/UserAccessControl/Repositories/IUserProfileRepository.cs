using Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserProfileRepository
    {
        Task<int> CreateUserProfile(UserProfileModel model);
        Task<int> DeleteUserProfileById(int id);
        Task<UserProfileModel> GetUserProfileByAccountId(int accountId);
        Task<UserProfileModel> GetUserProfileById(int id);
        Task<int> UpdateUserAccountStatus(int id, UserProfileModel.AccountStatus accountStatus);
        Task<int> UpdateUserAccountType(int id, UserProfileModel.AccountType accountType);
    }
}