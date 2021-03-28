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

            MessageModel model = new MessageModel();
            model.ChannelId = messageModel.ChannelId;
            model.UserId = messageModel.UserId;
            model.Message = messageModel.Message;

            await _messagingService.SendMessageAsync(model);
            return true;
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

            UserAccountModel userAccountModel = await _userAccountRepository.GetAccountByUsername(model.Username);

            await _messagingService.AddUserToChannelAsync(userAccountModel.Id, model.ChannelId);

            return true;
        }
        [HttpPost]

        public async Task<bool> RemoveUserChannel([FromBody] UserChannelModel model)
        {

            await _messagingService.RemoveUserFromChannelAsync(model.ChannelId, model.UserId);

            return true;
        }

        [HttpPost]

        public async Task<IEnumerable<ChannelModel>> GetUserChannels([FromBody] int userId)
        {

            return await _messagingService.GetAllUserChannelsAsync(userId);

        }

        [HttpPost]
        public async Task<bool> CreateChannel([FromBody] CreateChannelModel channelModel)
        {

            ChannelModel channel = new ChannelModel();
            channel.Name = channelModel.Name;
            channel.OwnerId = channelModel.OwnerId;


            await _messagingService.CreateChannelAsync(channel);
            return true;
        }


        [HttpPost]
        public async Task<int> GetChannelOwner([FromBody] int channelid)
        {


            int ownerId = await _messagingService.GetChannelOwnerAsync(channelid);
            if (ownerId == 0)
            {

                return -1;
            }

            return ownerId;



        }

        [HttpPost]
        public async Task<bool> DeleteChannel([FromBody] int channelid)
        {

            await _messagingService.DeleteChannelAsync(channelid);

            return true;
        }

        [HttpPost]
        public async Task<bool> DeleteMessage([FromBody] int messageId)
        {

            await _messagingService.DeleteMessageAsync(messageId);

            return true;
        }

    }
}
