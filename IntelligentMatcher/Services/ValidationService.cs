using IntelligentMatcher.Services;
using System;
using UserManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ValidationService
    {
        private readonly UserAccountService _userAccountService;

        public ValidationService(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

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
    }
}
