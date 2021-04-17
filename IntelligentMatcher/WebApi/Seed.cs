using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccountSettings;
using Messaging;
using Security;
using FriendList;
using PublicUserProfile;

namespace WebApi
{
    public class Seed
    {
        public async Task SeedUsers(int seedAmount)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);

            var loginAttempts = await loginAttemptsRepository.GetAllLoginAttempts();

            var accounts = await userAccountRepository.GetAllAccounts();            
            var profiles = await userProfileRepository.GetAllUserProfiles();
            var accountSettings = await userAccountSettingsRepository.GetAllSettings();

            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            var userChannels = await userChannelsRepo.GetAllUserChannelsAsync();

            foreach (var userChannel in userChannels)
            {
                await userChannelsRepo.DeleteUserChannelsByIdAsync(userChannel.Id);
            }

            await DataAccessTestHelper.ReseedAsync("UserChannels", 0, connectionString, dataGateway);

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

            if (loginAttempts != null)
            {
                var numRows = loginAttempts.ToList().Count;

                for (int i = 1; i <= numRows; ++i)
                {
                    await loginAttemptsRepository.DeleteLoginAttemptsById(i);
                }
            }

            await DataAccessTestHelper.ReseedAsync("LoginAttempts", 0, connectionString, dataGateway);

            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            var publicProfiles = await publicUserProfileRepo.GetAllPublicProfiles();

            foreach (var profile in publicProfiles)
            {
                await publicUserProfileRepo.DeletePublicProfileById(profile.Id);
            }

            await DataAccessTestHelper.ReseedAsync("PublicUserProfile", 0, connectionString, dataGateway);

