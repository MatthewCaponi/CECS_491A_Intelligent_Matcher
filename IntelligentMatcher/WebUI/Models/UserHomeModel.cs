using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement;
using UserManagement.Models;
using static Models.UserProfileModel;

namespace WebUI.Models
{
    public class UserHomeModel
    {
        public List<UserListModel> UserList { get; set; } = new List<UserListModel>();

        public WebUserProfileModel User { get; set; } = new WebUserProfileModel();

        public List<SelectListItem> AccountTypes { get; set; } = new List<SelectListItem>();

        public string Popcorn { get; set; }
    }
}
