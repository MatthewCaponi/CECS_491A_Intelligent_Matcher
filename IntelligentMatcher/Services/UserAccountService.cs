using DataAccess.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace IntelligentMatcher.Services
{
    public class UserAccountService
    {
        private IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<WebUserAccountModel> GetUserAccount(int id)
        {
            var userAccountModel = await _userAccountRepository.GetAccountById(id);
            var webUserAccountModel = new WebUserAccountModel();
            var propOne = userAccountModel.GetType().GetProperties();
            foreach (var item in propOne)
            {
                webUserAccountModel.GetType().GetProperty(item.Name).SetValue(item.GetValue(userAccountModel, null), null);
            }

            return webUserAccountModel;        
        }

        public async Task<List<WebUserAccountModel>> GetAllUserAccounts()
        {
            var userAccounts = await _userAccountRepository.GetAllAccounts();

            var webUserAccounts = userAccounts.Select(x => new WebUserAccountModel()
            {
                Id = x.Id,
                Username = x.Username,
                EmailAddress = x.EmailAddress,
                AccountType = x.AccountType,
                AccountStatus = x.AccountStatus,
                CreationDate = x.CreationDate,
                UpdationDate = x.UpdationDate
            }).ToList();

            return webUserAccounts;
        }

        public async Task<int> CreateAccount(WebUserAccountModel webUserAccountModel)
        {
            var propOne = webUserAccountModel.GetType().GetProperties();
            var userAccountModel = new UserAccountModel();
            foreach (var item in propOne)
            {
                userAccountModel.GetType().GetProperty(item.Name).SetValue(item.GetValue(webUserAccountModel, null), null);
            }

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
