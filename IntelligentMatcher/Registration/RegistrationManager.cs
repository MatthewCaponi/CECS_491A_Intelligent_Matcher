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
        private EmailVerificationService _emailVerificationService;
        private UserAccountService _userAccountService;
        private UserProfileService _userProfileService;
        private readonly ValidationService _validationService;
        private readonly ICryptographyService _cryptographyService;

        public RegistrationManager(EmailVerificationService emailVerificationService, UserAccountService userAccountService,
            UserProfileService userProfileService, ValidationService validationService, ICryptographyService cryptographyService)
        {
            _emailVerificationService = emailVerificationService;
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
            _validationService = validationService;
            _cryptographyService = cryptographyService;
            ILogServiceFactory factory = new LogSeviceFactory();
            factory.AddTarget(TargetType.Text);

            _logger = factory.CreateLogService<RegistrationManager>();
        }

        public async Task<Tuple<bool, ResultModel<int>>> RegisterNewAccount(WebUserAccountModel accountModel,
            WebUserProfileModel userModel, bool emailIsActive, string password)
        {
            ResultModel<int> registry = new ResultModel<int>();
            if (await _validationService.UsernameExists(accountModel))
            {
                registry.ErrorMessage = ErrorMessage.UsernameExists;
                return new Tuple<bool, ResultModel<int>>(false, registry);
            }
            if (await _validationService.EmailExists(accountModel))
            {
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

            //Send Verification Email
            while (!(await _emailVerificationService.SendVerificationEmail(accountModel.EmailAddress)) && emailIsActive)
            {
                continue;
            }
            //Return the Result (Pass or Fail)
            if (emailIsActive)
            {
                registry.Result = registerID;
                return new Tuple<bool, ResultModel<int>>(true, registry);
            }
            else
            {
                registry.Result = registerID;
                registry.ErrorMessage = ErrorMessage.EmailNotSent;
                return new Tuple<bool, ResultModel<int>>(false, registry);
            }

        }

    }
}
