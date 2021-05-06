using BusinessLayer.CrossCuttingConcerns;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using UserManagement.Models;

namespace IntelligentMatcher.Services
{
    public interface IUserAccountService
    {
        Task<Result<List<WebUserAccountModel>>> GetAllUserAccounts();
        Task<Result<WebUserAccountModel>> GetUserAccount(int id);
        Task<Result<WebUserAccountModel>> GetUserAccountByUsername(string username);
        Task<Result<WebUserAccountModel>> GetUserAccountByEmail(string emailAddress);
        Task<Result<int>> CreateAccount(WebUserAccountModel webUserAccountModel);
        Task<Result<bool>> DeleteAccount(int id);
        Task<Result<bool>> ChangeUsername(int accountId, string newUsername);
        Task<Result<bool>> ChangePassword(int accountId, string newPassword);
        Task<Result<bool>> ChangeEmail(int accountId, string newEmail);
    }
}