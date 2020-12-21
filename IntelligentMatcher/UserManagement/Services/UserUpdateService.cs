using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Models.UserProfileModel;

namespace UserManagement.Services
{
    public class UserUpdateService
    {
        public static async Task<bool> ChangeUsername(int accountId, string newUsername)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);

            try
            {
                var returned = await userAccount.UpdateAccountUsername(accountId, newUsername);
                var user = userAccount.GetUserAccountById(accountId);
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

        public static async Task<bool> ChangePassword(int accountId, string newPassword)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);

            try
            {
                var returned = await userAccount.UpdateAccountPassword(accountId, newPassword);
                var user = userAccount.GetUserAccountById(accountId);
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

        public static async Task<bool> ChangeEmail(int accountId, string newEmail)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);

            try
            {
                var returned = await userAccount.UpdateAccountEmail(accountId, newEmail);
                var user = userAccount.GetUserAccountById(accountId);
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
