using Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserAccountRepository
    {
        Task<int> CreateUserAccount(UserAccountModel model);
        Task<int> DeleteUserAccountById(int id);
        Task<UserAccountModel> GetAccountByEmail(string email);
        Task<UserAccountModel> GetAccountByUsername(string username);
        Task<UserAccountModel> GetUserAccountById(int id);
        Task<int> UpdateAccountEmail(int id, string email);
        Task<int> UpdateAccountPassword(int id, string password);
        Task<int> UpdateAccountUsername(int id, string username);
    }
}