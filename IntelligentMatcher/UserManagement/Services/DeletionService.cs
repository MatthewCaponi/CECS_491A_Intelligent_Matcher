using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Models.UserProfileModel;

namespace UserManagement.Services
{
    public static class DeletionService
    {
        public static async Task<bool> DeleteAccount(int id)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();

            UserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            if (await userAccount.DeleteUserAccountById(id) != 0)
            {
                await userProfile.UpdateUserAccountStatus(id, AccountStatus.Deleted.ToString());
                return true;
            }

            return false;
        }

        public static async Task<bool> DeleteProfile(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();

            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            if (await userProfile.DeleteUserProfileById(accountId) != 0)
            {
                return true;
            }

            return false;
        }

    }
}
