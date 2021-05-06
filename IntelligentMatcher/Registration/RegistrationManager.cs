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
using BusinessLayer.CrossCuttingConcerns;

namespace Registration
{
    public class RegistrationManager : BusinessLayerBase, IRegistrationManager
    {

        private readonly ILogService _logger;
        private IEmailService _emailService;
        private IUserAccountService _userAccountService;
        private IUserProfileService _userProfileService;
        private readonly IValidationService _validationService;
        private readonly ICryptographyService _cryptographyService;

        private static System.Timers.Timer _timer;




        public RegistrationManager(IEmailService emailService, IUserAccountService userAccountService,
            IUserProfileService userProfileService, IValidationService validationService, ICryptographyService cryptographyService, ILogService logger)
        {
            _emailService = emailService;
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
            _validationService = validationService;
            _cryptographyService = cryptographyService;
            _logger = logger;
        }

        public async Task<Result<int>> RegisterAccount(WebUserAccountModel webUserAccountModel,
            WebUserProfileModel webUserProfileModel, string password)
        {
            #region InputValidation
            // Create Result to determine the result and message the UI will present
            if (ContainsNullOrEmptyParameter(webUserAccountModel))
            {
                return Result<int>.Failure(ErrorMessage.ContainsNullOrEmptyParameters.ToString());
            }

            if (ContainsNullOrEmptyParameter(webUserProfileModel))
            {
                return Result<int>.Failure(ErrorMessage.ContainsNullOrEmptyParameters.ToString());
            }

            if (String.IsNullOrEmpty(password))
            {
                return Result<int>.Failure(ErrorMessage.IsNullOrEmpty.ToString());
            }

            if (InvalidMinLength(password, 8))
            {
                return Result<int>.Failure(ErrorMessage.InvalidLength.ToString());
            }

            if (!ContainsRequiredCharacterTypes(password, true, true, true, true))
            {
                return Result<int>.Failure(ErrorMessage.LacksMinCharacterTypes.ToString());
            }

            #endregion

            #region BusinessValidation

            if (await _validationService.UsernameExists(webUserAccountModel.Username))
            {
                _logger.Log(ErrorMessage.UsernameExists.ToString(), LogTarget.All, LogLevel.error, this.ToString(), "User_Logging");
                return Result<int>.Failure(ErrorMessage.UsernameExists.ToString());
            }

            if (await _validationService.EmailExists(webUserAccountModel.EmailAddress))
            {
                _logger.Log(ErrorMessage.EmailExists.ToString(), LogTarget.All, LogLevel.error, this.ToString(), "User_Logging");
                return Result<int>.Failure(ErrorMessage.EmailExists.ToString());
            }

            #endregion

            // Creates User Account and gets Account ID to pass along
            var result = await _userAccountService.CreateAccount(webUserAccountModel);

            if (!result.WasSuccessful)
            {
                return Result<int>.Failure(result.ErrorMessage.ToString());
            }

            var accountID = result.SuccessValue;

            // Sets the password for the new Account
            var encrypted = !await _cryptographyService.newPasswordEncryptAsync(password, accountID);
            if (!encrypted)
            {
                return Result<int>.Failure("Password Encryption Failed");
            }

            // Passes on the Account ID to the User Profile Model
            webUserProfileModel.UserAccountId = accountID;

            // Create User Profile with the Passed on Account ID
            var userProfileId = await _userProfileService.CreateUserProfile(webUserProfileModel);

            //Log and Return result
            _logger.Log("User: " + webUserAccountModel.Username + " was registered", LogTarget.All, LogLevel.info, this.ToString(), "User_Logging");
            resultModel.WasSuccessful = true;
            resultModel.SuccessValue = accountID;

            await _emailService.CreateVerificationToken(accountID);

            var emailResult = await SendVerificationEmail(accountID);

            //Log Email Result
            if (emailResult == true)
            {
                _logger.Log("Verification email sent to " + webUserAccountModel.Username, LogTarget.All, LogLevel.info, this.ToString(), "User_Logging");
            }
            else
            {
                _logger.Log("Verification email failed to send to " + webUserAccountModel.Username, LogTarget.All, LogLevel.error, this.ToString(), "User_Logging");
            }

            return resultModel;
        }

            resultModel.WasSuccessful = false;
            resultModel.ErrorMessage = ErrorMessage.InvalidPassword;

            return resultModel;
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
