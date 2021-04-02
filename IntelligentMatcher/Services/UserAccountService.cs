using DataAccess.Repositories;
using Models;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<WebUserAccountModel> GetUserAccount(int id)
        {
            var userAccountModel = await _userAccountRepository.GetAccountById(id);
            var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new WebUserAccountModel());

            return webUserAccountModel;
        }

        public async Task<WebUserAccountModel> GetUserAccountByUsername(string username)
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

        public async Task<WebUserAccountModel> GetUserAccountByEmail(string emailAddress)
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

        public async Task<int> CreateAccount(WebUserAccountModel webUserAccountModel)
        {
            var userAccountModel = ModelConverterService.ConvertTo(webUserAccountModel, new UserAccountModel());
            var userAccountId = await _userAccountRepository.CreateAccount(userAccountModel);

            return userAccountId;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            int returnValue = await _userAccountRepository.DeleteAccountById(id);

            return true;
        }

        public async Task<bool> ChangeUsername(int accountId, string newUsername)
        {
            var returned = await _userAccountRepository.UpdateAccountUsername(accountId, newUsername);
            return true;
        }

        public async Task<bool> ChangePassword(int accountId, string newPassword)
        {
            var returned = await _userAccountRepository.UpdateAccountPassword(accountId, newPassword);
            return true;
        }

        public async Task<bool> ChangeEmail(int accountId, string newEmail)
        {
            var returned = await _userAccountRepository.UpdateAccountEmail(accountId, newEmail);
            return true;
        }
    }
}
