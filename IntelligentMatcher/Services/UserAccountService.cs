using BusinessLayer.CrossCuttingConcerns;
using DataAccess.Repositories;
using Models;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace IntelligentMatcher.Services
{
    public class UserAccountService : BusinessLayerBase, IUserAccountService
    {

        private IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<Result<List<WebUserAccountModel>>> GetAllUserAccounts()
        {
            var result = ModelConverterService.ConvertTo(await _userAccountRepository.GetAllAccounts(), new Result<List<WebUserAccountModel>>());

            if (result.WasSuccessful)
            {
                var userAccounts = result.SuccessValue;

                List<WebUserAccountModel> webUserAccounts = new List<WebUserAccountModel>();
                foreach (var userAccountModel in userAccounts)
                {
                    var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new WebUserAccountModel());
                    webUserAccounts.Add(webUserAccountModel);
                }

                return Result<List<WebUserAccountModel>>.Success(webUserAccounts);
            }

            return Result<List<WebUserAccountModel>>.Failure(result.ErrorMessage.ToString());
        }


        public async Task<Result<WebUserAccountModel>> GetUserAccount(int id)
        {
            var result = ModelConverterService.ConvertTo(await _userAccountRepository.GetAccountById(id), new Result<WebUserAccountModel>());

            if (!result.WasSuccessful)
            {
                return Result<WebUserAccountModel>.Failure(result.ErrorMessage.ToString());
            }

            var userAccountModel = result.SuccessValue;

            if (IsNull(userAccountModel))
            {
                return Result<WebUserAccountModel>.Failure(ErrorMessage.NullObject.ToString());
            }

            var webUserAccountModel = ModelConverterService.ConvertTo(result.SuccessValue, new WebUserAccountModel());

            return Result<WebUserAccountModel>.Success(webUserAccountModel);
        }

        public async Task<Result<WebUserAccountModel>> GetUserAccountByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Result<WebUserAccountModel>.Failure(ErrorMessage.IsNullOrEmpty.ToString());
            }

            var result = ModelConverterService.ConvertTo(await _userAccountRepository.GetAccountByUsername(username), new Result<WebUserAccountModel>());

            if (!result.WasSuccessful)
            {
                return Result<WebUserAccountModel>.Failure(result.ErrorMessage.ToString());
            }

            var userAccountModel = result.SuccessValue;

            if (IsNull(userAccountModel))
            {
                return Result<WebUserAccountModel>.Failure(ErrorMessage.NullObject.ToString());
            }

            var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new WebUserAccountModel());
            return Result<WebUserAccountModel>.Success(webUserAccountModel);

        }

        public async Task<Result<WebUserAccountModel>> GetUserAccountByEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return Result<WebUserAccountModel>.Failure(ErrorMessage.IsNullOrEmpty.ToString());
            }

            var result = ModelConverterService.ConvertTo(await _userAccountRepository.GetAccountByUsername(emailAddress), new Result<WebUserAccountModel>());

            if (!result.WasSuccessful)
            {
                return Result<WebUserAccountModel>.Failure(result.ErrorMessage.ToString());
            }

            var userAccountModel = result.SuccessValue;

            if (IsNull(userAccountModel))
            {
                return Result<WebUserAccountModel>.Failure(ErrorMessage.NullObject.ToString());
            }

            var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new WebUserAccountModel());
            return Result<WebUserAccountModel>.Success(webUserAccountModel);
        }


        public async Task<Result<int>> CreateAccount(WebUserAccountModel webUserAccountModel)
        {
            if (IsNull(webUserAccountModel))
            {
                return Result<int>.Failure(ErrorMessage.NullObject.ToString());
            }

            if (ContainsNullOrEmptyParameter(webUserAccountModel))
            {
                return Result<int>.Failure(ErrorMessage.ContainsNullOrEmptyParameters.ToString());
            }

            var userAccountModel = ModelConverterService.ConvertTo(webUserAccountModel, new UserAccountModel());

            var result = ModelConverterService.ConvertTo(await _userAccountRepository.CreateAccount(userAccountModel), new Result<int>());

            if (!result.WasSuccessful)
            {
                return Result<int>.Failure(result.ErrorMessage.ToString());
            }

            return Result<int>.Success(result.SuccessValue);
        }

        public async Task<Result<bool>> DeleteAccount(int id)
        {
            var result = ModelConverterService.ConvertTo(await _userAccountRepository.DeleteAccountById(id), new Result<bool>());

            if (!result.WasSuccessful)
            {
                return Result<bool>.Failure(result.ErrorMessage.ToString());
            }

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ChangeUsername(int accountId, string newUsername)
        {
            var result = ModelConverterService.ConvertTo(await _userAccountRepository.UpdateAccountUsername(accountId, newUsername), new Result<bool>());

            if (!result.WasSuccessful)
            {
                return Result<bool>.Failure(result.ErrorMessage.ToString());
            }

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ChangePassword(int accountId, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                return Result<bool>.Failure(ErrorMessage.IsNullOrEmpty.ToString());
            }

            var result = ModelConverterService.ConvertTo(await _userAccountRepository.UpdateAccountPassword(accountId, newPassword), new Result<bool>());

            if (!result.WasSuccessful)
            {
                return Result<bool>.Failure(result.ErrorMessage.ToString());
            }

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ChangeEmail(int accountId, string newEmail)
        {
            if (string.IsNullOrEmpty(newEmail))
            {
                return Result<bool>.Failure(ErrorMessage.IsNullOrEmpty.ToString());
            }

            var result = ModelConverterService.ConvertTo(await _userAccountRepository.UpdateAccountEmail(accountId, newEmail), new Result<bool>());

            if (!result.WasSuccessful)
            {
                return Result<bool>.Failure(result.ErrorMessage.ToString());
            }

            return Result<bool>.Success(true);
        }
    }
}
