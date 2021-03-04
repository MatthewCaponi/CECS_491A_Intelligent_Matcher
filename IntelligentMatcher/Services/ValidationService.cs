using IntelligentMatcher.Services;
using System;
using UserManagement.Models;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Services;

namespace Services
{
    public class ValidationService
    {
        private readonly UserAccountService _userAccountService;
        private readonly UserProfileService _userProfileService;

        public ValidationService(UserAccountService userAccountService, UserProfileService userProfileService)
        {
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
        }

        //come back to this
        public bool IsNull(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UserExists(int id)
        {
            var userAccounts = await (_userAccountService.GetAllUserAccounts());
            if (userAccounts.Any(x => x.Id == id))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UsernameExists(WebUserAccountModel webUserAccountModel)
        {
            var userAccounts = await _userAccountService.GetAllUserAccounts();
            if (userAccounts.Any(x => x.Username == webUserAccountModel.Username))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EmailExists(WebUserAccountModel webUserAccountModel)
        {
            var userAccounts = await _userAccountService.GetAllUserAccounts();
            if (userAccounts.Any(x => x.Username == webUserAccountModel.Username))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UserIsActive(int id)
        {
            var userAccount = await _userAccountService.GetUserAccount(id);
            if (userAccount.AccountStatus == AccountStatus.Active.ToString())
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ListIsEmpty(Type type)
        {
            var userProfiles = await _userProfileService.GetAllUsers();
            if (userProfiles.Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
}
