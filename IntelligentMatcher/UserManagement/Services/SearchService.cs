using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Services
{
    public static class SearchService
    {
        public static async Task<bool> SearchUser(string username, string email)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);

            var user = await userAccount.GetAccountByUsername(username);

            if (user == null)
            {
                var emailAddress = await userAccount.GetAccountByEmail(email);
                if (email == null)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
