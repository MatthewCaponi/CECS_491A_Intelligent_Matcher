using DataAccess;
using DataAccess.Repositories;
using Models;
using System;
using System.Threading.Tasks;
using UserInterface.WebUI.Models;
using UserManagement.Models;
using static Models.UserProfileModel;

namespace UserManagement.Services
{
    public static class UserCreationService
    {
        public static async Task<int> CreateAccount(UserCreateModel model)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            UserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            UserAccountModel userAccount = new UserAccountModel(model.Username, model.Password, model.email);
            var userAccountId = await userAccountRepository.CreateUserAccount(userAccount);

            UserProfileModel userProfileModel = 
                new UserProfileModel(model.FirstName, model.LastName, DateTime.Parse(model.DateOfBirth), 
                DateTime.Now, model.accountType.ToString(), "Active", userAccountId);

            return userAccountId;
        }
            
    }
}
