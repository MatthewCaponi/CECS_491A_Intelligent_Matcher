using IntelligentMatcher.Services;
using IntelligentMatcher.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IUserAccountService _userAccountService;

        public UserManagementController(IUserManager userManager, IUserAccountService userAccountService)
        {
            _userManager = userManager;
            _userAccountService = userAccountService;
        }

        //[HttpGet]
        /*public async Task<ActionResult<List<string>>> GetAllAccounts()
        {
            return (await _userManager.GetAllUserAccounts());
        }
        */
    }
}
