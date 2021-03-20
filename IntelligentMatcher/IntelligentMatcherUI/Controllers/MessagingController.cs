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

    [ApiController]
    [Route("[controller]")]
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


        [HttpPost("sendmessage")]
        public async Task<bool> SendMessageAsync([FromBody] SendMessageModel messageModel)
        {

            MessageModel model = new MessageModel();
            model.ChannelId = messageModel.ChannelId;
            model.UserId = messageModel.UserId;
            model.Message = messageModel.Message;


            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);

            await messagingService.sendMessageAsync(model);
            return true;
        }

        [HttpPost("getmessages")]
        public async Task<IEnumerable<MessageModel>> GetChannelMessagesAsync([FromBody] int channelId)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);

            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository); 
            IEnumerable<MessageModel>  models = await messagingService.GetAllChannelMessagesAsync(channelId);

            return models;
        }

        [HttpPost("getchannelusers")]
        public async Task<IEnumerable<UserIdModel>> GetAllUsersInGroup([FromBody] int channelId)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
            IEnumerable<UserIdModel> models = await messagingService.GetAllUsersInChannelAsync(channelId);

            return models;
        }


        [HttpPost("addusertogroup")]
        public async Task<bool> AddUserChannel([FromBody] UsernameChannelAdd model)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);

            UserAccountModel userAccountModel = await userAccountRepository.GetAccountByUsername(model.Username);

            await messagingService.AddUserToChannelAsync(userAccountModel.Id, model.ChannelId);

            return true;
        }
        [HttpPost("removeuserchannel")]

        public async Task<bool> RemoveUserChannel([FromBody] UserChannelModel model)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);


            await messagingService.RemoveUserFromChannelAsync(model.ChannelId, model.UserId);

            return true;
        }

        [HttpPost("getuserchannels")]

        public async Task<IEnumerable<ChannelModel>> GetUserChannels([FromBody] int userId)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);


            return await messagingService.GetAllUserChannelsAsync(userId);

        }

        [HttpPost("createchannel")]
        public async Task<bool> CreateGroupAsync([FromBody] CreateChannelModel channelModel)
        {

            ChannelModel channel = new ChannelModel();
            channel.Name = channelModel.Name;
            channel.OwnerId = channelModel.OwnerId;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);

            await messagingService.CreateChannelAsync(channel);
            return true;
        }


        [HttpPost("getchannelowner")]
        public async Task<int> GetChannelOwner([FromBody] int channelid)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);



            int ownerId = await messagingService.GetChannelOwnerAsync(channelid);
            if (ownerId == 0)
            {

                return -1;
            }

            return ownerId;



        }

        [HttpPost("deletechannel")]
        public async Task<bool> DeleteChannel([FromBody] int channelid)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
            await messagingService.DeleteChannelAsync(channelid);

            return true;
        }

    }
}
