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
        Task<UserAccountModel> GetAccountByEmail(string email);
        Task<string> GetSaltById(int id);
        Task<string> GetPasswordById(int id);
        Task<string> GetStatusById(int id);
        Task<int?> CreateAccount(UserAccountModel model);
        Task<bool> DeleteAccountById(int id);             
        Task<bool> UpdateAccountUsername(int id, string username);
        Task<bool> UpdateAccountPassword(int id, string password);
        Task<bool> UpdateAccountEmail(int id, string email);
        Task<bool> UpdateAccountSalt(int id, string salt);
        Task<bool> UpdateAccountStatus(int id, string accountStatus);
        Task<bool> UpdateAccountType(int id, string accountType);
    }
}