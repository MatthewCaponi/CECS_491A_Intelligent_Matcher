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

            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);
            
            try
            {
                int returnValue =  await userAccount.DeleteUserAccountById(id);
                if (returnValue == 0)
                {
                    return false;
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }


        }

        public static async Task<bool> DeleteProfile(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();

            IUserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            try
            {
                await userProfile.DeleteUserProfileById(accountId);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}