            if (accounts != null)
            {
                var numRows = accounts.ToList().Count;

                for (int i = 1; i <= numRows; ++i)
                {                 
                    await userProfileRepository.DeleteUserProfileByAccountId(i);
                    await userAccountSettingsRepository.DeleteUserAccountSettingsByUserId(i);
                    await userAccountRepository.DeleteAccountById(i);
                }
            }
            
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserProfile", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserAccountSettings", 0, connectionString, dataGateway);

            PublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo);

            for (int i = 1; i < seedAmount; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel();
                UserProfileModel userProfileModel = new UserProfileModel();
                UserAccountSettingsModel userAccountSettingsModel = new UserAccountSettingsModel();

                userAccountModel.Id = i;
                userAccountModel.Username = "TestUser" + i;
                userAccountModel.Password = "" + i;
                userAccountModel.Salt = "" + i;
                userAccountModel.EmailAddress = "TestEmailAddress" + i;
                userAccountModel.AccountType = "TestAccountType" + i;
                userAccountModel.AccountStatus = "TestAccountStatus" + i;
                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

                userProfileModel.Id = i;
                userProfileModel.FirstName = "TestFirstName" + i;
                userProfileModel.Surname = "TestSurname" + i;
                userProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
                userProfileModel.UserAccountId = userAccountModel.Id;
                
                userAccountSettingsModel.Id = i;
                userAccountSettingsModel.UserId = userAccountModel.Id;
                userAccountSettingsModel.FontSize = 12;
                userAccountSettingsModel.FontStyle = "Time New Roman";
                userAccountSettingsModel.ThemeColor = "White";
           
                await userAccountRepository.CreateAccount(userAccountModel);
                await cryptographyService.newPasswordEncryptAsync("TestPassword" + i, i);
                await userProfileRepository.CreateUserProfile(userProfileModel);
                await userAccountSettingsRepository.CreateUserAccountSettings(userAccountSettingsModel);

                PublicUserProfileModel publicUserProfileModel = new PublicUserProfileModel();
                publicUserProfileModel.UserId = userAccountModel.Id;
                
                publicUserProfileModel.Description = "My name is " + userAccountModel.Username;
                publicUserProfileModel.Visibility = "Public";
                publicUserProfileModel.Age = userAccountModel.Id + 20;
                publicUserProfileModel.Hobbies = "These are my hobbies";
                publicUserProfileModel.Intrests = "These are my intrests";
                publicUserProfileModel.Height = "This is how tall I am";
                await publicUserProfileManager.createPublicUserProfileAsync(publicUserProfileModel);




            }


            IMessagingService messagingService = new MessagingService(messagesRepo, channelsRepo, userChannelsRepo, userAccountRepository);

            ChannelModel model = new ChannelModel();
            model.OwnerId = 1;
            model.Name = "Jakes Group";
            await messagingService.CreateChannelAsync(model);

            for(int i = 2; i < 19; i++)
            {
                await messagingService.AddUserToChannelAsync(i, 1);

            }

            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);





            IEnumerable<FriendsListJunctionModel> friends = await friendListRepo.GetAllFriends();
            foreach (var friend in friends)
            {

                await friendListRepo.DeleteFriendListbyId(friend.Id);
            }

            await DataAccessTestHelper.ReseedAsync("FriendsList", 0, connectionString, dataGateway);



            IEnumerable<FriendsListJunctionModel> requets = await friendRequestListRepo.GetAllFriendRequests();
            foreach (var request in requets)
            {

                await friendRequestListRepo.DeleteFriendRequestListbyId(request.Id);
            }

            await DataAccessTestHelper.ReseedAsync("FriendRequestList", 0, connectionString, dataGateway);



            IEnumerable<FriendsListJunctionModel> blocks = await friendBlockListRepo.GetAllFriendBlocks();
            foreach (var block in blocks)
            {

                await friendBlockListRepo.DeleteFriendBlockbyId(block.Id);
            }

            await DataAccessTestHelper.ReseedAsync("FriendBlockList", 0, connectionString, dataGateway);



            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);

            for (int i = 10; i < 15; i++)
            {
                await friendListManager.RequestFriendAsync(1, i);

            }

            await friendListManager.RequestFriendAsync(18, 1);


            for (int i = 2; i < 10; i++)
            {
                await friendListManager.RequestFriendAsync(1, i);

                await friendListManager.ConfirmFriendAsync(1, i);
            }

            for (int i = 3; i < 10; i++)
            {
                await friendListManager.RequestFriendAsync(2, i);

                await friendListManager.ConfirmFriendAsync(2, i);
            }

            await friendListManager.BlockFriendAsync(19, 1);

            MessageModel messageModel = new MessageModel();
            messageModel.ChannelId = 1;
            messageModel.UserId = 1;
            messageModel.Message = "Hi Tim";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 1;
            messageModel.UserId = 2;
            messageModel.Message = "Whats up Jake";
            await messagingService.SendMessageAsync(messageModel);

            model = new ChannelModel();
            model.OwnerId = 3;
            model.Name = "Richards Group";
            await messagingService.CreateChannelAsync(model);

            await messagingService.AddUserToChannelAsync(1, 2);
            await messagingService.AddUserToChannelAsync(2, 2);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 3;
            messageModel.Message = "Hi Jake and Tim";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 1;
            messageModel.Message = "Whats up Richard how are you doing";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 3;
            messageModel.Message = "Im Chilling";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 3;
            messageModel.Message = "Just doing some homework";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 1;
            messageModel.Message = "Oh nice, I started spring break this week and I finished all of my homework a few days ago so im just relaxing playing some warzone";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 3;
            messageModel.Message = "Oh sweet";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 2;
            messageModel.Message = "Whats up guys its me Tim";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 3;
            messageModel.Message = "Hey Tim";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 1;
            messageModel.Message = "Whats up tim";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 2;
            messageModel.Message = "Nothing much guys";
            await messagingService.SendMessageAsync(messageModel);

            messageModel = new MessageModel();
            messageModel.ChannelId = 2;
            messageModel.UserId = 1;
            messageModel.Message = "You guys wanna hop on warzone with me";
            await messagingService.SendMessageAsync(messageModel);

        }
    }
}
