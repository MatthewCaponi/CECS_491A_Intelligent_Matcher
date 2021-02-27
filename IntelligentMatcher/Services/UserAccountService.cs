using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.App_Specific_Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace IntelligentMatcher.Services
{
    public class UserAccountService
    {
        private readonly IUserRepository _userRepository;

        public UserAccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserAccountModel> GetUserAccount(UserAccountModel userAccountModel, int id)
        {
            try
            {
                return await userAccountModel.GetUserAccountById(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async List<UserAccountModel> GetAllUserAccounts()
        {
            return Task.FromResult(_userRepository.GetAllAccounts());
        }

        public async Task<int> CreateAccount(UserAccountModel userAccountModel)
        {
            UserAccountModel userAccount = new UserAccountModel(userAccountModel.Username, userAccountModel.Password, userAccountModel.EmailAddress);

            try
            {
                var userAccountId = await userAccountRepository.CreateUserAccount(userAccount);
                UserProfileModel userProfileModel =
                new UserProfileModel(userAccountModel.FirstName, userAccountModel.Surname, DateTime.Parse(userAccountModel.DateOfBirth),
                userAccountModel.AccountCreationDate, userAccountModel.AccountType.ToString(), userAccountModel.AccountStatus, userAccountId);
                await userProfileRepository.CreateUserProfile(userProfileModel);

                return userAccountId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<bool> DeleteAccount(int id)
        {

            try
            {
                int returnValue = await userAccount.DeleteUserAccountById(id);
                if (returnValue == 0)
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

        public async Task<bool> ChangeUsername(int accountId, string newUsername)
        {
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

        public async Task<bool> ChangePassword(int accountId, string newPassword)
        {
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

        public async Task<bool> ChangeEmail(int accountId, string newEmail)
        {
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
