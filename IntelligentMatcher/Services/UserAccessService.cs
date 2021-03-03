using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using static Models.UserProfileModel;

namespace IntelligentMatcher.Services
{
    public class UserAccessService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccessService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<bool> ChangeAccountStatus(int accountId, AccountStatus accountStatus)
        {
            var returned = await _userAccountRepository.UpdateAccountStatus(accountId, accountStatus.ToString());
            return true;
        }
    }
}
