//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Security;
//using DataAccess;
//using DataAccess.Repositories;
//using Messaging;
//using UserAccountSettings;
//using Models;
//using System.Collections.Generic;
//using WebApi.Controllers;
//using AuthorizationPolicySystem;
//using AuthorizationResolutionSystem;

//namespace IntelligentMatcherUI.Controllers
//{
//    public class SendMessageModel
//    {
//        public int ChannelId { get; set; }
//        public int UserId { get; set; }
//        public string Message { get; set; }

//    }

//    public class CreateChannelModel
//    {
//        public int OwnerId { get; set; }
//        public string Name { get; set; }

//    }

//    public class UsernameChannelAdd
//    {

//        public string Username { get; set; }

//        public int ChannelId { get; set; }

//    }
//    [Route("[controller]/[action]")]
//    [ApiController]
//    public class MessagingController : ApiBaseController
//    {



//        private readonly IMessagingService _messagingService;
//        private readonly IUserAccountRepository _userAccountRepository;
//        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;
//        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;

//        public MessagingController(IMessagingService messagingService, IUserAccountRepository userAccountRepository, 
//            IAuthorizationPolicyManager authorizationPolicyManager, IAuthorizationResolutionManager authorizationResolutionManager)
//        {
//            _messagingService = messagingService;
//            _userAccountRepository = userAccountRepository;
//            _authorizationPolicyManager = authorizationPolicyManager;
//            _authorizationResolutionManager = authorizationResolutionManager;
//        }


//        [HttpPost]
//        public async Task<ActionResult<bool>> SendMessage([FromBody] SendMessageModel messageModel)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging:write", "user", messageModel.UserId.ToString());

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }

//                MessageModel model = new MessageModel();
//                model.ChannelId = messageModel.ChannelId;
//                model.UserId = messageModel.UserId;
//                model.Message = messageModel.Message;

//                await _messagingService.SendMessageAsync(model);
//                return Ok(true);
//            }
//            catch
//            {
//                return StatusCode(404);
//            }

//        }

//        [HttpPost]
//        public async Task<ActionResult<IEnumerable<MessageModel>>> GetChannelMessages([FromBody] int channelId)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging:read", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }

//                try
//                {
//                    IEnumerable<MessageModel> models = await _messagingService.GetAllChannelMessagesAsync(channelId);

//                    return Ok(models);
//                }
//                catch
//                {
//                    return StatusCode(404);

//                }

                
//            } catch
//            {
//                return StatusCode(404);
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult<IEnumerable<UserIdModel>>> GetAllUsersInGroup([FromBody] int channelId)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging.channel:read", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                IEnumerable<UserIdModel> models = await _messagingService.GetAllUsersInChannelAsync(channelId);

//                return Ok(models);
//            }
//            catch
//            {
//                return StatusCode(404);

//            }

//        }

//        [HttpPost]
//        public async Task<ActionResult<bool>> AddUserChannel([FromBody] UsernameChannelAdd model)
//        {
//            try
//            {             
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging.channel:write", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                UserAccountModel userAccountModel = await _userAccountRepository.GetAccountByUsername(model.Username);

//                await _messagingService.AddUserToChannelAsync(userAccountModel.Id, model.ChannelId);

//                return Ok(true);
//            }
//            catch
//            {
//                return StatusCode(404);
//            }

//        }
//        [HttpPost]

//        public async Task<ActionResult<bool>> RemoveUserChannel([FromBody] UserChannelModel model)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging.channel:delete", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                await _messagingService.RemoveUserFromChannelAsync(model.ChannelId, model.UserId);
//                return Ok(true);
//            }
//            catch
//            {
//                return StatusCode(404);
//            }
//        }

//        [HttpPost]

//        public async Task<ActionResult<IEnumerable<ChannelModel>>> GetUserChannels([FromBody] int userId)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging:read", "user", userId.ToString());

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                return Ok(await _messagingService.GetAllUserChannelsAsync(userId));

//            }
//            catch
//            {
//                return StatusCode(404);
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult<bool>> CreateChannel([FromBody] CreateChannelModel channelModel)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging:write", "user", channelModel.OwnerId.ToString());

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                ChannelModel channel = new ChannelModel();
//                channel.Name = channelModel.Name;
//                channel.OwnerId = channelModel.OwnerId;


//                await _messagingService.CreateChannelAsync(channel);
//                return Ok(true);
//            }
//            catch
//            {
//                return StatusCode(404);
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult<int>> GetChannelOwner([FromBody] int channelid)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging.channel:read", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                int ownerId = await _messagingService.GetChannelOwnerAsync(channelid);
//                if (ownerId == 0)
//                {

//                    return StatusCode(404);
//                }

//                return Ok(ownerId);
//            }
//            catch
//            {
//                return StatusCode(404);
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult<bool>> DeleteChannel([FromBody] int channelid)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging:delete", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                await _messagingService.DeleteChannelAsync(channelid);
//                return Ok(true);
//            }
//            catch
//            {
//                return Ok(false);
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult<bool>> DeleteMessage([FromBody] int messageId)
//        {
//            try
//        {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging:delete", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                await _messagingService.DeleteMessageAsync(messageId);
//                return Ok(true);
//            }
//            catch
//            {
//                return Ok(false);
//            }

//        }

//        [HttpPost]
//        public async Task<ActionResult<bool>> SetOnline([FromBody] int userId)
//        {
//            try 
//            { 
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging:read", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                await _messagingService.SetUserOnlineAsync(userId);
//                return Ok(true);
//            }
//            catch
//            {
//                return Ok(false);
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult<bool>> SetOffline([FromBody] int userId)
//        {
//            try
//            {
//                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
//                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy("messaging:read", "user");

//                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
//                {
//                    return StatusCode(403);
//                }
//                await _messagingService.SetUserOfflineAsync(userId);
//                return Ok(true);
//            }
//            catch
//            {
//                return Ok(false);
//            }

//        }
//    }
//}
