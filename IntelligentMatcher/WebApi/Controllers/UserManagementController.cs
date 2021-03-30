using IntelligentMatcher.Services;
using IntelligentMatcher.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using WebApi.ApiModels;

namespace IntelligentMatcherUI.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserManagementController : ControllerBase
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
            return (await _userManager.GetAllUserProfiles()).SuccessValue;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebUserProfileModel>> GetUserProfile(int id)
        {
            return (await _userManager.GetUserProfile(id)).SuccessValue;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SecureUserModel>> GetUserAccounty(int id)
        {
            var userAccount = (await _userManager.GetUserAccount(id)).SuccessValue;
            return new SecureUserModel
            {
                Id = userAccount.Id,
                Username = userAccount.Username,
                Token = _tokenService.CreateToken(userAccount)
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SecureUserModel>> GetUserAccount(int id)
        {
            var userAccount = (await _userManager.GetUserAccount(id)).SuccessValue;
            return new SecureUserModel
            {
                Id = userAccount.Id,
                Username = userAccount.Username,
                Token = _tokenService.CreateToken(userAccount)
            };
        }
    }
}
