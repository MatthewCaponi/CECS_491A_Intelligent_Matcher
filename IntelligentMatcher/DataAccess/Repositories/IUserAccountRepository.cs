using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserAccountRepository
    {
        Task<IEnumerable<UserAccountModel>> GetAllAccounts();
        Task<UserAccountModel> GetAccountById(int id);
        Task<UserAccountModel> GetAccountByUsername(string username);
        Task<UserProfileModel> GetAccountByEmail(string email);
        Task<string> GetSaltById(int id);
        Task<int> CreateAccount(UserAccountModel model);
        Task<int> DeleteAccountById(int id);             
        Task<int> UpdateAccountUsername(int id, string username);
        Task<int> UpdateAccountPassword(int id, string password);
        Task<int> UpdateAccountEmail(int id, string email);
        Task<int> UpdateAccountSalt(int id, string salt);
        Task<int> UpdateAccountStatus(int id, string accountStatus);
        Task<int> UpdateAccountType(int id, string accountType);
    }
}