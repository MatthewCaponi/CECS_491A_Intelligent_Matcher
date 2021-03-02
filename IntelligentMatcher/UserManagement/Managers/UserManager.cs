using System;
using System.Threading.Tasks;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services;
using IntelligentMatcher.Services;

namespace IntelligentMatcher.UserManagement
{
    public class UserManager : IUserManager
    {
        private readonly UserService _userService;
        private readonly UserAccountService _userAccountService;
        private readonly UserAccessService _userAccessService;

        public UserManager(UserService userService, UserAccountService userAccountService, UserAccessService userAccessService)
        {
            _userService = userService;
            _userAccountService = userAccountService;
            _userAccessService = userAccessService;
        }

        public async Task<WebUserProfileModel> GetUser(int id)
        {
            
            var userProfileModel = await _userService.GetUser(id);

        }

        public async Task<int> CreateUser(WebUserAccountModel accountModel, WebUserProfileModel userModel)
        {
            var users = _userAccountService.GetAllUserAccounts();
            if (users.Any(x => users.Username == x.Username))
            {

            }
            try
            {
                var accountCreated = await _userAccountService.CreateAccount(accountModel);
                if (accountCreated)
                {
                    return await _userService.CreateUser(userModel);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }     
        }

        public async Task<bool> DeleteUser(int accountId)
        {
            if (await DeletionService.DeleteAccount(accountId))
            {
                return true;
            }
           
            return false;
        }

        public async Task<bool> DisableUser(int accountId)
        {
            if (await UserAccessService.DisableAccount(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EnableUser(int accountId)
        {
            if (await UserAccessService.EnableAccount(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateUsername(int accountId, string newUsername)
        {
            if (await UserUpdateService.ChangeUsername(accountId, newUsername))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePassword(int accountId, string newPassword)
        {
            if (await UserUpdateService.ChangePassword(accountId, newPassword))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEmail(int accountId, string newEmail)
        {
            if (await UserUpdateService.ChangeEmail(accountId, newEmail))
            {
                return true;
            }

            return false;
        }
    }
