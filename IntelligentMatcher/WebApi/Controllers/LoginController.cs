using BusinessModels;
using Login;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace WebApi.Controllers
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string ipAddress { get; set; }
    }

    public class ForgotUsernameModel
    {
        public string emailAddress { get; set; }
        public DateTimeOffset dateOfBirth { get; set; }
    }

    public class ForgotPasswordValidationModel
    {
        public string username { get; set; }
        public string emailAddress { get; set; }
        public DateTimeOffset dateOfBirth { get; set; }
    }

    public class ResetPasswordModel
    {
        public string password { get; set; }
        public int accountId { get; set; }
    }

    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;

        public LoginController(ILoginManager loginManager)
        {
            _loginManager = loginManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Result<WebUserAccountModel>>> Login([FromBody] LoginModel loginModel)
        {
            var loginResult = await _loginManager.Login(loginModel.username, loginModel.password, loginModel.ipAddress);

            return loginResult;
        }

        [HttpPost("forgotUsername")]
        public async Task<ActionResult<Result<string>>> ForgotUsername([FromBody] ForgotUsernameModel forgotUsernameModel)
        {
            var forgotUsernameResult = await _loginManager.ForgotUsername(forgotUsernameModel.emailAddress,
                forgotUsernameModel.dateOfBirth);

            return forgotUsernameResult;
        }

        [HttpPost("forgotPasswordValidation")]
        public async Task<ActionResult<Result<WebUserAccountModel>>> ForgotPasswordValidation
            ([FromBody] ForgotPasswordValidationModel forgotPasswordValidationModel)
        {
            var forgotPasswordResult = await _loginManager.ForgotPasswordValidation
                (forgotPasswordValidationModel.username, forgotPasswordValidationModel.emailAddress,
                forgotPasswordValidationModel.dateOfBirth);

            return forgotPasswordResult;
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<Result<WebUserAccountModel>>> ResetPassword
            ([FromBody]ResetPasswordModel resetPasswordModel)
        {
            var resetPasswordResult = await _loginManager.ResetPassword(resetPasswordModel.password,
                resetPasswordModel.accountId);

            return resetPasswordResult;
        }
    }
}
