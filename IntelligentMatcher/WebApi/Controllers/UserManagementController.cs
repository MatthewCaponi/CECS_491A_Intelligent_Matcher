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
        public ActionResult<List<WebUserAccountModel>> GetAllUserAccounts()
        {
            return  _userManager.GetAllUserAccounts().Result.SuccessValue;
        }
   
        [HttpGet]
        public ActionResult<List<WebUserProfileModel>> GetAllUserProfiles()
        {
            return _userManager.GetAllUserProfiles().Result.SuccessValue;
        }

        [HttpGet("{id}")]
        public ActionResult<WebUserProfileModel> GetUserProfile(int id)
        {
            return _userManager.GetUserProfile(id).Result.SuccessValue;
        }
    }
}
