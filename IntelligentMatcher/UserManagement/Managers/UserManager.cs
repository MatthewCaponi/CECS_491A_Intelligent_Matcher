using System;
using System.Threading.Tasks;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services;
using IntelligentMatcher.Services;
using BusinessModels;
using Services;

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

        public async Task<WebUserProfileModel> GetUserProfile(int id)
        {
            return await _userProfileService.GetUser(id);
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

        public async Task<bool> DisableUser(int accountId)
        {
            if (await UserAccessService.DisableAccount(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EnableUser(int accountId)
        {
            if (await UserAccessService.EnableAccount(accountId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateUsername(int accountId, string newUsername)
        {
            if (await UserUpdateService.ChangeUsername(accountId, newUsername))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePassword(int accountId, string newPassword)
        {
            if (await UserUpdateService.ChangePassword(accountId, newPassword))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEmail(int accountId, string newEmail)
        {
            if (await UserUpdateService.ChangeEmail(accountId, newEmail))
            {
                return true;
            }

            return false;
        }
    }
