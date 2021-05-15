using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Security;
using DataAccess;
using DataAccess.Repositories;
using Messaging;
using UserAccountSettings;
using Models;
using System.Collections.Generic;
using WebApi.Controllers;
using AuthorizationPolicySystem;
using AuthorizationResolutionSystem;

namespace IntelligentMatcherUI.Controllers
{
    public class SendMessageModel
    {
        public int ChannelId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }

    }

    public class CreateChannelModel
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }

    }

    public class UsernameChannelAdd
    {

        public string Username { get; set; }

        public int ChannelId { get; set; }

    }
    [Route("[controller]/[action]")]
    [ApiController]
    public class MessagingController : ApiBaseController
    {



        private readonly IMessagingService _messagingService;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;

        public MessagingController(IMessagingService messagingService, IUserAccountRepository userAccountRepository,
            IAuthorizationPolicyManager authorizationPolicyManager, IAuthorizationResolutionManager authorizationResolutionManager)
        {
            _messagingService = messagingService;
            _userAccountRepository = userAccountRepository;
            _authorizationPolicyManager = authorizationPolicyManager;
            _authorizationResolutionManager = authorizationResolutionManager;
        }


        [HttpPost]
        public async Task<ActionResult<bool>> SendMessage([FromBody] SendMessageModel messageModel)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", messageModel.UserId.ToString()));
                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "messaging:send"
                    }, claims);
                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }

                MessageModel model = new MessageModel();
                model.ChannelId = messageModel.ChannelId;
                model.UserId = messageModel.UserId;
                model.Message = messageModel.Message;

                await _messagingService.SendMessageAsync(model);
                return Ok(true);
            }
            catch
            {
                return StatusCode(404);
            }

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetChannelMessages([FromBody] UserChannelModel model)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", model.UserId.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "messaging:get"
                    }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }

                try
                {
                    IEnumerable<MessageModel> models = await _messagingService.GetAllChannelMessagesAsync(model.ChannelId);

                    return Ok(models);
                }
                catch
                {
                    return StatusCode(404);

                }


            }
            catch
            {
                return StatusCode(404);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserIdModel>>> GetAllUsersInGroup([FromBody] UserChannelModel model)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", model.UserId.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "messaging.users:get"
                    }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                IEnumerable<UserIdModel> models = await _messagingService.GetAllUsersInChannelAsync(model.ChannelId);

                return Ok(models);
            }
            catch
            {
                return StatusCode(404);

            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddUserChannel([FromBody] UsernameChannelAdd model)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "messaging.channels:addUser"
                    }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                UserAccountModel userAccountModel = await _userAccountRepository.GetAccountByUsername(model.Username);

                await _messagingService.AddUserToChannelAsync(userAccountModel.Id, model.ChannelId);

                return Ok(true);
            }
            catch
            {
                return StatusCode(404);
            }

        }
        [HttpPost]

        public async Task<ActionResult<bool>> RemoveUserChannel([FromBody] UserChannelModel model)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", model.UserId.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "messaging.channels:removeUser"
                    }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _messagingService.RemoveUserFromChannelAsync(model.ChannelId, model.UserId);
                return Ok(true);
            }
            catch
            {
                return StatusCode(404);
            }
        }

        [HttpPost]

        public async Task<ActionResult<IEnumerable<ChannelModel>>> GetUserChannels([FromBody] int userId)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", userId.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "channels.user:getall"
                    }, claims);
                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                return Ok(await _messagingService.GetAllUserChannelsAsync(userId));

            }
            catch
            {
                return StatusCode(404);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateChannel([FromBody] CreateChannelModel channelModel)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "channels:create"
                    }, claims);
                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                ChannelModel channel = new ChannelModel();
                channel.Name = channelModel.Name;
                channel.OwnerId = channelModel.OwnerId;


                await _messagingService.CreateChannelAsync(channel);
                return Ok(true);
            }
            catch
            {
                return StatusCode(404);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> GetChannelOwner([FromBody] UserChannelModel model)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", model.UserId.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "channels:getowner"
                    }, claims);
                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                int ownerId = await _messagingService.GetChannelOwnerAsync(model.ChannelId);
                if (ownerId == 0)
                {

                    return StatusCode(404);
                }

                return Ok(ownerId);
            }
            catch
            {
                return StatusCode(404);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> DeleteChannel([FromBody] UserChannelModel model)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", model.UserId.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "channels:delete"
                    }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _messagingService.DeleteChannelAsync(model.ChannelId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> DeleteMessage([FromBody] UserChannelModel model)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", model.UserId.ToString()));


                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "message:delete"
                    }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _messagingService.DeleteMessageAsync(model.ChannelId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> SetOnline([FromBody] int userId)
        {
            try
            {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", userId.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "channel:setonline"
                    }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _messagingService.SetUserOnlineAsync(userId);
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
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);

                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", userId.ToString()));

                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "channel:setoffline"
                    }, claims);

                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }
                await _messagingService.SetUserOfflineAsync(userId);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }

        }
    }
}
