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
using Services;
using Models.DALListingModels;
using WebApi.Models;

namespace IntelligentMatcherUI.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {

        private readonly IPublicUserProfileManager _publicUserProfileManager;
        private readonly IFriendListManager _friendListManager;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserInteractionService _userInteractionService;
        private readonly ITraditionalListingSearchRepository _traditionalListingSearchRepository;

        public UserProfileController(IPublicUserProfileManager publicUserProfileManager, IFriendListManager friendListManager, IUserAccountRepository userAccountRepository, IUserInteractionService userInteractionService, ITraditionalListingSearchRepository traditionalListingSearchRepository)
        {
            _publicUserProfileManager = publicUserProfileManager;
            _friendListManager = friendListManager;
            _userAccountRepository = userAccountRepository;
            _userInteractionService = userInteractionService;
            _traditionalListingSearchRepository = traditionalListingSearchRepository;
        }


        [HttpPost]
        public async Task<PublicUserProfileModel> GetUserProfile([FromBody] int userId)
        {

            return await _publicUserProfileManager.GetUserProfileAsync(userId);

        }

        [HttpPost]
        public async Task<IEnumerable<DALListingModel>> GetUserListings([FromBody] int userId)
        {

            return await _traditionalListingSearchRepository.GetAllListingsByUserId(userId);

        }


        [HttpPost]
        public async Task<bool> SaveUserProfile([FromBody] PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileManager.EditPublicUserProfileAsync(model);
                return true;
            }
            catch
            {
                return false;
            }


        }

        [HttpPost]
        public async Task<bool> ReportUser([FromBody] UserReportsModel model)
        {
            try
            {
                await _userInteractionService.CreateReportAsync(model);
                return true;
            }
            catch
            {
                return false;
            }

        }


        [HttpPost]
        public async Task<bool> GetViewStatus([FromBody] DualIdModel model)
        {
            try
            {
                string status = await _friendListManager.GetFriendStatusUserIdAsync(model.UserId, model.FriendId);
                var profileModel = await _publicUserProfileManager.GetUserProfileAsync(model.FriendId);
                if (status == "Friends" && profileModel.Visibility == "Friends")
                {
                    return true;

                }
                if (profileModel.Visibility == "Public")
                {
                    return true;

                }
                return false;
            }
            catch
            {
                return false;
            }


        }


        [HttpPost]
        public async Task<FriendStatus> GetFriendStatus([FromBody] DualIdModel model)
        {

            string status = await _friendListManager.GetFriendStatusUserIdAsync(model.UserId, model.FriendId);
            FriendStatus friendStatus = new FriendStatus();
            friendStatus.Status = status;
            return friendStatus;

        }

        [HttpPost]
        public async Task<bool> SetOnline([FromBody] int userId)
        {
            try
            {
                await _publicUserProfileManager.SetUserOnlineAsync(userId);
                return true;
            }
            catch
            {
                return false;
            }


        }

        [HttpPost]
        public async Task<bool> SetOffline([FromBody] int userId)
        {
            try
            {
                await _publicUserProfileManager.SetUserOfflineAsync(userId);
                return true;
            }
            catch
            {
                return false;
            }


        }

        [HttpPost]
        public async Task<NonUserProfileData> GetOtherData([FromBody] int userId)
        {

            NonUserProfileData model = new NonUserProfileData();
            UserAccountModel userAccountModel = await _userAccountRepository.GetAccountById(userId);
            model.Username = userAccountModel.Username;
            string[] dates = userAccountModel.CreationDate.ToString().Split(" ");
            model.JoinDate = dates[0];
            return model;

        }

        [HttpPost]
        public async Task<bool> UploadPhoto()
        {


            try
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
                    if (filetype[filetype.Length - 1].ToLower() == "png" || filetype[filetype.Length - 1].ToLower() == "jpg" || filetype[filetype.Length - 1].ToLower() == "jpeg")
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
                        await _publicUserProfileManager.EditUserProfilePictureAsync(model);
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
            catch
            {
                return false;
            }

         


        }

    }
}

