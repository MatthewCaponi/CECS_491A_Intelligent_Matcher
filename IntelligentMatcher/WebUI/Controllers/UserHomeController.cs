using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public UserHomeController(UserManagement.IUserManager userManager, IUserListManager userListManager)
        {
            _userManager = userManager;
            _userListManager = userListManager;
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
                    AccountCreationDate = x.AccountCreationDate
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
        public async Task<IActionResult> Create(UserCreateModel userCreateModel)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            int id = await _userManager.CreateUser(userCreateModel);

            return View();
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
