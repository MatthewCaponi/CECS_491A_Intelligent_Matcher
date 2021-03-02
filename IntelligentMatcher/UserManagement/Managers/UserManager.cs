using System;
using System.Threading.Tasks;
using System.Linq;
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
        private UserAccountService _userAccountService;
        private UserProfileService _userProfileService;
        private UserAccessService _userAccessService;
        private readonly ValidationService _validationService;

        public UserManager(UserAccountService userAccountService, UserProfileService userProfileService,
            UserAccessService userAccessService, ValidationService validationService)
        {
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
            _userAccessService = userAccessService;
            _validationService = validationService;
        }

        public async Task<Tuple<bool, ResultModel<WebUserProfileModel>>> GetUserProfile(int id)
        {
            ResultModel<WebUserProfileModel> resultModel = new ResultModel<WebUserProfileModel>();
            if (!await _validationService.UserExists(id))
            {
                resultModel.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return new Tuple<bool, ResultModel<WebUserProfileModel>>(false, resultModel);
            }

            resultModel.Result = await _userProfileService.GetUser(id);
            return new Tuple<bool, ResultModel<WebUserProfileModel>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<List<WebUserProfileModel>>>> GetAllUserProfiles()
        {
            ResultModel<List<WebUserProfileModel>> resultModel = new ResultModel<List<WebUserProfileModel>>();
            if (await _validationService.ListIsEmpty(typeof(WebUserProfileModel)))
            {
                resultModel.ErrorMessage = ErrorMessage.NoUsersExist;
                return new Tuple<bool, ResultModel<List<WebUserProfileModel>>>(false, resultModel);
            }

            resultModel.Result = await _userProfileService.GetAllUsers();
            return new Tuple<bool, ResultModel<List<WebUserProfileModel>>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<WebUserAccountModel>>> GetUserAccount(int id)
        {
            ResultModel<WebUserAccountModel> resultModel = new ResultModel<WebUserAccountModel>();
            if (!await _validationService.UserExists(id))
            {
                resultModel.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return new Tuple<bool, ResultModel<WebUserAccountModel>>(false, resultModel);
            }

            resultModel.Result = await _userAccountService.GetUserAccount(id);
            return new Tuple<bool, ResultModel<WebUserAccountModel>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<List<WebUserAccountModel>>>> GetAllUserAccounts()
        {
            ResultModel<List<WebUserAccountModel>> resultModel = new ResultModel<List<WebUserAccountModel>>();
            if (await _validationService.ListIsEmpty(typeof(WebUserAccountModel)))
            {
                resultModel.ErrorMessage = ErrorMessage.NoUsersExist;
                return new Tuple<bool, ResultModel<List<WebUserAccountModel>>>(false, resultModel);
            }

            resultModel.Result = await _userAccountService.GetAllUserAccounts();
            return new Tuple<bool, ResultModel<List<WebUserAccountModel>>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<int>>> CreateUser(WebUserAccountModel webUserAccountModel, WebUserProfileModel webUserProfileModel)
        {
            ResultModel<int> resultModel = new ResultModel<int>();
            if (_validationService.IsNull(webUserAccountModel))
            {
                resultModel.ErrorMessage = ErrorMessage.Null;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }
            if (await _validationService.UsernameExists(webUserAccountModel))
            {
                resultModel.ErrorMessage = ErrorMessage.UsernameExists;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }
            if (await _validationService.EmailExists(webUserAccountModel))
            {
                resultModel.ErrorMessage = ErrorMessage.EmailExists;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }

            var userAccountID = await _userAccountService.CreateAccount(webUserAccountModel);
            webUserProfileModel.UserAccountId = userAccountID;
            await _userProfileService.CreateUser(webUserProfileModel);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<int>>> DeleteUser(int accountId)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            if (!await _validationService.UserExists(accountId))
            {
                resultModel.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }

            await _userAccountService.DeleteAccount(accountId);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<int>>> DisableUser(int accountId)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            if (!await _validationService.UserExists(accountId))
            {
                resultModel.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }
            if (!await _validationService.UserIsActive(accountId))
            {
                resultModel.ErrorMessage = ErrorMessage.UserIsNotActive;
            }

            await _userAccessService.ChangeAccountStatus(accountId, AccountStatus.Disabled);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<int>>> EnableUser(int accountId)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            if (await _validationService.UserIsActive(accountId))
            {
                resultModel.ErrorMessage = ErrorMessage.UserIsActive;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }

            await _userAccessService.ChangeAccountStatus(accountId, AccountStatus.Active);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<int>>> UpdateUsername(int accountId, string newUsername)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            if (!await _validationService.UserExists(accountId))
            {
                resultModel.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }

            await _userAccountService.ChangeUsername(accountId, newUsername);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<int>>> UpdatePassword(int accountId, string newPassword)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            if (!await _validationService.UserExists(accountId))
            {
                resultModel.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }

            await _userAccountService.ChangeUsername(accountId, newPassword);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }

        public async Task<Tuple<bool, ResultModel<int>>> UpdateEmail(int accountId, string newEmail)
        {
            ResultModel<int> resultModel = new ResultModel<int>();

            if (!await _validationService.UserExists(accountId))
            {
                resultModel.ErrorMessage = ErrorMessage.UserDoesNotExist;
                return new Tuple<bool, ResultModel<int>>(false, resultModel);
            }

            await _userAccountService.ChangeEmail(accountId, newEmail);

            return new Tuple<bool, ResultModel<int>>(true, resultModel);
        }
    }
}
