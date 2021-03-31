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
            try
            {
                // Returns the result from deleting the account after the link expires
                // Conditionals check if the profile and account were deleted
                if (await _userProfileService.DeleteProfile(accountId))
                {
                    if(await _userAccountService.DeleteAccount(accountId))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> VerifyEmail(int accountId)
        {
            try
            {
                // Returns the result from changing the Account after verifying the email
                // Conditional checks if the change was successful
                var accountStatusHasChangedToActive = await _userAccessService.ChangeAccountStatus(accountId,
                    AccountStatus.Active);

                if (accountStatusHasChangedToActive)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
