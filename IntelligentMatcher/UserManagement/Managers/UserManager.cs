﻿using DataAccess;
using DataAccess.Repositories;
using Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;
using static Models.UserProfileModel;

namespace UserManagement
{
    public class UserManager : IUserManager
    {
        ILogService _logger;
        public UserManager()
        {
            ILogServiceFactory factory = new LogSeviceFactory();
            factory.AddTarget(TargetType.Text);

            _logger = factory.CreateLogService<UserManager>();
        }

        public async Task<int> CreateUser(UserCreateModel model)
        {
            try
            {
                return await UserCreationService.CreateAccount(model);
            }
            catch(Exception e)
            {
                _logger.LogError(new UserLoggingEvent(EventName.UserEvent, "", 0, AccountType.User), e, $"Exception: {e.Message}");
                throw new Exception(e.Message, e.InnerException);
            }     
        }

        public async Task<bool> DeleteUser(int accountId)
        {
            if (await DeletionService.DeleteAccount(accountId))
            {
                return true;
            }
           
            return false;
        }

        public async Task<bool> DisableUser(int accountId)
        {
            if (await UserAccessService.DisableAccount(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> BanUser(int accountId)
        {
            if (await UserAccessService.Ban(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EnableUser(int accountId)
        {
            if (await UserAccessService.EnableAccount(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> SuspendUser(int accountId)
        {
            if (await UserAccessService.Suspend(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateUsername(int accountId, string newUsername)
        {
            if (await UserUpdateService.ChangeUsername(accountId, newUsername))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePassword(int accountId, string newPassword)
        {
            if (await UserUpdateService.ChangePassword(accountId, newPassword))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEmail(int accountId, string newEmail)
        {
            if (await UserUpdateService.ChangeEmail(accountId, newEmail))
            {
                return true;
            }

            return false;
        }


    }
}
