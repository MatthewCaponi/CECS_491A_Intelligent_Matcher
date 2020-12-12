using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Models.UserProfileModel;

namespace UserManagement.Services
{
    public static class UserAccessService
    {
        public static async Task<bool> DisableAccount(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            if (await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Disabled.ToString()) != 0)
            {
                return true;
            }

            return false; 
        }

        public static async Task<bool> EnableAccount(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            if (await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Active.ToString()) != 0)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> Suspend(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            if (await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Suspended.ToString()) != 0)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> Ban(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            if (await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Banned.ToString()) != 0)
            {
                return true;
            }

            return false;
        }
    }
}
