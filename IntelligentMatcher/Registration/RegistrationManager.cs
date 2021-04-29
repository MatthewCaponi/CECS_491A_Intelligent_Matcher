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

            var usernameAlreadyExists = await _validationService.UsernameExists(accountModel.Username);

            if (usernameAlreadyExists)
            {
                // Log and return Username existing result
                //_logger.LogInfo(_loggingEvent, ErrorMessage.UsernameExists.ToString());
                _logger.Log(ErrorMessage.UsernameExists.ToString(), LogTarget.Text, this.ToString());
                _logger.Log(ErrorMessage.UsernameExists.ToString(), LogTarget.Json, this.ToString());
                resultModel.Success = false;
                resultModel.ErrorMessage = ErrorMessage.UsernameExists;

                return resultModel;
            }

            var emailAlreadyExists = await _validationService.EmailExists(accountModel.EmailAddress);

            if (emailAlreadyExists)
            {
                // Log and return Email existing result
                //_logger.LogInfo(_loggingEvent, ErrorMessage.EmailExists.ToString());
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
            await _userProfileService.CreateUserProfile(userModel);

            // Re-Clarify the logging event

            //Log and Return result
            //_logger.LogInfo(_loggingEvent, "User Registered");
            resultModel.Success = true;
            resultModel.SuccessValue = accountID;

            var emailResult = await SendVerificationEmail(accountID);
            
            //Log Email Result
            if(emailResult == true)
            {
                //_logger.LogInfo(_loggingEvent, "Email Sent");
            }
            else
            {
                //_logger.LogInfo(_loggingEvent, "Email Not Sent");
            }

            // First items of these tuples are immutable
            // A new one must be returned for the success conditional
            return resultModel;
        }

        public async Task<bool> SendVerificationEmail(int accountId)
        {
            var account = await _userAccountService.GetUserAccount(accountId);

            // Create New Email Model
            var emailModel = new EmailModel();

            // Set the Email Model Attributes
            emailModel.Recipient = account.EmailAddress;
            emailModel.Sender = "support@infinimuse.com";
            emailModel.TrackOpens = true;
            emailModel.Subject = "Welcome!";
            emailModel.TextBody = "Welcome to InfiniMuse!";
            emailModel.HtmlBody = "Thank you for registering! " +
                "Please <a href='http://localhost:3000'>Enter the Site!</a> " +
                "<strong>You now have access to the features.</strong>";
            emailModel.MessageStream = "outbound";
            emailModel.Tag = "Welcome";

            //Send Verification Email
            var result = await _emailService.SendEmail(emailModel);

            return result;
        }
    }
}
