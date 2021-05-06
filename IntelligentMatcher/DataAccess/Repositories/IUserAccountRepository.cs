using DataAccessLayer.CrossCuttingConcerns;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserAccountRepository
    {
        Task<Result<IEnumerable<UserAccountModel>>> GetAllAccounts();
        Task<Result<UserAccountModel>> GetAccountById(int id);
        Task<Result<UserAccountModel>> GetAccountByUsername(string username);
        Task<Result<UserAccountModel>> GetAccountByEmail(string email);
        Task<Result<string>> GetSaltById(int id);
        Task<Result<int>> CreateAccount(UserAccountModel model);
        Task<Result<bool>> DeleteAccountById(int id);
        Task<Result<bool>> UpdateAccountUsername(int id, string username);
        Task<Result<bool>> UpdateAccountPassword(int id, string password);
        Task<Result<bool>> UpdateAccountEmail(int id, string email);
        Task<Result<bool>> UpdateAccountSalt(int id, string salt);
        Task<Result<bool>> UpdateAccountStatus(int id, string accountStatus);
        Task<Result<bool>> UpdateAccountType(int id, string accountType);
        Task<Result<string>> GetPasswordById(int id);
        Task<Result<string>> GetStatusById(int id);
    }
}