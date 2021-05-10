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
                return _next(httpContext);
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

                    _logService.Log(validated.ToString(), LogTarget.All, LogLevel.info, this.ToString(), "API_Dev_Logs");
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
