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
using DataAccessUnitTestes;
using Security;
using UserAccountSettings;
using System.Linq;
using Moq;

namespace BusinessLayerUnitTests.Messaging
{
    [TestClass]
    public class MessagingUnitTests
    {

        [DataTestMethod]
        [DataRow(1, 1, "Sending Test Message")]
        public async Task sendMessage_MessageSent_MessageCreated(int channelId, int userId, string message)
        {

            MessageModel model = new MessageModel();
            model.ChannelId = channelId;
            model.UserId = userId;
            model.Message = message;


            Mock<IMessagesRepo>  messagesRepo = new Mock<IMessagesRepo>();
            Mock<IChannelsRepo> channelsRepo = new Mock<IChannelsRepo>();
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserChannelsRepo> userChannelsRepo = new Mock<IUserChannelsRepo>();
            IMessagingService messagingService = new MessagingService(messagesRepo.Object, channelsRepo.Object, userChannelsRepo.Object, userAccountRepository.Object);
            try
            {
                await messagingService.SendMessageAsync(model);
            }
            catch
            {
                Assert.IsTrue(false);
            }

  


        }

        [DataTestMethod]
        [DataRow(1, 1, "Sending Test Message")]
        public async Task deleteMessage_MessageSentThenDeleted_MessageDeleted(int channelId, int userId, string message)
        {

            MessageModel model = new MessageModel();
            model.ChannelId = channelId;
            model.UserId = userId;
            model.Message = message;


            Mock<IMessagesRepo> messagesRepo = new Mock<IMessagesRepo>();
            Mock<IChannelsRepo> channelsRepo = new Mock<IChannelsRepo>();
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserChannelsRepo> userChannelsRepo = new Mock<IUserChannelsRepo>();
            IMessagingService messagingService = new MessagingService(messagesRepo.Object, channelsRepo.Object, userChannelsRepo.Object, userAccountRepository.Object);
            try
            {
                await messagingService.SendMessageAsync(model);
                await messagingService.DeleteMessageAsync(0);

            }
            catch
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(true);





        }


        [DataTestMethod]
        [DataRow(1, 1, "My Channel")]
        public async Task DeleteChannel_ChannelDeletion_ChannelDeleted(int ChannelId, int OwnerId, string ChannelName)
        {



            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;
            model.Name = ChannelName;
            Mock<IMessagesRepo> messagesRepo = new Mock<IMessagesRepo>();
            Mock<IChannelsRepo> channelsRepo = new Mock<IChannelsRepo>();
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserChannelsRepo> userChannelsRepo = new Mock<IUserChannelsRepo>();
            IMessagingService messagingService = new MessagingService(messagesRepo.Object, channelsRepo.Object, userChannelsRepo.Object, userAccountRepository.Object);
            try
            {
                await messagingService.CreateChannelAsync(model);

            }
            catch
            {
                Assert.IsTrue(false);
            }
            try
            {
                await messagingService.DeleteChannelAsync(ChannelId);
            }
            catch
            {
                Assert.IsTrue(false);
            }

            Assert.IsTrue(true);


        }

        [DataTestMethod]
        [DataRow(1, 1, 1, "My Channel", 2)]
        public async Task AddUserToChannel_AddUser_UserAdded(int ChannelId, int UserId, int OwnerId, string ChannelName, int NewUserId)
        {



            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;
            model.Name = ChannelName;

            Mock<IMessagesRepo> messagesRepo = new Mock<IMessagesRepo>();
            Mock<IChannelsRepo> channelsRepo = new Mock<IChannelsRepo>();
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserChannelsRepo> userChannelsRepo = new Mock<IUserChannelsRepo>();
            IMessagingService messagingService = new MessagingService(messagesRepo.Object, channelsRepo.Object, userChannelsRepo.Object, userAccountRepository.Object);
            try
            {
                await messagingService.CreateChannelAsync(model);
                await messagingService.AddUserToChannelAsync(NewUserId, ChannelId);
            }
            catch
            {
                Assert.IsTrue(false);
            }

            Assert.IsTrue(true);




        }

        [DataTestMethod]
        [DataRow(1, 1, 1, "My Channel", 2)]
        public async Task RemoveUserFromChannel_RemoveUser_UserRemoved(int ChannelId, int UserId, int OwnerId, string ChannelName, int AddUser)
        {



            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;
            model.Name = ChannelName;

            Mock<IMessagesRepo> messagesRepo = new Mock<IMessagesRepo>();
            Mock<IChannelsRepo> channelsRepo = new Mock<IChannelsRepo>();
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserChannelsRepo> userChannelsRepo = new Mock<IUserChannelsRepo>();
            IMessagingService messagingService = new MessagingService(messagesRepo.Object, channelsRepo.Object, userChannelsRepo.Object, userAccountRepository.Object);
            try
            {
                await messagingService.CreateChannelAsync(model);
                await messagingService.AddUserToChannelAsync(AddUser, ChannelId);
                await messagingService.RemoveUserFromChannelAsync(ChannelId, AddUser);
            }
            catch
            {
                Assert.IsTrue(true);
            }

        }



        [DataTestMethod]
        [DataRow(1, 1, 1, "My Channel", 2)]
        public async Task GetChannelOwner_ChannelOwner_RetrievedChannelOwner(int ChannelId, int UserId, int OwnerId, string ChannelName, int NewUserId)
        {



            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;
            model.Name = ChannelName;
            Mock<IMessagesRepo> messagesRepo = new Mock<IMessagesRepo>();
            Mock<IChannelsRepo> channelsRepo = new Mock<IChannelsRepo>();
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();
            Mock<IUserChannelsRepo> userChannelsRepo = new Mock<IUserChannelsRepo>();
            IMessagingService messagingService = new MessagingService(messagesRepo.Object, channelsRepo.Object, userChannelsRepo.Object, userAccountRepository.Object);
            await messagingService.CreateChannelAsync(model);
            try
            {
                int ownerId = await messagingService.GetChannelOwnerAsync(ChannelId);
                Assert.IsTrue(true);

            }
            catch
            {
                Assert.IsTrue(false);
            }
        }




    }
}
