using BusinessModels;
using BusinessModels.UserAccessControl;
using ControllerModels;
using ControllerModels.RegistrationModels;
using Microsoft.AspNetCore.Mvc;
using Registration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement;
using IntelligentMatcher.Services;
using Services;
using Registration.Services;
using Exceptions;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationManager _registrationManager;
        private readonly IUserAccountService _userAccountService;
        private readonly IEmailService _emailService;

        public class TokenIdModel
        {
            public int UserId { get; set; }
            public string Token { get; set; }
        }

        public RegistrationController(IRegistrationManager registrationManager, IUserAccountService userAccountService, IEmailService emailService)
        {
            _registrationManager = registrationManager;
            _userAccountService = userAccountService;
            _emailService = emailService;
        }


        [HttpPost]
        public async Task<bool> ConfirmUser([FromBody] TokenIdModel tokenIds)
        {
            try
            {
                return await _emailService.ValidateStatusToken(tokenIds.UserId, tokenIds.Token);
            }
            catch (SqlCustomException)
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<RegistrationResultModel> RegisterUser([FromBody] RegistrationModel registrationModel)
        {
            try
            {
                var registrationResultModel = new RegistrationResultModel();

                if (registrationModel.username == null || registrationModel.password == null || registrationModel.emailAddress == null
                    || registrationModel.firstName == null || registrationModel.surname == null || registrationModel.dateOfBirth == null)
                {
                    registrationResultModel.Success = false;
                    registrationResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return registrationResultModel;
                }

                var userAccount = new WebUserAccountModel();

                userAccount.Username = registrationModel.username;
                userAccount.EmailAddress = registrationModel.emailAddress;
                userAccount.AccountType = AccountType.User.ToString();
                userAccount.AccountStatus = AccountStatus.Inactive.ToString();
                userAccount.CreationDate = DateTimeOffset.UtcNow;
                userAccount.UpdationDate = DateTimeOffset.UtcNow;

                var userProfile = new WebUserProfileModel();

                userProfile.FirstName = registrationModel.firstName;
                userProfile.Surname = registrationModel.surname;
                userProfile.DateOfBirth = DateTimeOffset.Parse(registrationModel.dateOfBirth);

                var registrationResult = await _registrationManager.RegisterAccount(userAccount, userProfile,
                    registrationModel.password, registrationModel.ipAddress);

                registrationResultModel.Success = registrationResult.WasSuccessful;

                if (registrationResult.WasSuccessful)
                {
                    registrationResultModel.AccountId = registrationResult.SuccessValue;
                    registrationResultModel.ErrorMessage = ErrorMessage.None.ToString();

                    return registrationResultModel;
                }
                else
                {
                    registrationResultModel.ErrorMessage = registrationResult.ErrorMessage.ToString();

                    return registrationResultModel;
                }
            }
            catch (SqlCustomException)
            {
                var registrationResultModel = new RegistrationResultModel();

                registrationResultModel.Success = false;
                registrationResultModel.ErrorMessage = "You could not be registered. Try again.";

                return registrationResultModel;
            }
            catch (NullReferenceException)
            {
                var registrationResultModel = new RegistrationResultModel();

                registrationResultModel.Success = false;
                registrationResultModel.ErrorMessage = "A null was returned when registering";

                return registrationResultModel;
            }
        }

        [HttpPost]
        public async Task<bool> ResendEmail([FromBody] int accountId)
        {
            try
            {
                var emailResult = await _registrationManager.SendVerificationEmail(accountId);

                return emailResult;
            }
            catch (SqlCustomException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
    }
}
