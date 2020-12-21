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

            try
            {
                await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Disabled.ToString());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async Task<bool> EnableAccount(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            try
            {
                await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Active.ToString());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async Task<bool> Suspend(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            try
            {
                await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Suspended.ToString());
                return true;
            }
            catch (Exception e)
            {
                return false;
            } 
        }

        public static async Task<bool> Ban(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            try
            {
                await userProfile.UpdateUserAccountStatus(accountId, AccountStatus.Banned.ToString());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
