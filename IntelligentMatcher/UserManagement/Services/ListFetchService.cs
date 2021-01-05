using DataAccess;
using DataAccess.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Services
{
    public static class ListFetchService
    {
        public static async Task<List<UserListTransferModel>> FetchUsers()
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserRepository userAccount = new UserRepository(dataGateway, connectionString);

            try
            {
                return await userAccount.GetUserList();
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public static async Task<UserAccountModel> FetchUserAccount(int id)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);

            try
            {
                return await userAccount.GetUserAccountById(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<UserProfileModel> FetchUserProfile(int id)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            try
            {
                return await userProfile.GetUserProfileByAccountId(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
