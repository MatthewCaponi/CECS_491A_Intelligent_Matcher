using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.App_Specific_Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public static async Task<List<Models.UserProfileModel>> GetAllUsers()
        {
            try
            {
                return await userAccount.GetUserList();
            }
            catch (Exception e)
            {
                return null;
            }        
        }     

        public async Task<global::Models.UserProfileModel> GetUser(int id)
        {

            try
            {
                return await userProfile.GetUserProfileByAccountId(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<int> CreateUser(Models.UserProfileModel model)
        {
            try
            {
                var userAccountId = await userAccountRepository.CreateUserAccount(userAccount);
                global::Models.UserProfileModel userProfileModel =
                new global::Models.UserProfileModel(model.FirstName, model.Surname, DateTime.Parse(model.DateOfBirth),
                model.AccountCreationDate, model.AccountType.ToString(), model.AccountStatus, userAccountId);
                await userProfileRepository.CreateUserProfile(userProfileModel);

                return userAccountId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<bool> DeleteProfile(int accountId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();

            IUserProfileRepository userProfile = new UserProfileRepository(dataGateway, connectionString);

            try
            {
                await userProfile.DeleteUserProfile(accountId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
