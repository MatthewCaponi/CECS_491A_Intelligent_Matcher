using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement
{
    public class UserManager
    {
        public async Task<bool> CreateUser(UserCreateModel model)
        {
            if (await SearchService.SearchUser(model.Username, model.email))
            {
                await UserCreationService.CreateAccount(model);
                return true;
            }

            return false;         
        }
    }
}
