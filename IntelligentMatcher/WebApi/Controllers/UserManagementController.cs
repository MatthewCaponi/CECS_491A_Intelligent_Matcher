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
using AuthorizationResolutionSystem;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserManagementController : ApiBaseController
    {
        private readonly IUserManager _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;

        public UserManagementController(IUserManager userManager, ITokenService tokenService, IAuthorizationResolutionManager authorizationResolutionManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authorizationResolutionManager = authorizationResolutionManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<WebUserAccountModel>>> GetAllUserAccounts()
        {
            return (await _userManager.GetAllUserAccounts()).SuccessValue;
        }
   
        [HttpGet]
        public async Task<ActionResult<List<WebUserProfileModel>>> GetAllUserProfiles()
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);

            if(!_authorizationResolutionManager.Authorize(token))
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
