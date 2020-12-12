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
            UserRepository userAccount = new UserRepository(dataGateway, connectionString);

            return await userAccount.GetUserList();
        }
        
    }
}
