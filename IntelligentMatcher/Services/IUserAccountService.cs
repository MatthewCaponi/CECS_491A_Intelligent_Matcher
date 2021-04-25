using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using UserManagement.Models;

namespace IntelligentMatcher.Services
{
    public interface IUserAccountService
    {
        Task<bool> ChangeEmail(int accountId, string newEmail);
        Task<bool> ChangePassword(int accountId, string newPassword);
        Task<bool> ChangeUsername(int accountId, string newUsername);
        Task<int> CreateAccount(WebUserAccountModel webUserAccountModel);
        Task<bool> DeleteAccount(int id);
        Task<List<WebUserAccountModel>> GetAllUserAccounts();
        Task<WebUserAccountModel> GetUserAccount(int id);
        Task<WebUserAccountModel> GetUserAccountByUsername(string username);
        Task<WebUserAccountModel> GetUserAccountByEmail(string emailAddress);

    }
}