using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement
{
    public class UserManager
    {
        public async Task<bool> CreateUser(UserCreateModel model)
        {
            if (await SearchService.SearchUser(model.Username, model.email))
            {
                await UserCreationService.CreateAccount(model);
                return true;
            }

            return false;         
        }

        public async Task<bool> DeleteUser(int accountId)
        {
            if (await DeletionService.DeleteAccount(accountId))
            {
                if (await DeletionService.DeleteProfile(accountId))
                {
                    return true;
                }

                return false;
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


    }
}
