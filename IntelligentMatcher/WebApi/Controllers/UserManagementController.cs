using IntelligentMatcher.Services;
using IntelligentMatcher.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserManagementController(IUserManager userManager)
        {
            _userManager = userManager;
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
    }
}
