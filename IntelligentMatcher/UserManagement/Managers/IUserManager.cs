using BusinessModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace IntelligentMatcher.UserManagement
{
    public interface IUserManager
    {
        Task<Result<List<WebUserAccountModel>>> GetAllUserAccounts();
        Task<Result<List<WebUserProfileModel>>> GetAllUserProfiles();
        Task<Result<WebUserAccountModel>> GetUserAccount(int id);
        Task<Result<WebUserProfileModel>> GetUserProfile(int id);
        Task<Result<int>> CreateUser(WebUserAccountModel webUserAccountModel, WebUserProfileModel webUserProfileModel);
        Task<Tuple<bool, ErrorMessage>> DeleteUser(int accountId);
        Task<Tuple<bool, ErrorMessage>> DisableUser(int accountId);
        Task<Tuple<bool, ErrorMessage>> EnableUser(int accountId);
        Task<Tuple<bool, ErrorMessage>> UpdateEmail(int accountId, string newEmail);
        Task<Tuple<bool, ErrorMessage>> UpdatePassword(int accountId, string newPassword);
        Task<Tuple<bool, ErrorMessage>> UpdateUsername(int accountId, string newUsername);
    }
}