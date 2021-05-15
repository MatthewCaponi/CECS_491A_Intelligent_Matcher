using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Models;
using System.Collections.Generic;
using FriendList;
using DataAccess;
using DataAccess.Repositories;

using WebApi.Controllers;
using IdentityServices;
using AuthorizationResolutionSystem;
using AuthorizationPolicySystem;
using WebApi;
using WebApi.Access_Information;
using Cross_Cutting_Concerns;
using BusinessModels.UserAccessControl;
using WebApi.Models;
using UserClaimModel = BusinessModels.UserAccessControl.UserClaimModel;

namespace WebApi.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class FriendListController : ApiBaseController
    {

        private readonly IFriendListManager _friendListManager;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;
        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;
        public FriendListController(IFriendListManager friendListManager, IUserAccountRepository userAccountRepository, ITokenService tokenService, IAuthorizationResolutionManager authorizationResolutionManager,
            IAuthorizationPolicyManager authorizationPolicyManager)
        {
            _friendListManager = friendListManager;
            _userAccountRepository = userAccountRepository;
            _tokenService = tokenService;
            _authorizationResolutionManager = authorizationResolutionManager;
            _authorizationPolicyManager = authorizationPolicyManager;
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<FriendListModel>>> GetAllFriends([FromBody] int userId)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var claims = new List<UserClaimModel>();
            claims.Add(new UserClaimModel("Id", userId.ToString()));
            var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:read",
            }, claims);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            try
            {
                return Ok(await _friendListManager.GetAllFriendAsync(userId));
            }
            catch
            {
                return StatusCode(404);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<FriendListModel>>> GetAllRequets([FromBody] int userId)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var claims = new List<UserClaimModel>();
            claims.Add(new UserClaimModel("Id", userId.ToString()));
            var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:read",
            }, claims);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            try
            {
                return Ok(await _friendListManager.GetAllRequestsAsync(userId));
            }
            catch
            {
                return StatusCode(404);
            }
        }

        //[HttpPost]
        public async Task<ActionResult<IEnumerable<FriendListModel>>> GetAllRequetsOutgoing([FromBody] int userId)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var claims = new List<UserClaimModel>();
            claims.Add(new UserClaimModel("Id", userId.ToString()));
            var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:read",
            }, claims);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }
            try
            {
                return Ok(await _friendListManager.GetAllRequestsOutgoingAsync(userId));
            }
            catch
            {
                return StatusCode(404);
            }
        }

        //[HttpPost]
        public async Task<ActionResult<bool>> RemoveFriend([FromBody] DualIdModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<UserClaimModel>();
                claims.Add(new UserClaimModel("Id", ids.UserId.ToString()));
                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:delete",
            }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _friendListManager.RemoveFriendAsync(ids.UserId, ids.FriendId);
                return Ok(true);
            }
            catch
            {
                return StatusCode(404);

            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> BlockFriend([FromBody] DualIdModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<UserClaimModel>();
                claims.Add(new UserClaimModel("Id", ids.UserId.ToString()));
                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:block",
            }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _friendListManager.BlockFriendAsync(ids.UserId, ids.FriendId);
                return Ok(true);
            }
            catch
            {
                return StatusCode(404);

            }

        }


        [HttpPost]
        public async Task<ActionResult<bool>> ApproveFriend([FromBody] DualIdModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<UserClaimModel>();
                claims.Add(new UserClaimModel("Id", ids.UserId.ToString()));
                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:approve",
            }, claims);
                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _friendListManager.ConfirmFriendAsync(ids.UserId, ids.FriendId);
                return Ok(true);
            }
            catch
            {
                return StatusCode(404);

            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> CancelFriendRequest([FromBody] DualIdModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<UserClaimModel>();
                claims.Add(new UserClaimModel("Id", ids.UserId.ToString()));
                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:write",
            }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _friendListManager.CancelFriendRequestAsync(ids.UserId, ids.FriendId);
                return Ok(true);
            }
            catch
            {
                return StatusCode(404);

            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<FriendListModel>>> GetMutualFriends([FromBody] DualIdModel ids)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var claims = new List<UserClaimModel>();
            var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:read",
            }, claims);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }
            try
            {
                return Ok(await _friendListManager.GetMutualFriends(ids.UserId, ids.FriendId));

            }
            catch
            {
                return StatusCode(404);
            }

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<FriendListModel>>> GetAllBlocking([FromBody] int userId)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var claims = new List<UserClaimModel>();
            claims.Add(new UserClaimModel("Id", userId.ToString()));
            var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:read",
            }, claims);
            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            try
            {
                return Ok(await _friendListManager.GetAllBlockingUserAsync(userId));

            }
            catch
            {
                return StatusCode(404);
            }

        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<FriendListModel>>> GetAllBlocks([FromBody] int userId)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var claims = new List<UserClaimModel>();
            claims.Add(new UserClaimModel("Id", userId.ToString()));
            var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:read",
            }, claims);

            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }

            try
            {
                return Ok(await _friendListManager.GetAllBlocksAsync(userId));

            }
            catch
            {
                return StatusCode(404);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateFriendRequest([FromBody] IdUsernameModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<UserClaimModel>();
                claims.Add(new UserClaimModel("Id", ids.UserId.ToString()));
                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
            {
                "friends_list:write",
            }, claims);
                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                UserAccountModel model = await _userAccountRepository.GetAccountByUsername(ids.FriendUsername);
                int friendId = model.Id;
                await _friendListManager.RequestFriendAsync(friendId, ids.UserId);
                return Ok(true);
            }
            catch
            {
                return StatusCode(404);
            }
        }



    }
}
