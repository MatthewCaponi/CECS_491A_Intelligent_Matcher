using IntelligentMatcher.Services;
using System;
using UserManagement.Models;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Services;


namespace Services
{
    public class ValidationService : IValidationService
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IUserProfileService _userProfileService;
     
        

        public ValidationService(IUserAccountService userAccountService, IUserProfileService userProfileService)
        {
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
        }

        //come back to this
        public bool IsNull(object obj)
        {
            if (!(obj is null))
            {
                return true;
            }

            return false;
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

        public async Task<bool> UsernameExists(string username)
        {
            var userAccounts = await _userAccountService.GetAllUserAccounts();
            if (userAccounts.Any(x => x.Username == username))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EmailExists(string emailAddress)
        {
            var userAccounts = await _userAccountService.GetAllUserAccounts();
            if (userAccounts.Any(x => x.EmailAddress == emailAddress))
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

       
       
    }
}
