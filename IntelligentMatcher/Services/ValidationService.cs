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
        private readonly WebUserAccountModel _webUserAccountModel;

        public ValidationService(UserAccountService userAccountService, WebUserAccountModel webUserAccountModel)
        {
            _userAccountService = userAccountService;
            _webUserAccountModel = webUserAccountModel;
        }

        public bool IsNull(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UsernameExists()
        {
            var userAccounts = await _userAccountService.GetAllUserAccounts();
            if (userAccounts.Any(x => x.Username == _webUserAccountModel.Username))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EmailExists()
        {
            var userAccounts = await _userAccountService.GetAllUserAccounts();
            if (userAccounts.Any(x => x.Username == _webUserAccountModel.Username))
            {
                return true;
            }

            return false;
        }
    }
}
