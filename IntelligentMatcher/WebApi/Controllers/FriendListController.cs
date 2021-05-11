using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Models;
using System.Collections.Generic;
using FriendList;
using DataAccess;
using DataAccess.Repositories;
using WebApi.Models;
using WebApi.Controllers;
using IdentityServices;
using AuthorizationResolutionSystem;
using AuthorizationPolicySystem;
using WebApi;
using WebApi.Access_Information;

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
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), userId.ToString(), true, false, false);

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
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), userId.ToString(), true, false, false);

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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<FriendListModel>>> GetAllRequetsOutgoing([FromBody] int userId)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), userId.ToString(), true, false, false);

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

        [HttpPost]
        public async Task<ActionResult<bool>> RemoveFriend([FromBody] DualIdModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), ids.UserId.ToString(), true, false, false);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _friendListManager.RemoveFriendAsync(ids.UserId, ids.FriendId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);

            }

        }



        [HttpPost]
        public async Task<ActionResult<bool>> BlockFriend([FromBody] DualIdModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), ids.UserId.ToString(), true, false, false);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _friendListManager.BlockFriendAsync(ids.UserId, ids.FriendId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);

            }

        }


        [HttpPost]
        public async Task<ActionResult<bool>> ApproveFriend([FromBody] DualIdModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), ids.UserId.ToString(), true, false, false);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _friendListManager.ConfirmFriendAsync(ids.UserId, ids.FriendId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);

            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> CancelFriendRequest([FromBody] DualIdModel ids)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), ids.UserId.ToString(), true, false, false);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _friendListManager.CancelFriendRequestAsync(ids.UserId, ids.FriendId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);

            }

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<FriendListModel>>> GetMutualFriends([FromBody] DualIdModel ids)
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(),  true, false, false);

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
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), true, false, false);

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
            var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), true, false, false);

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
                var accessPolicy = _authorizationPolicyManager.ConfigureDefaultPolicy(Resources.friend_list.ToString(), Role.user.ToString(), ids.UserId.ToString(), true, false, false);

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
                return Ok(false);
            }

        }



    }
}
