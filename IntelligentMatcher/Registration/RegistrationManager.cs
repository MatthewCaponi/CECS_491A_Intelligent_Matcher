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

namespace Registration
{
    public class RegistrationManager : IRegistrationManager
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

        public async Task<Result<int>> RegisterAccount(WebUserAccountModel accountModel,
            WebUserProfileModel userModel, string password, string ipAddress)
        {
            // Create Result to determine the result and message the UI will present
            var resultModel = new Result<int>();
            if(accountModel.Username == null || accountModel.EmailAddress == null || userModel.FirstName == null ||
                userModel.Surname == null || userModel.DateOfBirth == null || password == null)
            {
                resultModel.Success = false;
                resultModel.ErrorMessage = ErrorMessage.Null;

                return resultModel;
            }
            else if(password.Length >= 8 && password.Any(char.IsDigit)
                    && password.Any(char.IsUpper) && password.Any(char.IsLower))
            {
                var usernameAlreadyExists = await _validationService.UsernameExists(accountModel.Username);

                if (usernameAlreadyExists)
                {
                    // Log and return Username existing result
                    _logger.Log(ErrorMessage.UsernameExists.ToString(), LogTarget.All, LogLevel.error, this.ToString(), "User_Logging");
                    resultModel.Success = false;
                    resultModel.ErrorMessage = ErrorMessage.UsernameExists;

                    return resultModel;
                }

                var emailAlreadyExists = await _validationService.EmailExists(accountModel.EmailAddress);

                if (emailAlreadyExists)
                {
                    // Log and return Email existing result
                    _logger.Log(ErrorMessage.EmailExists.ToString(), LogTarget.All, LogLevel.error, this.ToString(), "User_Logging");
                    resultModel.Success = false;
                    resultModel.ErrorMessage = ErrorMessage.EmailExists;

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

                //Log and Return result
                _logger.Log("User: " + accountModel.Username + " was registered", LogTarget.All, LogLevel.info, this.ToString(), "User_Logging");
                resultModel.Success = true;
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

            resultModel.Success = false;
            resultModel.ErrorMessage = ErrorMessage.InvalidPassword;

            return resultModel;

        }

        public async Task<bool> SendVerificationEmail(int accountId)
        {
            var account = await _userAccountService.GetUserAccount(accountId);

            string token = await _emailService.GetStatusToken(accountId);
            string confirmUrl = "http://localhost:3000/ConfirmAccount?id=" + accountId.ToString() + "?key=" + token;

            // Create New Email Model
            EmailModel emailModel =  _emailService.GetEmailOptions();

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

        public async Task DeleteIfNotActive(int userId)
        {
            WebUserAccountModel model = await _userAccountService.GetUserAccount(userId);

            if (model.AccountStatus != "Active")
            {
                await _userAccountService.DeleteAccount(userId);
            }

        }
    }
}
