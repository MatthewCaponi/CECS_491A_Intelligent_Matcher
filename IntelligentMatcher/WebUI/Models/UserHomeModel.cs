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

        public UserCreateModel UserSubmission { get; set; } = new UserCreateModel();

        public List<SelectListItem> AccountTypes { get; set; } = new List<SelectListItem>();
    }
}
