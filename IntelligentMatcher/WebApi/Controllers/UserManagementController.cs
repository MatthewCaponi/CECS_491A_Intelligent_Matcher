using IdentityServices;
using IntelligentMatcher.Services;
using IntelligentMatcher.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using WebApi.Models;
using Cross_Cutting_Concerns;
using WebApi.Controllers;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserManagementController : ApiBaseController
    {
        private readonly IUserManager _userManager;
        private readonly ITokenService _tokenService;

        public UserManagementController(IUserManager userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WebUserAccountModel>>> GetAllUserAccounts()
        {
            return (await _userManager.GetAllUserAccounts()).SuccessValue;
        }
   
        [HttpGet]
        public async Task<ActionResult<List<WebUserProfileModel>>> GetAllUserProfiles()
        {
            ExtractHeader(HttpContext, "Authorization", ',', 1);
            var decodedToken = _tokenService.DecodeToken(token);

            decodedToken.Claims.ToList().ForEach(a => { Console.WriteLine(a.Type + "-" + a.Value); });
            List<UserClaimModel> userClaims= new List<UserClaimModel>();

            foreach (var claim in decodedToken.Claims)
            {
                var userClaim = ModelConverterService.ConvertTo(claim, new UserClaimModel());
                userClaims.Add(userClaim);
            };

            userClaims.ForEach(a => { Console.WriteLine(a.Type + ": " + a.Value); });

            var scope = userClaims.Where(a => a.Type == "Scope").FirstOrDefault();
            var role = userClaims.Where(a => a.Type == "Role").FirstOrDefault();

            string [] scopes = scope.Value.ToString().Split(',');

            //ClaimsPrinciple claimsPrinciple = new ClaimsPrinciple()
            //{
            //    Id = 0,
            //    Claims = userClaims,
            //    Scopes = scopes
            //}
            if (!(scopes.Contains("User Management") && scopes.Contains("Read") && role.Value == "Admin"))
            {
                return StatusCode(403);
            }
     
            return Ok((await _userManager.GetAllUserProfiles()).SuccessValue);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebUserProfileModel>> GetUserProfile(int id)
        {
            return (await _userManager.GetUserProfile(id)).SuccessValue;
        }
    }
}
