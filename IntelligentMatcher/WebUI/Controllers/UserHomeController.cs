using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class UserHomeController : Controller
    {
        private readonly ILogger<UserHomeController> _logger;
        private readonly IUserManager _userManager;
        private readonly IUserListManager _userListManager;

        public UserHomeController(UserManagement.IUserManager userManager, IUserListManager userListManager)
        {
            _userManager = userManager;
            _userListManager = userListManager;
        }

        public async Task<IActionResult> Index()
        {
            var userList = await _userListManager.PopulateListOfUsers();

            
            return View(userList);
        }

        public IActionResult UserInfo()
        {
            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
