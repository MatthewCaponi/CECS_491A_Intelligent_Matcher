using Logging;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Services;

namespace Registration
{
    public class VerificationManager : IVerificationManager
    {
        ILogService _logger;

        public VerificationManager()
        {
            ILogServiceFactory factory = new LogSeviceFactory();
            factory.AddTarget(TargetType.Text);

            _logger = factory.CreateLogService<VerificationManager>();
        }

        public async Task<bool> LinkExpired(int accountId)
        {
            // Place Holder
            if (await webUserAccountService.DeleteAccount(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> VerifyEmail(int AccountId)
        {
            // Place Holder
            if (await UserAccessService.EnableAccount(accountId))
            {
                return true;
            }

            return false;
        }
    }
}
