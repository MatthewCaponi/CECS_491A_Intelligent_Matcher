using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logging;
using Microsoft.AspNetCore.Mvc;
using UserManagement;
using UserManagement.Models;
/*
namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IUserListManager _userListManager;
        private ILogServiceFactory _logFactory;
        private ILogService _logService;

        public UserController(UserManagement.IUserManager userManager, IUserListManager userListManager, ILogServiceFactory logFactory)
        {
            _userManager = userManager;
            _userListManager = userListManager;
            _logFactory = logFactory;

            _logFactory.AddTarget(TargetType.Text);
            _logService = _logFactory.CreateLogService<UserHomeController>();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserProfile(int id)
        {
            UserInfoModel model = new UserInfoModel();
            model = await _userManager.GetUserInfo(id);

            return View(model);
        }
    }
}*/
