using DataAccess;
using DataAccess.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Services;

namespace UserManagement
{
    public class UserListManager : IUserListManager
    {
        public async Task<List<UserListTransferModel>> PopulateListOfUsers()
        {
            return await ListFetchService.FetchUsers();

        }
    }
}
