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

        public UserProfileModel User { get; set; } = new UserProfileModel();

        public List<SelectListItem> AccountTypes { get; set; } = new List<SelectListItem>();

        public string Popcorn { get; set; }
    }
}
