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

        ILogService _logger;
        private IEmailService _emailService;
        private IUserAccountService _userAccountService;
        private IUserProfileService _userProfileService;
        private readonly IValidationService _validationService;
        private readonly ICryptographyService _cryptographyService;

        public RegistrationManager(IEmailService emailService, IUserAccountService userAccountService,
            IUserProfileService userProfileService, IValidationService validationService, ICryptographyService cryptographyService)
        {
            _emailService = emailService;
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
            _validationService = validationService;
            _cryptographyService = cryptographyService;
            ILogServiceFactory factory = new LogSeviceFactory();
            factory.AddTarget(TargetType.Text);

            _logger = factory.CreateLogService<RegistrationManager>();
        }

        public async Task<Result<int>> RegisterAccount(WebUserAccountModel accountModel,
            WebUserProfileModel userModel, string password, string ipAddress)
        {
            // Create Result to determine the result and message the UI will present
            var resultModel = new Result<int>();
            // Clarify the logging event
            ILoggingEvent _loggingEvent = new UserLoggingEvent(EventName.UserEvent, ipAddress,
                    accountModel.Id, AccountType.User.ToString());

            var usernameAlreadyExists = await _validationService.UsernameExists(accountModel.Username);

            if (usernameAlreadyExists)
            {
                // Log and return Username existing result
                _logger.LogInfo(_loggingEvent, ErrorMessage.UsernameExists.ToString());
                resultModel.Success = false;
                resultModel.ErrorMessage = ErrorMessage.UsernameExists;

                return resultModel;
            }

            var emailAlreadyExists = await _validationService.EmailExists(accountModel.EmailAddress);

            if (emailAlreadyExists)
            {
                // Log and return Email existing result
                _logger.LogInfo(_loggingEvent, ErrorMessage.EmailExists.ToString());
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

            // Create New Email Model
            var emailModel = new EmailModel();

            // Re-Clarify the logging event
            _loggingEvent = new UserLoggingEvent(EventName.UserEvent, ipAddress,
                    accountID, AccountType.User.ToString());

            //Log and Return result
            _logger.LogInfo(_loggingEvent, "User Registered");
            resultModel.Success = true;
            resultModel.SuccessValue = accountID;

            // Set the Email Model Attributes
            emailModel.Recipient = accountModel.EmailAddress;
            emailModel.Sender = "support@infinimuse.com";
            emailModel.TrackOpens = true;
            emailModel.Subject = "Welcome!";
            emailModel.TextBody = "Welcome to InfiniMuse!";
            emailModel.HtmlBody = "Thank you for registering! " +
                "Please <a href='index.cshtml'>Enter the Site!</a> " +
                "<strong>You now have access to the features.</strong>";
            emailModel.MessageStream = "outbound";
            emailModel.Tag = "Welcome";

            //Send Verification Email
            await _emailService.SendEmail(emailModel);
            
            //Log Email Result
            _logger.LogInfo(_loggingEvent, "Email Sent");

            // First items of these tuples are immutable
            // A new one must be returned for the success conditional
            return resultModel;
        }
    }
}
