using BusinessModels;
using ControllerModels;
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
        public async Task<ActionResult<Result<WebUserAccountModel>>> Login([FromBody] LoginModel loginModel)
        {
            var loginResult = await _loginManager.Login(loginModel.username, loginModel.password, loginModel.ipAddress);

            return loginResult;
        }

        [HttpPost]
        public async Task<ActionResult<Result<string>>> ForgotUsername([FromBody] ForgotInformationModel forgotInformationModel)
        {
            var forgotUsernameResult = await _loginManager.ForgotUsername(forgotInformationModel.emailAddress,
                forgotInformationModel.dateOfBirth);

            return forgotUsernameResult;
        }

        [HttpPost]
        public async Task<ActionResult<Result<WebUserAccountModel>>> ForgotPasswordValidation
            ([FromBody] ForgotInformationModel forgotInformationModel)
        {
            var forgotPasswordResult = await _loginManager.ForgotPasswordValidation
                (forgotInformationModel.username, forgotInformationModel.emailAddress,
                forgotInformationModel.dateOfBirth);

            return forgotPasswordResult;
        }

        [HttpPost]
        public async Task<ActionResult<Result<WebUserAccountModel>>> ForgotPasswordCodeInput
            ([FromBody] ForgotPasswordCodeInputModel forgotPasswordCodeInputModel)
        {
            var forgotPasswordCodeResult = await _loginManager.ForgotPasswordCodeInput
                (forgotPasswordCodeInputModel.code, forgotPasswordCodeInputModel.accountId);

            return forgotPasswordCodeResult;
        }

        [HttpPost]
        public async Task<ActionResult<Result<WebUserAccountModel>>> ResetPassword
            ([FromBody]ResetPasswordModel resetPasswordModel)
        {
            var resetPasswordResult = await _loginManager.ResetPassword(resetPasswordModel.password,
                resetPasswordModel.accountId);

            return resetPasswordResult;
        }
    }
}
