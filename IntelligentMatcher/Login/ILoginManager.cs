﻿using System;
using System.Threading.Tasks;
using BusinessModels;
using UserManagement.Models;

namespace Login
{
    public interface ILoginManager
    {
        Task<Result<WebUserAccountModel>> Login(string username, string password, string IpAddress);
        Task<Result<WebUserAccountModel>> ForgotUsernameValidation(string emailAddress, DateTimeOffset dateOfBirth);
        Task<Result<WebUserAccountModel>> ForgotPasswordValidation(string username, string emailAddress, 
            DateTimeOffset dateOfBirth);
        Task<Result<WebUserAccountModel>> ResetPassword(string password, int accountId);
    }
}
