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
namespace BusinessLayerUnitTests.Messaging
{
    [TestClass]
    public class MessagingIntegrationTests
    {
        [TestInitialize()]
        public async Task Init()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);

            var settings = await userAccountSettingsRepository.GetAllSettings();

            foreach (var setting in settings)
            {
                await userAccountSettingsRepository.DeleteUserAccountSettingsByUserId(setting.UserId);
            }

            await DataAccessTestHelper.ReseedAsync("UserAccountSettings", 0, connectionString, dataGateway);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }

            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);



            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            var messages = await messagesRepo.GetAllMessagesAsync();

            foreach (var message in messages)
            {
                await messagesRepo.DeleteMessageByIdAsync(message.Id);
            }

            await DataAccessTestHelper.ReseedAsync("Messages", 0, connectionString, dataGateway);


            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            var channels = await channelsRepo.GetAllChannelsAsync();

            foreach (var channel in channels)
            {
                await channelsRepo.DeleteChannelbyIdAsync(channel.Id);
            }

            await DataAccessTestHelper.ReseedAsync("Channels", 0, connectionString, dataGateway);


            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            var userChannels = await userChannelsRepo.GetAllUserChannelsAsync();

            foreach (var userChannel in userChannels)
            {
                await userChannelsRepo.DeleteUserChannelsByIdAsync(userChannel.Id);
            }

            await DataAccessTestHelper.ReseedAsync("Channels", 0, connectionString, dataGateway);


            int i = 1;
            UserAccountModel userAccountModel = new UserAccountModel();
            userAccountModel.Id = i;
            userAccountModel.Username = "TestUser" + i;
            userAccountModel.Password = "" + i;
            userAccountModel.Salt = "" + i;
            userAccountModel.EmailAddress = "TestEmailAddress" + i;
            userAccountModel.AccountType = "TestAccountType" + i;
            userAccountModel.AccountStatus = "TestAccountStatus" + i;
            userAccountModel.CreationDate = DateTimeOffset.UtcNow;
            userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

            await userAccountRepository.CreateAccount(userAccountModel);

            UserAccountRepository userAccountRepo = new UserAccountRepository(new SQLServerGateway(), new ConnectionStringData());
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepo);

            await cryptographyService.NewPasswordEncryptAsync("Password", 1);

            UserAccountSettingsModel userAccountSettingsModel = new UserAccountSettingsModel();
            userAccountSettingsModel.Id = 0;
            userAccountSettingsModel.UserId = 1;
            userAccountSettingsModel.FontSize = 12;
            userAccountSettingsModel.FontStyle = "Time New Roman";
            userAccountSettingsModel.ThemeColor = "White";


            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);


            await userAccountSettingsManager.CreateUserAccountSettingsAsync(userAccountSettingsModel);


            i = 2;
            userAccountModel.Id = i;
            userAccountModel.Username = "TestUser" + i;
            userAccountModel.Password = "" + i;
            userAccountModel.Salt = "" + i;
            userAccountModel.EmailAddress = "TestEmailAddress" + i;
            userAccountModel.AccountType = "TestAccountType" + i;
            userAccountModel.AccountStatus = "TestAccountStatus" + i;
            userAccountModel.CreationDate = DateTimeOffset.UtcNow;
            userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

            await userAccountRepository.CreateAccount(userAccountModel);


            await cryptographyService.NewPasswordEncryptAsync("Password", 1);

            userAccountSettingsModel.Id = 0;
            userAccountSettingsModel.UserId = 1;
            userAccountSettingsModel.FontSize = 12;
            userAccountSettingsModel.FontStyle = "Time New Roman";
            userAccountSettingsModel.ThemeColor = "White";




            await userAccountSettingsManager.CreateUserAccountSettingsAsync(userAccountSettingsModel);

        }


        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);

            var settings = await userAccountSettingsRepository.GetAllSettings();

            foreach (var setting in settings)
            {
                await userAccountSettingsRepository.DeleteUserAccountSettingsByUserId(setting.UserId);
            }

            await DataAccessTestHelper.ReseedAsync("UserAccountSettings", 0, connectionString, dataGateway);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }

            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);



            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            var messages = await messagesRepo.GetAllMessagesAsync();

            foreach (var message in messages)
            {
                await messagesRepo.DeleteMessageByIdAsync(message.Id);
            }

            await DataAccessTestHelper.ReseedAsync("Messages", 0, connectionString, dataGateway);


            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            var channels = await channelsRepo.GetAllChannelsAsync();

            foreach (var channel in channels)
            {
                await channelsRepo.DeleteChannelbyIdAsync(channel.Id);
            }

            await DataAccessTestHelper.ReseedAsync("Channels", 0, connectionString, dataGateway);


            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            var userChannels = await userChannelsRepo.GetAllUserChannelsAsync();

            foreach (var userChannel in userChannels)
            {
                await userChannelsRepo.DeleteUserChannelsByIdAsync(userChannel.Id);
            }

            await DataAccessTestHelper.ReseedAsync("Channels", 0, connectionString, dataGateway);


        }

        [DataTestMethod]
        [DataRow(1, 1, "Sending Test Message")]
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
                await messagingService.SendMessageAsync(model);
            }
            catch
            {
                Assert.IsTrue(false);
            }

            IEnumerable<MessageModel> models = await messagesRepo.GetAllMessagesByChannelIdAsync(channelId);


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
            await messagingService.CreateChannelAsync(model);
     

            Assert.IsTrue(true);


        }

        [DataTestMethod]
        [DataRow(1, 1, "My Channel")]
        public async Task DeleteChannel_ChannelDeletion_ChannelDeleted(int ChannelId, int OwnerId, string ChannelName)
        {



            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;
            model.Name = ChannelName;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
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

            ChannelModel channelModel = await channelsRepo.GetChannelbyIdAsync(ChannelId);
            if(channelModel != null)
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

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
            await messagingService.CreateChannelAsync(model);
            await messagingService.AddUserToChannelAsync(NewUserId, ChannelId);
            try
            {
                IEnumerable<UserIdModel> users = await messagingService.GetAllUsersInChannelAsync(ChannelId);
                bool done = false;
                foreach (UserIdModel user in users)
                {
                    if (user.UserId == NewUserId)
                    {
                        Assert.IsTrue(true);
                        done = true;
                    }
                }
                if (done == false)
                {
                    Assert.IsTrue(false);
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }
       



        }

        [DataTestMethod]
        [DataRow(1, 1, 1, "My Channel", 2)]
        public async Task RemoveUserFromChannel_RemoveUser_UserRemoved(int ChannelId, int UserId, int OwnerId, string ChannelName, int AddUser)
        {



            ChannelModel model = new ChannelModel();
            model.OwnerId = OwnerId;
            model.Name = ChannelName;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);
            IChannelsRepo channelsRepo = new ChannelsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);
            await messagingService.CreateChannelAsync(model);
            await messagingService.AddUserToChannelAsync(AddUser, ChannelId);
            await messagingService.RemoveUserFromChannelAsync(ChannelId, AddUser);

            try
            {
                IEnumerable<UserIdModel> users = await messagingService.GetAllUsersInChannelAsync(ChannelId);
                bool done = false;
                foreach (UserIdModel user in users)
                {
                    if (user.UserId == AddUser)
                    {
                        Assert.IsTrue(false);
                        done = true;
                    }
                }
                if (done == false)
                {
                    Assert.IsTrue(true);
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }
   
        }

    }
}
