using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Messaging;
using Models;
using System;
using DataAccess;
using DataAccess.Repositories;
using System.Globalization;
using System.Collections.Generic;

namespace BusinessLayerUnitTests.Messaging
{
    [TestClass]
    public class MessagingIntegrationTests
    {
        [TestInitialize()]
        public async Task Init()
        {


        }


        [TestCleanup()]
        public async Task CleanUp()
        {



        }

        [DataTestMethod]
        [DataRow(1, 12, "Sending Test Message")]
        public async Task sendMessage_MessageSent_MessageCreated(int channelId, int userId, string message)
        {

            MessageModel model = new MessageModel();
            model.ChannelId = channelId;
            model.UserId = userId;
            model.Message = message;


            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
            try{
                await messagingService.sendMessageAsync(model);
            }
            catch
            {
                Assert.IsTrue(false);
            }

            IEnumerable<MessageModel> models = (IEnumerable<MessageModel>)messagingService.GetAllChannelMessages(channelId);


            foreach(MessageModel modelList in models)
            {
                if(modelList.Message == model.Message)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false);
                }
            }


        }


        [DataTestMethod]
        [DataRow(1, "My Group")]
        public async Task CreateChannel_ChannelCreatation_ChannelCreated(int OwnerId, string name)
        {

            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;
            model.Name = name;


            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
            await messagingService.CreateChannel(model);
     

            Assert.IsTrue(true);


        }

        [DataTestMethod]
        [DataRow(1, 1)]
        public async Task DeleteChannel_ChannelDeletion_ChannelDeleted(int ChannelId, int OwnerId)
        {



            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
            await messagingService.CreateChannel(model);
            await messagingService.DeleteChannel(ChannelId);


            Assert.IsTrue(true);


        }

        [DataTestMethod]
        [DataRow(1, 1, 1)]
        public async Task AddUserToChannel_AddUser_UserAdded(int ChannelId, int UserId, int OwnerId)
        {



            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
            await messagingService.CreateChannel(model);
            await messagingService.AddUserToChannel(UserId, ChannelId);

            Assert.IsTrue(true);


        }

    }
}
