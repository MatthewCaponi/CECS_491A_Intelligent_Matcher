﻿using BusinessModels;
using ControllerModels;
using ControllerModels.LoginModels;
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
            var loginResultModel = new LoginResultModel();

            if (loginModel.username == "" || loginModel.password == "")
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

        [HttpPost]
        public async Task<ForgotUsernameResultModel> ForgotUsername([FromBody] ForgotInformationModel forgotInformationModel)
        {
            var forgotUsernameResultModel = new ForgotUsernameResultModel();

            if(forgotInformationModel.emailAddress == "")
            {
                forgotUsernameResultModel.Success = false;
                forgotUsernameResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return forgotUsernameResultModel;
            }
            var forgotUsernameResult = await _loginManager.ForgotUsername(forgotInformationModel.emailAddress,
                forgotInformationModel.dateOfBirth);

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

        [HttpPost]
        public async Task<ForgotPasswordResultModel> ForgotPasswordValidation
            ([FromBody] ForgotInformationModel forgotInformationModel)
        {
            var forgotPasswordResultModel = new ForgotPasswordResultModel();

            if (forgotInformationModel.emailAddress == "" || forgotInformationModel.username == "")
            {
                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return forgotPasswordResultModel;
            }

            var forgotPasswordResult = await _loginManager.ForgotPasswordValidation
                (forgotInformationModel.username, forgotInformationModel.emailAddress,
                forgotInformationModel.dateOfBirth);

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

        [HttpPost]
        public async Task<ForgotPasswordResultModel> ForgotPasswordCodeInput
            ([FromBody] ForgotPasswordCodeInputModel forgotPasswordCodeInputModel)
        {
            var forgotPasswordResultModel = new ForgotPasswordResultModel();

            if (forgotPasswordCodeInputModel.code == "")
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

        [HttpPost]
        public async Task<ForgotPasswordResultModel> ResetPassword
            ([FromBody]ResetPasswordModel resetPasswordModel)
        {
            var forgotPasswordResultModel = new ForgotPasswordResultModel();

            if (resetPasswordModel.password == "")
            {
                forgotPasswordResultModel.Success = false;
                forgotPasswordResultModel.ErrorMessage = ErrorMessage.Null.ToString();

                return forgotPasswordResultModel;
            }
            else if (resetPasswordModel.password.Length >= 8 && resetPasswordModel.password.Any(char.IsDigit)
                && resetPasswordModel.password.Any(char.IsUpper) && resetPasswordModel.password.Any(char.IsLower))
            {
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

            forgotPasswordResultModel.Success = false;
            forgotPasswordResultModel.ErrorMessage = ErrorMessage.InvalidPassword.ToString();

            return forgotPasswordResultModel;
        }
    }
}
