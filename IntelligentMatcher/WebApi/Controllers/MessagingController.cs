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
        public async Task<bool> SendMessage([FromBody] SendMessageModel messageModel)
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
        public async Task<IEnumerable<MessageModel>> GetChannelMessages([FromBody] int channelId)
        {
            IEnumerable<MessageModel> models = await _messagingService.GetAllChannelMessagesAsync(channelId);

            return models;
        }

        [HttpPost]
        public async Task<IEnumerable<UserIdModel>> GetAllUsersInGroup([FromBody] int channelId)
        {

            IEnumerable<UserIdModel> models = await _messagingService.GetAllUsersInChannelAsync(channelId);

            return models;
        }


        [HttpPost]
        public async Task<bool> AddUserChannel([FromBody] UsernameChannelAdd model)
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

        public async Task<bool> RemoveUserChannel([FromBody] UserChannelModel model)
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

        public async Task<IEnumerable<ChannelModel>> GetUserChannels([FromBody] int userId)
        {

            return await _messagingService.GetAllUserChannelsAsync(userId);

        }

        [HttpPost]
        public async Task<bool> CreateChannel([FromBody] CreateChannelModel channelModel)
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
        public async Task<int> GetChannelOwner([FromBody] int channelid)
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
        public async Task<bool> DeleteChannel([FromBody] int channelid)
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
        public async Task<bool> DeleteMessage([FromBody] int messageId)
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
        public async Task<bool> SetOnline([FromBody] int userId)
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
        public async Task<bool> SetOffline([FromBody] int userId)
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
