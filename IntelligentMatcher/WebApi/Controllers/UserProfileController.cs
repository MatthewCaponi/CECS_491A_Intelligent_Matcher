using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Models;
using System.Collections.Generic;
using FriendList;
using DataAccess;
using PublicUserProfile;
using DataAccess.Repositories;
using System.IO;
using System.Net.Http.Headers;

using System.Linq;    


using System.Net.Http;    


namespace IntelligentMatcherUI.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {

        public class OtherData
        {
            public string Username { get; set; }
            public string JoinDate { get; set; }
        }

        public class dualIdModel
        {
            public int UserId { get; set; }
            public int FriendId { get; set; }
        }




        public class FriendStatus
        {
            public string Status { get; set; }
        }

        /*
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
        private readonly IPublicUserProfileManager _publicUserProfileManager;
        private readonly IFriendListManager _friendListManager;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserProfileController(IPublicUserProfileManager publicUserProfileManager, IFriendListManager friendListManager, IUserAccountRepository userAccountRepository)
        {
            _publicUserProfileManager = publicUserProfileManager;
            _friendListManager = friendListManager;
            _userAccountRepository = userAccountRepository;
        }


        [HttpPost]
        public async Task<PublicUserProfileModel> GetUserProfile([FromBody] int userId)
        {

            return await _publicUserProfileManager.GetUserProfile(userId);

        }

        [HttpPost]
        public async Task<bool> SaveUserProfile([FromBody] PublicUserProfileModel model)
        {

            await _publicUserProfileManager.editPublicUserProfileAsync(model);
            return true;

        }


        [HttpPost]
        public async Task<bool> GetViewStatus([FromBody] dualIdModel model)
        {

            string status = await _friendListManager.GetFriendStatusUserIdAsync(model.UserId, model.FriendId);
            var profileModel = await _publicUserProfileManager.GetUserProfile(model.FriendId);
            if(status == "Friends" && profileModel.Visibility == "Friends")
            {
                return true;

            }
            if (profileModel.Visibility == "Public")
            {
                return true;

            }
            return false;

        }


        [HttpPost]
        public async Task<FriendStatus> GetFriendStatus([FromBody] dualIdModel model)
        {

            string status = await _friendListManager.GetFriendStatusUserIdAsync(model.UserId, model.FriendId);
            FriendStatus friendStatus = new FriendStatus();
            friendStatus.Status = status;
            return friendStatus;

        }

        [HttpPost]
        public async Task<bool> SetOnline([FromBody] int userId)
        {

            await _publicUserProfileManager.setUserOnline(userId);

            return true;

        }

        [HttpPost]
        public async Task<bool> SetOffline([FromBody] int userId)
        {

            await _publicUserProfileManager.setUserOffline(userId);

            return true;

        }

        [HttpPost]
        public async Task<OtherData> GetOtherData([FromBody] int userId)
        {

            OtherData model = new OtherData();
            UserAccountModel userAccountModel = await _userAccountRepository.GetAccountById(userId);
            model.Username = userAccountModel.Username;
            string[] dates = userAccountModel.CreationDate.ToString().Split(" ");
            model.JoinDate = dates[0];
            return model;

        }

        [HttpPost]
        public async Task<bool> UploadPhoto()
        {
                int userId = 0;
                foreach (string key in Request.Form.Keys)
                {
                    if (key.StartsWith("userId"))
                    {
                       userId = Convert.ToInt32(Request.Form[key]);
                    }
                }

                var postedFile = Request.Form.Files[0];
                var uploadFolder = Path.Combine("..\\IntelligentMatcherFrontend\\intelligent-matcher-web-ui\\public\\uploaded");
                if (postedFile.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(postedFile.ContentDisposition)
                        .FileName.Trim('"');
                    string[] filetype = fileName.Split(".");
                    if(filetype[filetype.Length - 1].ToLower() == "png" || filetype[filetype.Length - 1].ToLower() == "jpg" || filetype[filetype.Length - 1].ToLower() == "jpeg")
                        {
                            string newFileName = userId.ToString() + "profileImage." + filetype[filetype.Length - 1];
                            var finalPath = Path.Combine(uploadFolder, newFileName);
                            using (var fileStream = new FileStream(finalPath, FileMode.Create))
                            {
                                postedFile.CopyTo(fileStream);
                            }
                            PublicUserProfileModel model = new PublicUserProfileModel();
                            model.UserId = userId;
                            model.Photo = newFileName;
                            await _publicUserProfileManager.editUserProfilePicture(model);
                            return true;
                        }
                        else
                        {
                             return false;
                        }
             
                }
                else
                {

                    return false;
                }
         


        }

    }
}

