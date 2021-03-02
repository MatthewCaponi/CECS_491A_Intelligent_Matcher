using BusinessModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace IntelligentMatcher.UserManagement
{
    public interface IUserManager
    {
        Task<Tuple<bool, ResultModel<List<WebUserAccountModel>>>> GetAllUserAccounts();
        Task<Tuple<bool, ResultModel<List<WebUserProfileModel>>>> GetAllUserProfiles();
        Task<Tuple<bool, ResultModel<WebUserAccountModel>>> GetUserAccount(int id);
        Task<Tuple<bool, ResultModel<WebUserProfileModel>>> GetUserProfile(int id);
        Task<Tuple<bool, ResultModel<int>>> CreateUser(WebUserAccountModel webUserAccountModel, WebUserProfileModel webUserProfileModel);
        Task<Tuple<bool, ResultModel<int>>> DeleteUser(int accountId);
        Task<Tuple<bool, ResultModel<int>>> DisableUser(int accountId);
        Task<Tuple<bool, ResultModel<int>>> EnableUser(int accountId);    
        Task<Tuple<bool, ResultModel<int>>> UpdateEmail(int accountId, string newEmail);
        Task<Tuple<bool, ResultModel<int>>> UpdatePassword(int accountId, string newPassword);
        Task<Tuple<bool, ResultModel<int>>> UpdateUsername(int accountId, string newUsername);
    }
}