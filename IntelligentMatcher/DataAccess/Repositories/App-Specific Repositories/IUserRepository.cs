using Models.App_Specific_Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories.App_Specific_Repositories
{
    public interface IUserRepository
    {
        Task<UserInfoModel> GetAccountByEmail(string email);
        Task<int> UpdateAccountEmail(int id, string email);
    }
}