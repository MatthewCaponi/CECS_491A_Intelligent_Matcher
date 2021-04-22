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
        private static System.Timers.Timer _timer;

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

            // Re-Clarify the logging event
            _loggingEvent = new UserLoggingEvent(EventName.UserEvent, ipAddress,
                    accountID, AccountType.User.ToString());


            //Log and Return result
            _logger.LogInfo(_loggingEvent, "User Registered");
            resultModel.Success = true;
            resultModel.SuccessValue = accountID;

            var emailResult = await SendVerificationEmail(accountID);
            
            //Log Email Result
            if(emailResult == true)
            {
                _logger.LogInfo(_loggingEvent, "Email Sent");
            }
            else
            {
                _logger.LogInfo(_loggingEvent, "Email Not Sent");
            }

            // First items of these tuples are immutable
            // A new one must be returned for the success conditional
            return resultModel;
        }

        public async Task<bool> SendVerificationEmail(int accountId)
        {
            var account = await _userAccountService.GetUserAccount(accountId);

            string token = await _userAccountService.GetStatusToken(accountId);
            string confirmUrl = "https://localhost:3000/confirm?id=" + accountId.ToString() + "?key=" + token;
            // Create New Email Model
            var emailModel = new EmailModel();

            // Set the Email Model Attributes
            emailModel.Recipient = account.EmailAddress;
            emailModel.Sender = "support@infinimuse.com";
            emailModel.TrackOpens = true;
            emailModel.Subject = "Welcome!";
            emailModel.TextBody = "Welcome to InfiniMuse!";
            emailModel.HtmlBody = "Thank you for registering! " +
                "Please confirm your account with the link: <a href='"+ confirmUrl +"'>Confirm Your Account!</a> " +
                "<strong>Once confirmed you will have access to the features.</strong>";
            emailModel.MessageStream = "outbound";
            emailModel.Tag = "Welcome";







            //Send Verification Email
            var result = await _emailService.SendEmail(emailModel);





            //create auto expiration service 
            //run function below 

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                _timer = new System.Timers.Timer(10800000);
                _timer.Elapsed += async (sender, e) => await _userAccountService.DeleteIfNotActive(accountId);

            }).Start();






            // First items of these tuples are immutable
            // A new one must be returned for the success conditional

            return true;
        }
    }
}
