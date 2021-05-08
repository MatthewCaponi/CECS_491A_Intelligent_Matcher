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
using BusinessModels.UserAccessControl;
using UserClaimModel = BusinessModels.UserAccessControl.UserClaimModel;
using AuthorizationPolicySystem;
using WebApi;
using WebApi.Access_Information;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserManagementController : ApiBaseController
    {
        private readonly IUserManager _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;
        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;

        public UserManagementController(IUserManager userManager, ITokenService tokenService, IAuthorizationResolutionManager authorizationResolutionManager,
            IAuthorizationPolicyManager authorizationPolicyManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authorizationResolutionManager = authorizationResolutionManager;
            _authorizationPolicyManager = authorizationPolicyManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<WebUserAccountModel>>> GetAllUserAccounts()
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.user_management.ToString(), Role.admin.ToString(), true, false, false);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            return Ok((await _userManager.GetAllUserAccounts()).SuccessValue);
        }

        [HttpGet]
        public async Task<ActionResult<List<WebUserProfileModel>>> GetAllUserProfiles()
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.user_management.ToString(), Role.admin.ToString(), true, false, false);

            if(!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            return Ok((await _userManager.GetAllUserProfiles()).SuccessValue);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebUserProfileModel>> GetUserProfile(int id)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            //var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.user_management.ToString(), Role.admin.ToString(), id.ToString(), true, false, false);
            var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("user_management:read", id.ToString());


            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            return Ok((await _userManager.GetUserProfile(id)).SuccessValue);
        }
    }
}
