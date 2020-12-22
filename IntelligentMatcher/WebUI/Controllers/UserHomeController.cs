using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using UserManagement;
using UserManagement.Models;
using WebUI.Models;
using static Models.UserProfileModel;

namespace WebUI.Controllers
{
    public class UserHomeController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IUserListManager _userListManager;
        private ILogServiceFactory _logFactory;
        private ILogService _logService;

        public UserHomeController(UserManagement.IUserManager userManager, IUserListManager userListManager, ILogServiceFactory logFactory)
        {
            _userManager = userManager;
            _userListManager = userListManager;
            _logFactory = logFactory;

            _logFactory.AddTarget(TargetType.Text);
            _logService = _logFactory.CreateLogService<UserHomeController>();
        }

        public async Task<IActionResult> Index()
        {
            var userList = await _userListManager.PopulateListOfUsers();

            UserHomeModel model = new UserHomeModel();
            userList.ForEach(x =>
            {
                model.UserList.Add(new UserListModel
                {
                    UserId = x.UserId,
                    Username = x.Username,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AccountCreationDate = x.AccountCreationDate,
                    accountStatus = x.accountStatus
                });
            });

            List<string> accountTypes = Enum.GetValues(typeof(AccountType)).Cast<AccountType>().Select(x => x.ToString()).ToList();

            accountTypes.ForEach(x =>
            {
                model.AccountTypes.Add(new SelectListItem { Text = x });
            });
            
            return View(model);  
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel user)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            _logService.LogInfo($"Username: { user.Username} \nPassword: {user.Password}" +
                $" \nEmail: {user.email} \nFirst Name: {user.FirstName} \nLastName: {user.LastName}" +
                $" \nDate of Birth: {user.DateOfBirth} \nAccount Creation Date {user.AccountCreationDate}");
            int id = await _userManager.CreateUser(user);
            

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UsernameTest(UserCreateModel model)
        {
            if (ModelState.IsValid == false)
            {
                 _logService.LogDebug($"Username is: {model.Username}");
                return RedirectToAction("Index");
            }

            _logService.LogDebug($"Username is: {model.Username}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DumDum(string Popcorn)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            _logService.LogDebug($"Popcorn is: {Popcorn}");

            return RedirectToAction("Index");
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
