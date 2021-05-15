using DataAccess;
using DataAccess.Repositories;
using Models;
using System;
using Logging;
using UserManagement.Models;
using BusinessModels;
using IntelligentMatcher.Services;
using Services;
using System.Threading.Tasks;
using UserManagement.Services;
using Registration.Services;
using Security;
using System.Timers;
using System.Threading;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using System.IO;
using System.Linq;
using Exceptions;
using AuthorizationServices;
using BusinessModels.UserAccessControl;
using UserAccessControlServices;
using System.Collections.Generic;

namespace Registration
{
    public class RegistrationManager : IRegistrationManager
    {

        private readonly ILogService _logger;
        private readonly IClaimsPrincipalService _claimsPrincipalService;
        private readonly IAssignmentPolicyService _assignmentPolicyService;
        private IEmailService _emailService;
        private IUserAccountService _userAccountService;
        private IUserProfileService _userProfileService;
        private readonly IValidationService _validationService;
        private readonly ICryptographyService _cryptographyService;

        private static System.Timers.Timer _timer;




        public RegistrationManager(IEmailService emailService, IUserAccountService userAccountService,
            IUserProfileService userProfileService, IValidationService validationService, ICryptographyService cryptographyService, ILogService logger, IClaimsPrincipalService claimsPrincipalService,
            IAssignmentPolicyService assignmentPolicyService)
        {
            _emailService = emailService;
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
            _validationService = validationService;
            _cryptographyService = cryptographyService;
            _logger = logger;
            _claimsPrincipalService = claimsPrincipalService;
            _assignmentPolicyService = assignmentPolicyService;
        }

