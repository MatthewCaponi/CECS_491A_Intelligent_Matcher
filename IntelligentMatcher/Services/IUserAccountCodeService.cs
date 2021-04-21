using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserAccountCodeService
    {
        Task<bool> AddCode(string code, DateTimeOffset expirationTime, int accountId);
        Task<bool> UpdateCodeByAccountId(string code, DateTimeOffset expirationTime, int accountId);
        Task<bool> DeleteCodeByAccountId(int accountId);
        Task<BusinessUserAccountCodeModel> GetUserAccountCodeByAccountId(int accountId);
    }
}
