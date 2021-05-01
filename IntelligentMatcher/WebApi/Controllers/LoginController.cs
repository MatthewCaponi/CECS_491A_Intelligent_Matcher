using BusinessModels;
using ControllerModels;
using ControllerModels.LoginModels;
using Exceptions;
using Login;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;

        public LoginController(ILoginManager loginManager)
        {
            _loginManager = loginManager;
        }

        [HttpPost]
        public async Task<LoginResultModel> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var loginResultModel = new LoginResultModel();

                if (loginModel.username == null || loginModel.password == null)
                {
                    loginResultModel.Success = false;
                    loginResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return loginResultModel;
                }

                var loginResult = await _loginManager.Login(loginModel.username, loginModel.password, loginModel.ipAddress);

                loginResultModel.Success = loginResult.Success;

                if (loginResultModel.Success)
                {
                    loginResultModel.Username = loginResult.SuccessValue.Username;
                    loginResultModel.AccountType = loginResult.SuccessValue.AccountType.ToString();
                    loginResultModel.AccountStatus = loginResult.SuccessValue.AccountStatus.ToString();
                }
                else
                {
                    loginResultModel.ErrorMessage = loginResult.ErrorMessage.ToString();
                }

                return loginResultModel;
            }
            catch (SqlCustomException e)
            {
                var loginResultModel = new LoginResultModel();

                loginResultModel.Success = false;
                loginResultModel.ErrorMessage = e.Message;

                return loginResultModel;
            }
            catch (NullReferenceException e)
            {
                var loginResultModel = new LoginResultModel();

                loginResultModel.Success = false;
                loginResultModel.ErrorMessage = e.Message;

                return loginResultModel;
            }
        }

        [HttpPost]
        public async Task<ForgotUsernameResultModel> ForgotUsername([FromBody] ForgotInformationModel forgotInformationModel)
        {
            try
            {
                var forgotUsernameResultModel = new ForgotUsernameResultModel();

                if (forgotInformationModel.emailAddress == null || forgotInformationModel.dateOfBirth == null)
                {
                    forgotUsernameResultModel.Success = false;
                    forgotUsernameResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return forgotUsernameResultModel;
                }
                var forgotUsernameResult = await _loginManager.ForgotUsername(forgotInformationModel.emailAddress,
                    DateTimeOffset.Parse(forgotInformationModel.dateOfBirth));

                forgotUsernameResultModel.Success = forgotUsernameResult.Success;

                if (forgotUsernameResultModel.Success)
                {
                    forgotUsernameResultModel.Username = forgotUsernameResult.SuccessValue;
                }
                else
                {
                    forgotUsernameResultModel.ErrorMessage = forgotUsernameResult.ErrorMessage.ToString();
                }

                return forgotUsernameResultModel;
            }
            catch (SqlCustomException e)
            {
                var forgotUsernameResultModel = new ForgotUsernameResultModel();

                forgotUsernameResultModel.Success = false;
                forgotUsernameResultModel.ErrorMessage = e.Message;

                return forgotUsernameResultModel;
            }
            catch (NullReferenceException e)
            {
                var forgotUsernameResultModel = new ForgotUsernameResultModel();

                forgotUsernameResultModel.Success = false;
                forgotUsernameResultModel.ErrorMessage = e.Message;

                return forgotUsernameResultModel;
            }
        }

        [HttpPost]
        public async Task<ForgotPasswordResultModel> ForgotPasswordValidation
            ([FromBody] ForgotInformationModel forgotInformationModel)
        {
            try
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                if (forgotInformationModel.emailAddress == null || forgotInformationModel.username == null ||
                    forgotInformationModel.dateOfBirth == null)
                {
                    forgotPasswordResultModel.Success = false;
                    forgotPasswordResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return forgotPasswordResultModel;
                }

                var forgotPasswordResult = await _loginManager.ForgotPasswordValidation
                    (forgotInformationModel.username, forgotInformationModel.emailAddress,
                    DateTimeOffset.Parse(forgotInformationModel.dateOfBirth));

                forgotPasswordResultModel.Success = forgotPasswordResult.Success;

                if (forgotPasswordResultModel.Success)
                {
                    forgotPasswordResultModel.AccountId = forgotPasswordResult.SuccessValue.Id;
                }
                else
                {
                    forgotPasswordResultModel.ErrorMessage = forgotPasswordResult.ErrorMessage.ToString();
                }

                return forgotPasswordResultModel;
            }
            catch (SqlCustomException e)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = e.Message;

                return forgotPasswordResultModel;
            }
            catch (NullReferenceException e)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = e.Message;

                return forgotPasswordResultModel;
            }
        }

        [HttpPost]
        public async Task<ForgotPasswordResultModel> ForgotPasswordCodeInput
            ([FromBody] ForgotPasswordCodeInputModel forgotPasswordCodeInputModel)
        {
            try
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                if (forgotPasswordCodeInputModel.code == null)
                {
                    forgotPasswordResultModel.Success = false;
                    forgotPasswordResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return forgotPasswordResultModel;
                }

                var forgotPasswordCodeResult = await _loginManager.ForgotPasswordCodeInput
                    (forgotPasswordCodeInputModel.code, forgotPasswordCodeInputModel.accountId);

                forgotPasswordResultModel.Success = forgotPasswordCodeResult.Success;

                if (forgotPasswordResultModel.Success)
                {
                    forgotPasswordResultModel.AccountId = forgotPasswordCodeResult.SuccessValue.Id;
                }
                else
                {
                    forgotPasswordResultModel.ErrorMessage = forgotPasswordCodeResult.ErrorMessage.ToString();
                }

                return forgotPasswordResultModel;
            }
            catch (SqlCustomException e)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = e.Message;

                return forgotPasswordResultModel;
            }
            catch (NullReferenceException e)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = e.Message;

                return forgotPasswordResultModel;
            }
        }

        [HttpPost]
        public async Task<ForgotPasswordResultModel> ResetPassword
            ([FromBody]ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                if (resetPasswordModel.password == null)
                {
                    forgotPasswordResultModel.Success = false;
                    forgotPasswordResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                    return forgotPasswordResultModel;
                }
                var resetPasswordResult = await _loginManager.ResetPassword(resetPasswordModel.password,
                    resetPasswordModel.accountId);

                forgotPasswordResultModel.Success = resetPasswordResult.Success;

                if (forgotPasswordResultModel.Success)
                {
                    forgotPasswordResultModel.AccountId = resetPasswordResult.SuccessValue.Id;
                }
                else
                {
                    forgotPasswordResultModel.ErrorMessage = resetPasswordResult.ErrorMessage.ToString();
                }

                return forgotPasswordResultModel;
            }
            catch (SqlCustomException e)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = e.Message;

                return forgotPasswordResultModel;
            }
            catch (NullReferenceException e)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = e.Message;

                return forgotPasswordResultModel;
            }
        }
    }
}
