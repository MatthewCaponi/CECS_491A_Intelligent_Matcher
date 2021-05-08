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
using System.Linq;
using Exceptions;
using AuthenticationSystem;
using IdentityServices;
using BusinessModels.UserAccessControl;

namespace Login
{
    public class LoginManager : ILoginManager
    {
        private const int loginCounter = 0;
        private const int suspensionHours = 1;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IEmailService _emailService;
        private readonly ILoginAttemptsService _loginAttemptService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserAccountCodeService _userAccountCodeService;
        private readonly IUserProfileService _userProfileService;
        private readonly IAttributeAssignmentService _attributeAssignmentService;
        private readonly ITokenService _tokenService;

        public LoginManager(IAuthenticationService authenticationService, ICryptographyService cryptographyService,
            IEmailService emailService, ILoginAttemptsService loginAttemptsService, IUserAccountService userAccountService,
            IUserAccountCodeService userAccountCodeService, IUserProfileService userProfileService, IAttributeAssignmentService attributeAssignmentService, ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _cryptographyService = cryptographyService;
            _emailService = emailService;
            _loginAttemptService = loginAttemptsService;
            _userAccountService = userAccountService;
            _userAccountCodeService = userAccountCodeService;
            _userProfileService = userProfileService;
            _attributeAssignmentService = attributeAssignmentService;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> Login(string username, string password, string ipAddress)
        {
            try
            {
                var loginResult = new Result<string>();
                if (username == null || password == null)
                {
                    loginResult.WasSuccessful = false;
                    loginResult.ErrorMessage = ErrorMessage.Null;

                    return loginResult;
                }

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
                        loginResult.WasSuccessful = false;
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

                    loginResult.WasSuccessful = false;
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

 
                    loginResult.WasSuccessful = false;
                    loginResult.ErrorMessage = ErrorMessage.NoMatch;

                    return loginResult;
                }

                var scopes = _attributeAssignmentService.GetScopes(account.AccountType);
                var profile = await _userProfileService.GetUserProfileByAccountId(account.Id);
                StringBuilder delimitedScopes = new StringBuilder();
                foreach (var scope in scopes)
                {
                    delimitedScopes.Append(scope + ",");
                }

                var accessToken = _tokenService.CreateToken(new List<UserClaimModel>()
                    {
                          new UserClaimModel("scope", delimitedScopes.ToString()),
                            new UserClaimModel("role", account.AccountType),
                            new UserClaimModel("id", account.Id.ToString()),
                            new UserClaimModel("iss", this.ToString()),
                            new UserClaimModel("sub", account.Username),
                            new UserClaimModel("aud", account.Username),
                            new UserClaimModel("exp", "30"),
                            new UserClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                            new UserClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                                });

                var idToken = _tokenService.CreateToken(new List<UserClaimModel>()
                    {
                        new UserClaimModel("id", account.Id.ToString()),
                        new UserClaimModel("Scope", "id"),
                            new UserClaimModel("iss", this.ToString()),
                            new UserClaimModel("sub", account.Username),
                            new UserClaimModel("aud", account.Username),
                            new UserClaimModel("exp", "30"),
                            new UserClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                            new UserClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                            new UserClaimModel("firstName", profile.FirstName),
                            new UserClaimModel("lastName", profile.Surname),
                            new UserClaimModel("birthdate", profile.DateOfBirth.ToString()),
                                });
                await _loginAttemptService.ResetLoginCounterByIpAddress(ipAddress);

                TokenStorage tokenStorage = new TokenStorage();
                tokenStorage.IdToken = idToken;
                tokenStorage.AccessToken = accessToken;
                loginResult.WasSuccessful = true;
                loginResult.SuccessValue = idToken;

                return loginResult;
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

        public async Task<Result<string>> ForgotUsername(string emailAddress, DateTimeOffset dateOfBirth)
        {
            try
            {
                var forgotUsernameResult = new Result<string>();
                if (emailAddress == null || dateOfBirth == null)
                {
                    forgotUsernameResult.WasSuccessful = false;
                    forgotUsernameResult.ErrorMessage = ErrorMessage.Null;

                    return forgotUsernameResult;
                }

                var account = await _userAccountService.GetUserAccountByEmail(emailAddress);

                // Account will be null if an account with the given email address can't be found
                if (account == null)
                {
                    forgotUsernameResult.WasSuccessful = false;
                    forgotUsernameResult.ErrorMessage = ErrorMessage.NoMatch;

                    return forgotUsernameResult;
                }

                var profile = await _userProfileService.GetUserProfileByAccountId(account.Id);

                // Checks if the inputted Date of Birth does not match the one from the profile
                if (dateOfBirth != profile.DateOfBirth)
                {
                    forgotUsernameResult.WasSuccessful = false;
                    forgotUsernameResult.ErrorMessage = ErrorMessage.NoMatch;

                    return forgotUsernameResult;
                }

                forgotUsernameResult.WasSuccessful = true;
                forgotUsernameResult.SuccessValue = account.Username;

                return forgotUsernameResult;
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

        public async Task<Result<WebUserAccountModel>> ForgotPasswordValidation(string username, string emailAddress, 
            DateTimeOffset dateOfBirth)
        {
            try
            {
                var forgotPasswordResult = new Result<WebUserAccountModel>();
                if (username == null || emailAddress == null || dateOfBirth == null)
                {
                    forgotPasswordResult.WasSuccessful = false;
                    forgotPasswordResult.ErrorMessage = ErrorMessage.Null;

                    return forgotPasswordResult;
                }

                var account = await _userAccountService.GetUserAccountByUsername(username);

                // Account will be null if an account with the given email address can't be found
                if (account == null)
                {
                    forgotPasswordResult.WasSuccessful = false;
                    forgotPasswordResult.ErrorMessage = ErrorMessage.NoMatch;

                    return forgotPasswordResult;
                }

                // Checks if the inputted email address does not match the one from the account
                if (emailAddress != account.EmailAddress)
                {
                    forgotPasswordResult.WasSuccessful = false;
                    forgotPasswordResult.ErrorMessage = ErrorMessage.NoMatch;

                    return forgotPasswordResult;
                }
                var profile = await _userProfileService.GetUserProfileByAccountId(account.Id);

                // Checks if the inputted Date of Birth does not match the one from the profile
                if (dateOfBirth != profile.DateOfBirth)
                {
                    forgotPasswordResult.WasSuccessful = false;
                    forgotPasswordResult.ErrorMessage = ErrorMessage.NoMatch;

                    return forgotPasswordResult;
                }

                Random random = new Random();

                string code = "";
                for(var i = 0; i < 15; i++)
                {
                    code += ((char)(random.Next(1, 10) + 47)).ToString();
                }

                var accountCode = await _userAccountCodeService.GetUserAccountCodeByAccountId(account.Id);

                if(accountCode != null)
                {
                    await _userAccountCodeService.UpdateCodeByAccountId(code, DateTimeOffset.UtcNow.AddHours(suspensionHours),
                        account.Id);
                }
                else
                {
                    await _userAccountCodeService.AddCode(code, DateTimeOffset.UtcNow.AddHours(suspensionHours), account.Id);
                }

                // Create New Email Model
                var emailModel = new EmailModel();

                // Set the Email Model Attributes
                emailModel.Recipient = emailAddress;
                emailModel.Sender = "support@infinimuse.com";
                emailModel.TrackOpens = true;
                emailModel.Subject = "Reset Password Code";
                emailModel.TextBody = "Here is your code: " + code;
                emailModel.HtmlBody = "This code will expire in 1 hour. " + 
                    "Please enter <strong> " + code + " </strong> on the Code Entry Page.";
                emailModel.MessageStream = "outbound";
                emailModel.Tag = "Reset Password Code";

                // Send Reset Password Email
                await _emailService.SendEmail(emailModel);

                accountCode = await _userAccountCodeService.GetUserAccountCodeByAccountId(account.Id);

                forgotPasswordResult.WasSuccessful = true;
                forgotPasswordResult.SuccessValue = account;

                return forgotPasswordResult;
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

        public async Task<Result<WebUserAccountModel>> ForgotPasswordCodeInput(string code, int accountId)
        {
            try
            {
                var forgotPasswordCodeResult = new Result<WebUserAccountModel>();
                if (code == null)
                {
                    forgotPasswordCodeResult.WasSuccessful = false;
                    forgotPasswordCodeResult.ErrorMessage = ErrorMessage.Null;

                    return forgotPasswordCodeResult;
                }

                var userAccountCode = await _userAccountCodeService.GetUserAccountCodeByAccountId(accountId);

                if(userAccountCode == null)
                {
                    forgotPasswordCodeResult.WasSuccessful = false;
                    forgotPasswordCodeResult.ErrorMessage = ErrorMessage.Null;

                    return forgotPasswordCodeResult;
                }

                var timeExpired = (userAccountCode.ExpirationTime <= DateTimeOffset.UtcNow);

                if (timeExpired == true)
                {
                    await _userAccountCodeService.DeleteCodeByAccountId(accountId);

                    forgotPasswordCodeResult.WasSuccessful = false;
                    forgotPasswordCodeResult.ErrorMessage = ErrorMessage.CodeExpired;

                    return forgotPasswordCodeResult;
                }

                if (userAccountCode.Code == code)
                {
                    await _userAccountCodeService.DeleteCodeByAccountId(accountId);

                    forgotPasswordCodeResult.WasSuccessful = true;
                    forgotPasswordCodeResult.SuccessValue = await _userAccountService.GetUserAccount(accountId);

                    return forgotPasswordCodeResult;
                }
                else
                {
                    forgotPasswordCodeResult.WasSuccessful = false;
                    forgotPasswordCodeResult.ErrorMessage = ErrorMessage.NoMatch;
                    
                    return forgotPasswordCodeResult;
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

        public async Task<Result<WebUserAccountModel>> ResetPassword(string password, int accountId)
        {
            try
            {
                var resetPasswordResult = new Result<WebUserAccountModel>();
                if (password == null)
                {
                    resetPasswordResult.WasSuccessful = false;
                    resetPasswordResult.ErrorMessage = ErrorMessage.Null;

                    return resetPasswordResult;
                }

                if (password.Length >= 8 && password.Any(char.IsDigit)
                    && password.Any(char.IsUpper) && password.Any(char.IsLower))
                {
                    // Updates a new password by overwriting it and generates a new salt
                    await _cryptographyService.newPasswordEncryptAsync(password, accountId);

                    resetPasswordResult.WasSuccessful = true;
                    resetPasswordResult.SuccessValue = await _userAccountService.GetUserAccount(accountId);

                    return resetPasswordResult;
                }

                resetPasswordResult.WasSuccessful = false;
                resetPasswordResult.ErrorMessage = ErrorMessage.InvalidPassword;

                return resetPasswordResult;
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
    }
}
