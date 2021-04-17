using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Security;
using IntelligentMatcher.Services;
using UserManagement.Models;
using UserManagement.Services;
using Services;

namespace Login
{
    public class LoginManager : ILoginManager
    {
        private const int loginCounter = 0;
        private const int suspensionHours = 1;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICryptographyService _cryptographyService;
        private readonly ILoginAttemptsService _loginAttemptService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserAccountCodeService _userAccountCodeService;
        private readonly IUserProfileService _userProfileService;

        public LoginManager(IAuthenticationService authenticationService, ICryptographyService cryptographyService,
            ILoginAttemptsService loginAttemptsService, IUserAccountService userAccountService,
            IUserAccountCodeService userAccountCodeService, IUserProfileService userProfileService)
        {
            _authenticationService = authenticationService;
            _cryptographyService = cryptographyService;
            _loginAttemptService = loginAttemptsService;
            _userAccountService = userAccountService;
            _userAccountCodeService = userAccountCodeService;
            _userProfileService = userProfileService;
        }

        public async Task<Result<WebUserAccountModel>> Login(string username, string password, string ipAddress)
        {
            try
            {
                var loginResult = new Result<WebUserAccountModel>();
                var businessLoginAttemptsModel = await _loginAttemptService.GetLoginAttemptsByIpAddress(ipAddress);

                if (businessLoginAttemptsModel == null)
                {
                    await _loginAttemptService.AddIpAddress(ipAddress, loginCounter);
                    businessLoginAttemptsModel = await _loginAttemptService.GetLoginAttemptsByIpAddress(ipAddress);
                }

                if (businessLoginAttemptsModel.LoginCounter >= 5)
                {
                    if (DateTimeOffset.UtcNow < businessLoginAttemptsModel.SuspensionEndTime)
                    {
                        loginResult.Success = false;
                        loginResult.ErrorMessage = ErrorMessage.TooManyAttempts;

                        return loginResult;
                    }
                    await _loginAttemptService.ResetLoginCounterByIpAddress(ipAddress);
                }

                var account = await _userAccountService.GetUserAccountByUsername(username);

                // Account will be null if an account with the given username can't be found
                if (account == null)
                {
                    await _loginAttemptService.IncrementLoginCounterByIpAddress(ipAddress);
                    businessLoginAttemptsModel = await _loginAttemptService.GetLoginAttemptsByIpAddress(ipAddress);

                    if(businessLoginAttemptsModel.LoginCounter >= 5)
                    {
                        await _loginAttemptService.SetSuspensionEndTimeByIpAddress(ipAddress, suspensionHours);
                    }

                    loginResult.Success = false;
                    loginResult.ErrorMessage = ErrorMessage.NoMatch;

                    return loginResult;
                }

                var authenticateUser = await _authenticationService.AuthenticatePasswordWithUsename(password, username);

                if (authenticateUser == false)
                {
                    await _loginAttemptService.IncrementLoginCounterByIpAddress(ipAddress);
                    businessLoginAttemptsModel = await _loginAttemptService.GetLoginAttemptsByIpAddress(ipAddress);

                    if (businessLoginAttemptsModel.LoginCounter >= 5)
                    {
                        await _loginAttemptService.SetSuspensionEndTimeByIpAddress(ipAddress, suspensionHours);
                    }

                    loginResult.Success = false;
                    loginResult.ErrorMessage = ErrorMessage.NoMatch;

                    return loginResult;
                }
                await _loginAttemptService.ResetLoginCounterByIpAddress(ipAddress);

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

        public async Task<Result<string>> ForgotUsername(string emailAddress, DateTimeOffset dateOfBirth)
        {
            try
            {
                var forgotUsernameResult = new Result<string>();
                var account = await _userAccountService.GetUserAccountByEmail(emailAddress);

                // Account will be null if an account with the given email address can't be found
                if (account == null)
                {
                    forgotUsernameResult.Success = false;
                    forgotUsernameResult.ErrorMessage = ErrorMessage.NoMatch;

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
                forgotUsernameResult.SuccessValue = account.Username;

                return forgotUsernameResult;
            }
            catch
            {
                var forgotUsernameResult = new Result<string>();
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
                    forgotPasswordResult.ErrorMessage = ErrorMessage.NoMatch;

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

                Random random = new Random();

                string code = "";
                for(var i = 0; i < 15; i++)
                {
                    code += ((char)(random.Next(1, 10) + 47)).ToString();
                }

                await _userAccountCodeService.AddCode(code, DateTimeOffset.UtcNow.AddHours(1), account.Id);

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

        public async Task<Result<WebUserAccountModel>> ForgotPasswordCodeInput(string code, int accountId)
        {
            try
            {
                var forgotPasswordCodeResult = new Result<WebUserAccountModel>();

                var userAccountCode = await _userAccountCodeService.GetUserAccountCodeByAccountId(accountId);

                if(userAccountCode == null)
                {
                    forgotPasswordCodeResult.Success = false;
                    forgotPasswordCodeResult.ErrorMessage = ErrorMessage.Null;

                    return forgotPasswordCodeResult;
                }

                var timeExpired = (userAccountCode.ExpirationTime <= DateTimeOffset.UtcNow);

                if (timeExpired == true)
                {
                    await _userAccountCodeService.DeleteCode(accountId);

                    forgotPasswordCodeResult.Success = false;
                    forgotPasswordCodeResult.ErrorMessage = ErrorMessage.CodeExpired;

                    return forgotPasswordCodeResult;
                }

                if (userAccountCode.Code == code)
                {
                    await _userAccountCodeService.DeleteCode(accountId);

                    forgotPasswordCodeResult.Success = true;
                    forgotPasswordCodeResult.SuccessValue = await _userAccountService.GetUserAccount(accountId);

                    return forgotPasswordCodeResult;
                }
                else
                {
                    forgotPasswordCodeResult.Success = false;
                    forgotPasswordCodeResult.ErrorMessage = ErrorMessage.NoMatch;
                    
                    return forgotPasswordCodeResult;
                }
            }
            catch
            {
                var forgotPasswordCodeResult = new Result<WebUserAccountModel>();
                forgotPasswordCodeResult.Success = false;
                forgotPasswordCodeResult.ErrorMessage = ErrorMessage.AsyncError;

                return forgotPasswordCodeResult;
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
