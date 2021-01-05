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
        public async Task<List<UserListModel>> PopulateListOfUsers()
        {
            var users = await ListFetchService.FetchUsers();
            List<UserListModel> userDtos = new List<UserListModel>();
            foreach(UserListTransferModel user in users)
            {
                UserListModel userDto = new UserListModel(user.Id, user.Username, user.FirstName, user.LastName, user.AccountCreationDate, user.accountStatus);
                userDtos.Add(userDto);
            }

            return userDtos;
        }
    }
}
