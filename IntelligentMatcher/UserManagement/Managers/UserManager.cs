using System;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;
using IntelligentMatcher.Services;
using BusinessModels;
using Services;
using System.Collections.Generic;

namespace IntelligentMatcher.UserManagement
{
    public class UserManager : IUserManager
    {
        private IUserAccountService _userAccountService;
        private IUserProfileService _userProfileService;
        private IUserAccessService _userAccessService;
        private readonly IValidationService _validationService;

        public UserManager(IUserAccountService userAccountService, IUserProfileService userProfileService,
            IUserAccessService userAccessService, IValidationService validationService)
        {
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
            _userAccessService = userAccessService;
            _validationService = validationService;
        }

        public async Task<Result<WebUserProfileModel>> GetUserProfile(int id)
        {
            var result = new Result<WebUserProfileModel>();
            if (!await _validationService.UserExists(id))
            {
                result.Success = false;
                result.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return result;
            }

            result.Success = true;
            result.SuccessValue = await _userProfileService.GetUserProfile(id);
            return result;
        }

        public async Task<Result<List<WebUserProfileModel>>> GetAllUserProfiles()
        {
            var result = new Result<List<WebUserProfileModel>>();

            result.Success = true;
            result.SuccessValue = await _userProfileService.GetAllUsers();
            return result;
        }

        public async Task<Result<WebUserAccountModel>> GetUserAccount(int id)
        {
            var result = new Result<WebUserAccountModel>();
            if (!await _validationService.UserExists(id))
            {
                result.Success = false;
                result.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return result;
            }

            result.Success = true;
            result.SuccessValue = await _userAccountService.GetUserAccount(id);
            return result;
        }

        public async Task<Result<List<WebUserAccountModel>>> GetAllUserAccounts()
        {
            var result = new Result<List<WebUserAccountModel>>();

            result.Success = true;
            result.SuccessValue = await _userAccountService.GetAllUserAccounts();
            return result;
        }

        public async Task<Result<int>> CreateUser(WebUserAccountModel webUserAccountModel, WebUserProfileModel webUserProfileModel)
        {
            var result = new Result<int>();
            result.Success = false;
            if (_validationService.IsNull(webUserAccountModel))
            {
                result.ErrorMessage = ErrorMessage.Null;
                return result;
            }
            if (await _validationService.UsernameExists(webUserAccountModel.Username))
            {
                result.ErrorMessage = ErrorMessage.UsernameExists;
                return result;
            }
            if (await _validationService.EmailExists(webUserAccountModel.EmailAddress))
            {
                result.ErrorMessage = ErrorMessage.EmailExists;
                return result;
            }

            var userAccountID = await _userAccountService.CreateAccount(webUserAccountModel);
            webUserProfileModel.UserAccountId = userAccountID;
            await _userProfileService.CreateUserProfile(webUserProfileModel);

            result.Success = true;
            result.SuccessValue = userAccountID;
            return result;
        }

        public async Task<Tuple<bool, ErrorMessage>> DeleteUser(int accountId)
        {
            if (!await _validationService.UserExists(accountId))
            {
                return new Tuple<bool, ErrorMessage>(false, ErrorMessage.UserDoesNotExist);
            }

            await _userAccountService.DeleteAccount(accountId);

            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }

        public async Task<Tuple<bool, ErrorMessage>> DisableUser(int accountId)
        {
            if (!await _validationService.UserExists(accountId))
            {
                return new Tuple<bool, ErrorMessage>(false, ErrorMessage.UserDoesNotExist);
            }
            if (!await _validationService.UserIsActive(accountId))
            {
                return new Tuple<bool, ErrorMessage>(false, ErrorMessage.UserIsNotActive);
            }

            await _userAccessService.ChangeAccountStatus(accountId, AccountStatus.Disabled);

            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }

        public async Task<Tuple<bool, ErrorMessage>> EnableUser(int accountId)
        {
            if (await _validationService.UserIsActive(accountId))
            {
                return new Tuple<bool, ErrorMessage>(false, ErrorMessage.UserIsActive);
            }

            await _userAccessService.ChangeAccountStatus(accountId, AccountStatus.Active);

            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }

        public async Task<Tuple<bool, ErrorMessage>> UpdateUsername(int accountId, string newUsername)
        {
            if (!await _validationService.UserExists(accountId))
            {
                return new Tuple<bool, ErrorMessage>(false, ErrorMessage.UserDoesNotExist);
            }

            await _userAccountService.ChangeUsername(accountId, newUsername);

            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }

        public async Task<Tuple<bool, ErrorMessage>> UpdatePassword(int accountId, string newPassword)
        {
            if (!await _validationService.UserExists(accountId))
            {
                return new Tuple<bool, ErrorMessage>(false, ErrorMessage.UserDoesNotExist);
            }

            await _userAccountService.ChangeUsername(accountId, newPassword);

            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }

        public async Task<Tuple<bool, ErrorMessage>> UpdateEmail(int accountId, string newEmail)
        {
            if (!await _validationService.UserExists(accountId))
            {
                return new Tuple<bool, ErrorMessage>(false, ErrorMessage.UserDoesNotExist);
            }

            await _userAccountService.ChangeEmail(accountId, newEmail);

            return new Tuple<bool, ErrorMessage>(true, ErrorMessage.None);
        }
    }
}
