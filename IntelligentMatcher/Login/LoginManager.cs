using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Security;
using IntelligentMatcher.Services;
using UserManagement.Models;
using UserManagement.Services;

namespace Login
{
    public class LoginManager : ILoginManager
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserProfileService _userProfileService;

        public LoginManager(IAuthenticationService authenticationService, ICryptographyService cryptographyService,
            IUserAccountService userAccountService, IUserProfileService userProfileService)
        {
            _authenticationService = authenticationService;
            _cryptographyService = cryptographyService;
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
        }

        public async Task<Result<WebUserAccountModel>> Login(string username, string password, string IpAddress)
        {
            try
            {
                var loginResult = new Result<WebUserAccountModel>();
                var account = await _userAccountService.GetUserAccountByUsername(username);

                // Account will be null if an account with the given username can't be found
                if (account == null)
                {
                    loginResult.Success = false;
                    loginResult.ErrorMessage = ErrorMessage.UserDoesNotExist;

                    return loginResult;
                }

                var authenticateUser = await _authenticationService.AuthenticatePasswordWithUsename(password, username);

                if (authenticateUser == false)
                {
                    loginResult.Success = false;
                    loginResult.ErrorMessage = ErrorMessage.NoMatch;

                    return loginResult;
                }

                loginResult.Success = true;
                loginResult.SuccessValue = account;

                return loginResult;
            }
            catch
            {
                var loginResult = new Result<WebUserAccountModel>();
                loginResult.Success = false;
                loginResult.ErrorMessage = ErrorMessage.AsyncError;

                return loginResult;
            }

        }
        public async Task<Result<WebUserAccountModel>> ForgotUsernameValidation(string emailAddress, DateTimeOffset dateOfBirth)
        {
            try
            {
                var forgotUsernameResult = new Result<WebUserAccountModel>();
                var account = await _userAccountService.GetUserAccountByEmail(emailAddress);

                // Account will be null if an account with the given email address can't be found
                if (account == null)
                {
                    forgotUsernameResult.Success = false;
                    forgotUsernameResult.ErrorMessage = ErrorMessage.UserDoesNotExist;

                    return forgotUsernameResult;
                }

                var profile = await _userProfileService.GetUserProfileByAccountId(account.Id);

                // Checks if the inputted Date of Birth does not match the one from the profile
                if (dateOfBirth != profile.DateOfBirth)
                {
                    forgotUsernameResult.Success = false;
                    forgotUsernameResult.ErrorMessage = ErrorMessage.NoMatch;

                    return forgotUsernameResult;
                }

                forgotUsernameResult.Success = true;
                forgotUsernameResult.SuccessValue = account;

                return forgotUsernameResult;
            }
            catch
            {
                var forgotUsernameResult = new Result<WebUserAccountModel>();
                forgotUsernameResult.Success = false;
                forgotUsernameResult.ErrorMessage = ErrorMessage.AsyncError;

                return forgotUsernameResult;
            }
        }
        public async Task<Result<WebUserAccountModel>> ForgotPasswordValidation(string username, string emailAddress, 
            DateTimeOffset dateOfBirth)
        {
            try
            {
                var forgotPasswordResult = new Result<WebUserAccountModel>();
                var account = await _userAccountService.GetUserAccountByUsername(username);

                // Account will be null if an account with the given email address can't be found
                if (account == null)
                {
                    forgotPasswordResult.Success = false;
                    forgotPasswordResult.ErrorMessage = ErrorMessage.UserDoesNotExist;

                    return forgotPasswordResult;
                }

                // Checks if the inputted email address does not match the one from the account
                if (emailAddress != account.EmailAddress)
                {
                    forgotPasswordResult.Success = false;
                    forgotPasswordResult.ErrorMessage = ErrorMessage.NoMatch;

                    return forgotPasswordResult;
                }
                var profile = await _userProfileService.GetUserProfileByAccountId(account.Id);

                // Checks if the inputted Date of Birth does not match the one from the profile
                if (dateOfBirth != profile.DateOfBirth)
                {
                    forgotPasswordResult.Success = false;
                    forgotPasswordResult.ErrorMessage = ErrorMessage.NoMatch;

                    return forgotPasswordResult;
                }

                forgotPasswordResult.Success = true;
                forgotPasswordResult.SuccessValue = account;

                return forgotPasswordResult;
            }
            catch
            {
                var forgotPasswordResult = new Result<WebUserAccountModel>();
                forgotPasswordResult.Success = false;
                forgotPasswordResult.ErrorMessage = ErrorMessage.AsyncError;

                return forgotPasswordResult;
            }
        }

        public async Task<Result<WebUserAccountModel>> ResetPassword(string password, int accountId)
        {
            try
            {
                var resetPasswordResult = new Result<WebUserAccountModel>();

                // Updates a new password by overwriting it and generates a new salt
                await _cryptographyService.newPasswordEncryptAsync(password, accountId);

                resetPasswordResult.Success = true;
                resetPasswordResult.SuccessValue = await _userAccountService.GetUserAccount(accountId);

                return resetPasswordResult;
            }
            catch
            {
                var resetPasswordResult = new Result<WebUserAccountModel>();
                resetPasswordResult.Success = false;
                resetPasswordResult.ErrorMessage = ErrorMessage.AsyncError;

                return resetPasswordResult;
            }
        }
    }
}
