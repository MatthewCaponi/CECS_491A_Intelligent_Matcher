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

        public async Task<Tuple<bool, ResultModel<int>>> RegisterNewAccount(WebUserAccountModel accountModel,
            WebUserProfileModel userModel, bool emailIsActive, string password, string ipAddress)
        {
            ResultModel<int> registry = new ResultModel<int>();
            ILoggingEvent _loggingEvent = new UserLoggingEvent(EventName.UserEvent, ipAddress,
                    accountModel.Id, AccountType.User.ToString());
            if (await _validationService.UsernameExists(accountModel.Username))
            {
                _logger.LogInfo(_loggingEvent, ErrorMessage.UsernameExists.ToString());
                registry.ErrorMessage = ErrorMessage.UsernameExists;
                return new Tuple<bool, ResultModel<int>>(false, registry);
            }
            if (await _validationService.EmailExists(accountModel.EmailAddress))
            {
                _logger.LogInfo(_loggingEvent, ErrorMessage.EmailExists.ToString());
                registry.ErrorMessage = ErrorMessage.EmailExists;
                return new Tuple<bool, ResultModel<int>>(false, registry);
            }
            // Creates User Account and gets Account ID
            var registerID = await _userAccountService.CreateAccount(accountModel);

            // Sets the password for the new Account
            var passwordEncrypted = await _cryptographyService.newPasswordEncryptAsync(password, registerID);

            // Passes on the Account ID to the User Profile Model
            userModel.UserAccountId = registerID;

            // Create User Profile with the Passed on Account ID
            await _userProfileService.CreateUserProfile(userModel);

            // Create New Email Model
            var emailModel = new EmailModel(accountModel.EmailAddress, "support@infinimuse.com", true, "Welcome!",
                "Welcome to InfiniMuse!", "Thank you for registering! " +
                "Please <a href='index.cshtml'>Enter the Site!</a> " +
                "<strong>You now have access to the features.</strong>", "outbound", "Welcome");

            //Send Verification Email
            while (!(await _emailService.SendEmail(emailModel)) && emailIsActive)
            {
                continue;
            }
            //Return the Result (Pass or Fail)
            if (emailIsActive)
            {
                _logger.LogInfo(_loggingEvent, "Email Sent");
                _logger.LogInfo(_loggingEvent, "User Registered");
                registry.Result = registerID;
                return new Tuple<bool, ResultModel<int>>(true, registry);
            }
            else
            {
                _logger.LogInfo(_loggingEvent, ErrorMessage.EmailNotSent.ToString());
                registry.Result = registerID;
                registry.ErrorMessage = ErrorMessage.EmailNotSent;
                return new Tuple<bool, ResultModel<int>>(false, registry);
            }

        }

    }
}
