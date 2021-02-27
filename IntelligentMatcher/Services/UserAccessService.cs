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
        public async Task<bool> DisableAccount(int accountId)
        {
            try
            {
                var returned = await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Disabled.ToString());
                if (returned == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EnableAccount(int accountId)
        {
            try
            {
                var returned = await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Active.ToString());
                if (returned == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Suspend(int accountId)
        {
            try
            {
                var returned = await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Suspended.ToString());
                if (returned == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            } 
        }

        public async Task<bool> Ban(int accountId)
        {
            try
            {
                var returned = await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Banned.ToString());
                if (returned == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
