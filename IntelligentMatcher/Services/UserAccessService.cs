using DataAccess.Repositories;
using System.Threading.Tasks;
using UserManagement.Models;

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
