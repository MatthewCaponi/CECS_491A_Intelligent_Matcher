using DataAccess.Repositories;
using Exceptions;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UserManagement.Models;

namespace IntelligentMatcher.Services
{
    public class UserAccountService : IUserAccountService
    {

        private IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

 
        public async Task<List<WebUserAccountModel>> GetAllUserAccounts()
        {

                var userAccounts = await _userAccountRepository.GetAllAccounts();
                List<WebUserAccountModel> webUserAccounts = new List<WebUserAccountModel>();
                foreach (var userAccountModel in userAccounts)
                {
                    var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new WebUserAccountModel());
                    webUserAccounts.Add(webUserAccountModel);
                }

                return webUserAccounts;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }

        public async Task<WebUserAccountModel> GetUserAccount(int id)
        {
            try
            {
                var userAccountModel = await _userAccountRepository.GetAccountById(id);
                var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new WebUserAccountModel());

                return webUserAccountModel;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }

        public async Task<WebUserAccountModel> GetUserAccountByUsername(string username)
        {
            try
            {
                var userAccountModel = await _userAccountRepository.GetAccountByUsername(username);
                if (userAccountModel == null)
                {
                    return null;
                }
                else
                {
                    var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new WebUserAccountModel());
                    return webUserAccountModel;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }

        public async Task<WebUserAccountModel> GetUserAccountByEmail(string emailAddress)
        {
            try
            {
                var userAccountModel = await _userAccountRepository.GetAccountByEmail(emailAddress);
                if (userAccountModel == null)
                {
                    return null;
                }
                else
                {
                    var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new WebUserAccountModel());
                    return webUserAccountModel;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }


        public async Task<int> CreateAccount(WebUserAccountModel webUserAccountModel)
        {
            try
            {
                var userAccountModel = ModelConverterService.ConvertTo(webUserAccountModel, new UserAccountModel());
                var userAccountId = await _userAccountRepository.CreateAccount(userAccountModel);

                return userAccountId;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> DeleteAccount(int id)
        {
            try
            {
                int returnValue = await _userAccountRepository.DeleteAccountById(id);

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> ChangeUsername(int accountId, string newUsername)
        {
            try
            {
                var returned = await _userAccountRepository.UpdateAccountUsername(accountId, newUsername);
                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> ChangePassword(int accountId, string newPassword)
        {
            try
            {
                var returned = await _userAccountRepository.UpdateAccountPassword(accountId, newPassword);
                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> ChangeEmail(int accountId, string newEmail)
        {
            try
            {
                var returned = await _userAccountRepository.UpdateAccountEmail(accountId, newEmail);
                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }
    }
}
