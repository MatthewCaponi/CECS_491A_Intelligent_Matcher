using IntelligentMatcher.Services;
using Logging;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Services;
using UserManagement.Models;
using System.Threading.Tasks;

namespace Registration
{
    public class VerificationManager : IVerificationManager
    {
        ILogService _logger;
        private UserAccountService _userAccountService;
        private UserProfileService _userProfileService;
        private UserAccessService _userAccessService;

        public VerificationManager(UserAccountService userAccountService, UserProfileService userProfileService, 
            UserAccessService userAccessService)
        {
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
            _userAccessService = userAccessService;

            ILogServiceFactory factory = new LogSeviceFactory();
            factory.AddTarget(TargetType.Text);

            _logger = factory.CreateLogService<VerificationManager>();
        }

        public async Task<bool> LinkExpired(int accountId)
        {
            // Returns the result from deleting the account after the link expires
            if (await _userAccountService.DeleteAccount(accountId)) 
            {
                await _userProfileService.DeleteProfile(accountId);
                return true;
            }

            return false;
        }

        public async Task<bool> VerifyEmail(int accountId)
        {
            // Returns the result from changing the Account after verifying the email
            if (await _userAccessService.ChangeAccountStatus(accountId, AccountStatus.Active))
            {
                return true;
            }

            return false;
        }
    }
}
