using BusinessModels.UserAccessControl;
using IdentityServices;
using Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Custom_Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        private readonly ILogService _logService;

        public AuthorizationMiddleware(RequestDelegate next, ITokenService tokenService, ILogService logService)
        {
            _next = next;
            _tokenService = tokenService;
            _logService = logService;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;
            if (headers["Scope"].ToString().Contains("id"))
            {
                var userClaims = new List<UserClaimModel>
            {
                new UserClaimModel("scope", "friend_list,read"),
                new UserClaimModel("role", "user"),
                new UserClaimModel("id", "1"),
                new UserClaimModel("username", "TestUsername1"),
                new UserClaimModel("emailAddress", "TestEmailAddress1"),
                new UserClaimModel("firstName", "TestFirstName1"),
                new UserClaimModel("lastName", "TestLastName1"),
                new UserClaimModel("birthdate", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                new UserClaimModel("iss", "TestIssuer1"),
                new UserClaimModel("sub", "TestSubject1"),
                new UserClaimModel("aud", "TestAudience1"),
                new UserClaimModel("exp", "30"),
                new UserClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                new UserClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
            };

                createdToken = _tokenService.CreateToken(userClaims);
                Console.WriteLine("--------------\nToken\n--------------\n" + createdToken);
                _logService.Log(createdToken, LogTarget.All, LogLevel.info, this.ToString(), "API_Dev_Logs");
            }
            else
            {
                string token = String.Empty;
                if (!headers.ContainsKey("Authorization"))
                {
                    httpContext.Response.StatusCode = 403;
                    return Task.CompletedTask;
                }
                string createdToken = String.Empty;


                if (headers["Authorization"].ToString().Contains("Bearer"))
                {
                    var value = headers["Authorization"].ToString().Split(' ')[1];

                    var validated = _tokenService.ValidateToken(value);

                    if (!validated)
                    {
                        httpContext.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    }
                }

                return _next(httpContext);
            }
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
