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
using Microsoft.AspNetCore.Http;
using IdentityServices;
using AuthorizationResolutionSystem;
using AuthorizationPolicySystem;
using WebApi;
using WebApi.Access_Information;
using WebApi.Controllers;

namespace WebApi.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ApiBaseController
    {

        private readonly IPublicUserProfileManager _publicUserProfileManager;
        private readonly IFriendListManager _friendListManager;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserInteractionService _userInteractionService;
        private readonly ITraditionalListingSearchRepository _traditionalListingSearchRepository;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;
        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;

        public UserProfileController(IPublicUserProfileManager publicUserProfileManager, IFriendListManager friendListManager, IUserAccountRepository userAccountRepository, IUserInteractionService userInteractionService, ITraditionalListingSearchRepository traditionalListingSearchRepository, ITokenService tokenService, IAuthorizationResolutionManager authorizationResolutionManager,
            IAuthorizationPolicyManager authorizationPolicyManager)
        {
            _publicUserProfileManager = publicUserProfileManager;
            _friendListManager = friendListManager;
            _userAccountRepository = userAccountRepository;
            _userInteractionService = userInteractionService;
            _traditionalListingSearchRepository = traditionalListingSearchRepository;
            _tokenService = tokenService;
            _authorizationResolutionManager = authorizationResolutionManager;
            _authorizationPolicyManager = authorizationPolicyManager;
        }


        [HttpPost]
        public async Task<ActionResult<PublicUserProfileModel>> GetUserProfile([FromBody] int userId)
        {

            try
            {
                return Ok(await _publicUserProfileManager.GetUserProfileAsync(userId));

            }
            catch
            {
                return StatusCode(404);

            }

        }



        [HttpPost]
        public async Task<ActionResult<IEnumerable<DALListingModel>>> GetUserListings([FromBody] int userId)
        {

            try
            {
                return Ok(await _traditionalListingSearchRepository.GetAllListingsByUserId(userId));

            }
            catch
            {
                return StatusCode(404);

            }
        }


        [HttpPost]
        public async Task<ActionResult<bool>> SaveUserProfile([FromBody] PublicUserProfileModel model)
        {
            try
            {

                await _publicUserProfileManager.EditPublicUserProfileAsync(model);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }


        }

        [HttpPost]
        public async Task<ActionResult<bool>> ReportUser([FromBody] UserReportsModel model)
        {
            try
            {

                await _userInteractionService.CreateReportAsync(model);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }

        }


        [HttpPost]
        public async Task<ActionResult<bool>> GetViewStatus([FromBody] DualIdModel model)
        {
            try
            {

                string status = await _friendListManager.GetFriendStatusUserIdAsync(model.UserId, model.FriendId);
                var profileModel = await _publicUserProfileManager.GetUserProfileAsync(model.FriendId);
                if (status == "Friends" && profileModel.Visibility == "Friends")
                {
                    return Ok(true);

                }
                if (profileModel.Visibility == "Public")
                {
                    return Ok(true);

                }
                return Ok(false);
            }
            catch
            {
                return Ok(false);
            }


        }


        [HttpPost]
        public async Task<ActionResult<FriendStatus>> GetFriendStatus([FromBody] DualIdModel model)
        {

            try
            {
                string status = await _friendListManager.GetFriendStatusUserIdAsync(model.UserId, model.FriendId);
                FriendStatus friendStatus = new FriendStatus();
                friendStatus.Status = status;
                return Ok(friendStatus);
            }
            catch
            {
                return StatusCode(404);

            }



        }

        [HttpPost]
        public async Task<ActionResult<bool>> SetOnline([FromBody] int userId)
        {
            try
            {
 
                await _publicUserProfileManager.SetUserOnlineAsync(userId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }


        }

        [HttpPost]
        public async Task<ActionResult<bool>> SetOffline([FromBody] int userId)
        {
            try
            {

                await _publicUserProfileManager.SetUserOfflineAsync(userId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }


        }

        [HttpPost]
        public async Task<ActionResult<NonUserProfileData>> GetOtherData([FromBody] int userId)
        {

            try 
            {
                NonUserProfileData model = new NonUserProfileData();
                UserAccountModel userAccountModel = await _userAccountRepository.GetAccountById(userId);
                model.Username = userAccountModel.Username;
                string[] dates = userAccountModel.CreationDate.ToString().Split(" ");
                model.JoinDate = dates[0];
                return Ok(model);
            }
            catch
            {
                return StatusCode(404);
            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> UploadPhoto()
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
                        return Ok(true);
                    }
                    else
                    {
                        return Ok(false);
                    }

                }
                else
                {

                    return Ok(false);
                }
            }
            catch
            {
                return Ok(false);
            }




        }

    }
}

