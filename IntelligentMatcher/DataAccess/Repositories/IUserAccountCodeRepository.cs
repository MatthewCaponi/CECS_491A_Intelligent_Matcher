using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserAccountCodeRepository
    {
        Task<int> CreateUserAccountCode(UserAccountCodeModel model);
        Task<int> DeleteUserAccountCodeById(int id);
        Task<int> DeleteUserAccountCodeByAccountId(int accountId);
        Task<IEnumerable<UserAccountCodeModel>> GetAllUserAccountCodes();
        Task<UserAccountCodeModel> GetUserAccountCodeById(int id);
        Task<UserAccountCodeModel> GetUserAccountCodeByAccountId(int accountId);
    }
}
