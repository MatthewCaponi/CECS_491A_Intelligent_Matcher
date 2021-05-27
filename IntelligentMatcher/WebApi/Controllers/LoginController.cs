using BusinessModels;
using ControllerModels;
using ControllerModels.LoginModels;
using Exceptions;
using Login;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<Result<LoginResultModel>>> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel.username == null || loginModel.password == null)
            {
                return StatusCode(400);
            }

            var loginResult = await _loginManager.Login(loginModel.username, loginModel.password, loginModel.ipAddress);

            if (loginResult.WasSuccessful)
            {
                var idToken = loginResult.SuccessValue.IdToken;
                var accessToken = loginResult.SuccessValue.AccessToken;
                CookieOptions idOption = new CookieOptions();
                idOption.HttpOnly = false;

                CookieOptions accessOption = new CookieOptions();
                accessOption.HttpOnly = false;

                Response.Cookies.Append("IdToken", idToken, idOption);
                Response.Cookies.Append("AccessToken", accessToken, accessOption);

                return Ok("Success");
            }
            else if (loginResult.ErrorMessage == ErrorMessage.NoMatch)
            {
                return StatusCode(401);
            }
            else if (loginResult.ErrorMessage == ErrorMessage.Null)
            {
                return StatusCode(400);
            }
            else if (loginResult.ErrorMessage == ErrorMessage.TooManyAttempts)
            {
                return StatusCode(429);
            }
            else
            {
                return StatusCode(500);
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

                forgotUsernameResultModel.Success = forgotUsernameResult.WasSuccessful;

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
            catch (SqlCustomException)
            {
                var forgotUsernameResultModel = new ForgotUsernameResultModel();

                forgotUsernameResultModel.Success = false;
                forgotUsernameResultModel.ErrorMessage = "Could not verify the information given. Try again.";

                return forgotUsernameResultModel;
            }
            catch (NullReferenceException)
            {
                var forgotUsernameResultModel = new ForgotUsernameResultModel();

                forgotUsernameResultModel.Success = false;
                forgotUsernameResultModel.ErrorMessage = "A null was returned when checking the inputs.";

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

                forgotPasswordResultModel.Success = forgotPasswordResult.WasSuccessful;

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
            catch (SqlCustomException)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = "Could not verify the information given. Try again.";

                return forgotPasswordResultModel;
            }
            catch (NullReferenceException)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = "A null was returned when checking the inputs.";

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

                forgotPasswordResultModel.Success = forgotPasswordCodeResult.WasSuccessful;

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
            catch (SqlCustomException)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = "Failure to verify the code. Try again.";

                return forgotPasswordResultModel;
            }
            catch (NullReferenceException)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = "A null was returned when checking the code.";

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

                forgotPasswordResultModel.Success = resetPasswordResult.WasSuccessful;

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
            catch (SqlCustomException)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = "The password could not be changed. Try Again.";

                return forgotPasswordResultModel;
            }
            catch (NullReferenceException)
            {
                var forgotPasswordResultModel = new ForgotPasswordResultModel();

                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = "No account has been found.";

                return forgotPasswordResultModel;
            }
        }
    }
}
