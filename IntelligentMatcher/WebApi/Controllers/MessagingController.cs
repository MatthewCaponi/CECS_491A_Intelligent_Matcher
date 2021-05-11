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

namespace WebApi.Controllers
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
    public class MessagingController : ControllerBase
    {



        private readonly IMessagingService _messagingService;
        private readonly IUserAccountRepository _userAccountRepository;

        public MessagingController(IMessagingService messagingService, IUserAccountRepository userAccountRepository)
        {
            _messagingService = messagingService;
            _userAccountRepository = userAccountRepository;
        }


        [HttpPost]
        public async Task<ActionResult<bool>> SendMessage([FromBody] SendMessageModel messageModel)
        {
            try
            {
                MessageModel model = new MessageModel();
                model.ChannelId = messageModel.ChannelId;
                model.UserId = messageModel.UserId;
                model.Message = messageModel.Message;

                await _messagingService.SendMessageAsync(model);
                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetChannelMessages([FromBody] int channelId)
        {
            try
            {
                IEnumerable<MessageModel> models = await _messagingService.GetAllChannelMessagesAsync(channelId);

                return Ok(models);
            }
            catch
            {
                return StatusCode(404);

            }


        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserIdModel>>> GetAllUsersInGroup([FromBody] int channelId)
        {
            try
            {
                IEnumerable<UserIdModel> models = await _messagingService.GetAllUsersInChannelAsync(channelId);

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
                UserAccountModel userAccountModel = await _userAccountRepository.GetAccountByUsername(model.Username);

                await _messagingService.AddUserToChannelAsync(userAccountModel.Id, model.ChannelId);

                return true;
            }
            catch
            {
                return false;
            }

        }
        [HttpPost]

        public async Task<ActionResult<bool>> RemoveUserChannel([FromBody] UserChannelModel model)
        {
            try
            {
                await _messagingService.RemoveUserFromChannelAsync(model.ChannelId, model.UserId);
                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpPost]

        public async Task<ActionResult<IEnumerable<ChannelModel>>> GetUserChannels([FromBody] int userId)
        {
            try
            {
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
                ChannelModel channel = new ChannelModel();
                channel.Name = channelModel.Name;
                channel.OwnerId = channelModel.OwnerId;


                await _messagingService.CreateChannelAsync(channel);
                return true;
            }
            catch
            {
                return false;
            }

        }


        [HttpPost]
        public async Task<ActionResult<int>> GetChannelOwner([FromBody] int channelid)
        {

            try
            {
                int ownerId = await _messagingService.GetChannelOwnerAsync(channelid);
                if (ownerId == 0)
                {

                    return -1;
                }

                return ownerId;
            }
            catch
            {
                return -1;
            }




        }

        [HttpPost]
        public async Task<ActionResult<bool>> DeleteChannel([FromBody] int channelid)
        {
            try
            {
                await _messagingService.DeleteChannelAsync(channelid);
                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> DeleteMessage([FromBody] int messageId)
        {
            try
            {
                await _messagingService.DeleteMessageAsync(messageId);
                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> SetOnline([FromBody] int userId)
        {
            try
            {
                await _messagingService.SetUserOnlineAsync(userId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SetOffline([FromBody] int userId)
        {
            try
            {
                await _messagingService.SetUserOfflineAsync(userId);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