        public async Task<Result<int>> RegisterAccount(WebUserAccountModel accountModel,
            WebUserProfileModel userModel, string password, string ipAddress)
        {
            try
            {
                // Create Result to determine the result and message the UI will present
                var resultModel = new Result<int>();
                if (accountModel.Username == null || accountModel.EmailAddress == null || userModel.FirstName == null ||
                    userModel.Surname == null || userModel.DateOfBirth == null || password == null)
                {
                    resultModel.WasSuccessful = false;
                    resultModel.ErrorMessage = ErrorMessage.Null;
                    Console.WriteLine("Register user failed: " + resultModel.ErrorMessage.ToString());
                    return resultModel;
                }
                else if (password.Length >= 8 && password.Any(char.IsDigit)
                        && password.Any(char.IsUpper) && password.Any(char.IsLower))
                {
                    var usernameAlreadyExists = await _validationService.UsernameExists(accountModel.Username);

                    if (usernameAlreadyExists)
                    {
                        // Log and return Username existing result
                        _logger.Log(ErrorMessage.UsernameExists.ToString(), LogTarget.All, LogLevel.error, this.ToString(), "User_Logging");
                        resultModel.WasSuccessful = false;
                        resultModel.ErrorMessage = ErrorMessage.UsernameExists;
                        Console.WriteLine("Register user failed: " + resultModel.ErrorMessage.ToString());
                        return resultModel;
                    }

                    var emailAlreadyExists = await _validationService.EmailExists(accountModel.EmailAddress);

                    if (emailAlreadyExists)
                    {
                        // Log and return Email existing result
                        _logger.Log(ErrorMessage.EmailExists.ToString(), LogTarget.All, LogLevel.error, this.ToString(), "User_Logging");
                        resultModel.WasSuccessful = false;
                        resultModel.ErrorMessage = ErrorMessage.EmailExists;
                        Console.WriteLine("Register user failed: " + resultModel.ErrorMessage.ToString());
                        return resultModel;
                    }

                    // Creates User Account and gets Account ID to pass along
                    var accountID = await _userAccountService.CreateAccount(accountModel);

                    // Sets the password for the new Account
                    await _cryptographyService.newPasswordEncryptAsync(password, accountID);

                    // Passes on the Account ID to the User Profile Model
                    userModel.UserAccountId = accountID;

                    // Create User Profile with the Passed on Account ID
                    var userProfileId = await _userProfileService.CreateUserProfile(userModel);

                    var assignmentPolicy = await _assignmentPolicyService.GetAssignmentPolicyByRole(accountModel.AccountType, 1);
                    var scopes = assignmentPolicy.SuccessValue.AssignedScopes;
                    var userScopes = new List<UserScopeModel>();
                    var userClaims = new List<UserClaimModel>();

                    foreach (var scope in scopes)
                    {
                        var userScope = new UserScopeModel()
                        {
                            Type = scope.Type,
                            UserAccountId = accountID
                        };

                        userScopes.Add(userScope);
                        foreach (var claim in scope.Claims)
                        {
                            var repeat = false;
                            foreach (var userClaim in userClaims)
                            {
                                if (userClaim.Type == claim.Type)
                                {
                                    repeat = true;
                                }
                            }
                            if (repeat)
                            {
                                continue;
                            }

                            var userClaimModel = new UserClaimModel()
                            {
                                Type = claim.Type,
                                Value = claim.Value,
                                UseraAccountId = accountID
                            };

                            userClaims.Add(userClaimModel);
                        }
                    }

                    // Create a new claims principal
                    await _claimsPrincipalService.CreateClaimsPrincipal(new ClaimsPrincipal()
                    {
                        Scopes = userScopes,
                        Claims = userClaims,
                        Role = accountModel.AccountType,
                        UserAccountId = accountID
                    });

                    //Log and Return result
                    _logger.Log("User: " + accountModel.Username + " was registered", LogTarget.All, LogLevel.info, this.ToString(), "User_Logging");
                    resultModel.WasSuccessful = true;
                    resultModel.SuccessValue = accountID;

                    await _emailService.CreateVerificationToken(accountID);

                    var emailResult = await SendVerificationEmail(accountID);

                    //Log Email Result
                    if (emailResult == true)
                    {
                        _logger.Log("Verification email sent to " + accountModel.Username, LogTarget.All, LogLevel.info, this.ToString(), "User_Logging");
                    }
                    else
                    {
                        _logger.Log("Verification email failed to send to " + accountModel.Username, LogTarget.All, LogLevel.error, this.ToString(), "User_Logging");
                    }

                    return resultModel;
                }

                resultModel.WasSuccessful = false;
                resultModel.ErrorMessage = ErrorMessage.InvalidPassword;
                Console.WriteLine("Register user failed: " + resultModel.ErrorMessage.ToString());
                return resultModel;
            }
            catch (SqlCustomException e)
            {
                Console.WriteLine("Register user failed" + e.Message);
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Register user failed" + e.InnerException.Message);
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> SendVerificationEmail(int accountId)
        {
            try
            {
                var account = await _userAccountService.GetUserAccount(accountId);

                string token = await _emailService.GetStatusToken(accountId);
                string confirmUrl = "http://localhost:3000/ConfirmAccount?id=" + accountId.ToString() + "?key=" + token;

                // Create New Email Model
                EmailModel emailModel = _emailService.GetEmailOptions();

                if (emailModel != null)
                {
                    // Set the Email Model Attributes
                    emailModel.Recipient = account.EmailAddress;
                    emailModel.HtmlBody = string.Format(emailModel.HtmlBody, confirmUrl);

                    //Send Verification Email
                    var result = await _emailService.SendEmail(emailModel);
                    //create auto expiration service 
                    //run function below 

                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        _timer = new System.Timers.Timer(10800000);
                        _timer.Elapsed += async (sender, e) => await DeleteIfNotActive(accountId);

                    }).Start();
                    return result;

                }
                else
                {
                    return false;
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

        public async Task DeleteIfNotActive(int userId)
        {
            try
            {
                WebUserAccountModel model = await _userAccountService.GetUserAccount(userId);

                if (model.AccountStatus != "Active")
                {
                    await _userAccountService.DeleteAccount(userId);
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
    }
}
