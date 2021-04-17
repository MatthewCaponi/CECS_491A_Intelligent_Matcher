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
using FriendList;
using PublicUserProfile;
namespace BusinessLayerUnitTests.FriendList
{
    [TestClass]

    public class FriendListIntegrationTests
    {

        [TestInitialize()]
        public async Task Init()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);

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




            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IEnumerable<FriendsListJunctionModel> friends = await friendListRepo.GetAllFriends();

            foreach (var friend in friends)
            {
                await friendListRepo.DeleteFriendListbyId(friend.Id);
            }


            await DataAccessTestHelper.ReseedAsync("FriendsList", 0, connectionString, dataGateway);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IEnumerable<FriendsListJunctionModel> requets = await friendListRepo.GetAllFriends();
            foreach (var request in requets)
            {

                await friendListRepo.DeleteFriendListbyId(request.Id);
            }

            await DataAccessTestHelper.ReseedAsync("FriendRequestList", 0, connectionString, dataGateway);


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


            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            var profiles = await publicUserProfileRepo.GetAllPublicProfiles();

            foreach (var profile in profiles)
            {
                await publicUserProfileRepo.DeletePublicProfileById(profile.Id);
            }

            await DataAccessTestHelper.ReseedAsync("PublicUserProfile", 0, connectionString, dataGateway);



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



            await cryptographyService.newPasswordEncryptAsync("Password", 1);

            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo);

            PublicUserProfileModel publicUserProfileModel = new PublicUserProfileModel();

            publicUserProfileModel.UserId = i;

            await publicUserProfileManager.createPublicUserProfileAsync(publicUserProfileModel);

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

            publicUserProfileModel = new PublicUserProfileModel();
            publicUserProfileModel.UserId = i;
            await publicUserProfileManager.createPublicUserProfileAsync(publicUserProfileModel);

            i = 3;
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
            await cryptographyService.newPasswordEncryptAsync("Password", 1);
            publicUserProfileModel = new PublicUserProfileModel();
            publicUserProfileModel.UserId = i;
            await publicUserProfileManager.createPublicUserProfileAsync(publicUserProfileModel);
            userAccountSettingsModel.Id = 1;
            userAccountSettingsModel.UserId = 2;
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




            IMessagesRepo messagesRepo = new MessagesRepo(dataGateway, connectionString);

            IUserChannelsRepo userChannelsRepo = new UserChannelsRepo(dataGateway, connectionString);
            var userChannels = await userChannelsRepo.GetAllUserChannelsAsync();

            foreach (var userChannel in userChannels)
            {
                await userChannelsRepo.DeleteUserChannelsByIdAsync(userChannel.Id);
            }

            await DataAccessTestHelper.ReseedAsync("UserChannels", 0, connectionString, dataGateway);



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


            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            var profiles = await publicUserProfileRepo.GetAllPublicProfiles();

            foreach (var profile in profiles)
            {
                await publicUserProfileRepo.DeletePublicProfileById(profile.Id);
            }

            await DataAccessTestHelper.ReseedAsync("PublicUserProfile", 0, connectionString, dataGateway);




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


            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IEnumerable<FriendsListJunctionModel> friends = await friendListRepo.GetAllFriends();
            foreach (var friend in friends)
            {

                await friendListRepo.DeleteFriendListbyId(friend.Id);
            }

            await DataAccessTestHelper.ReseedAsync("FriendsList", 0, connectionString, dataGateway);


            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IEnumerable<FriendsListJunctionModel> requets = await friendRequestListRepo.GetAllFriendRequests();
            foreach (var request in requets)
            {

                await friendRequestListRepo.DeleteFriendRequestListbyId(request.Id);
            }

            await DataAccessTestHelper.ReseedAsync("FriendRequestList", 0, connectionString, dataGateway);


            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);

            IEnumerable<FriendsListJunctionModel> blocks = await friendBlockListRepo.GetAllFriendBlocks();
            foreach (var block in blocks)
            {

                await friendBlockListRepo.DeleteFriendBlockbyId(block.Id);
            }

            await DataAccessTestHelper.ReseedAsync("FriendBlockList", 0, connectionString, dataGateway);

        }




        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RequestFriendAsync_FriendRequested_NewFriendRequested(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);

            await friendListManager.RequestFriendAsync(userId1, userId2);

            IEnumerable<FriendsListJunctionModel> model = await friendRequestListRepo.GetAllUserFriendRequests(userId1);

            if (model == null)
            {
                Assert.IsTrue(false);

            }
            if (model.Count() == 0)
            {
                Assert.IsTrue(false);

            }

            foreach (var friend in model)
            {
                if (friend.User2Id == userId2)
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
        [DataRow(1, 2)]
        public async Task ConfirmFreindAsync_FriendConfirmed_NewFriendAdded(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);
            await friendListManager.RequestFriendAsync(userId1, userId2);

            await friendListManager.ConfirmFriendAsync(userId1, userId2);

            IEnumerable<FriendsListJunctionModel> model = await friendListRepo.GetAllUserFriends(userId1);
            if (model == null)
            {
                Assert.IsTrue(false);

            }
            if (model.Count() == 0)
            {
                Assert.IsTrue(false);

            }

            foreach(var friend in model)
            {
                if(friend.User2Id == userId2)
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
        [DataRow(1, 2)]
        public async Task CancelFriendRequest_FriendRequestCancelled_RequestCancelled(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);
            await friendListManager.RequestFriendAsync(userId1, userId2);

            await friendListManager.CancelFriendRequestAsync(userId1, userId2);

            IEnumerable<FriendsListJunctionModel> model = await friendListRepo.GetAllUserFriends(userId1);
            if(model == null)
            {
                Assert.IsTrue(false);
            }
            if (model.Count() == 0)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);
            }


        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task BlockFriendAsync_FriendBlocked_FriendRemovedAndBlocked(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);
            await friendListManager.RequestFriendAsync(userId1, userId2);

            await friendListManager.ConfirmFriendAsync(userId1, userId2);
            await friendListManager.BlockFriendAsync(userId1, userId2);

            IEnumerable<FriendsListJunctionModel> model = await friendListRepo.GetAllUserFriends(userId1);

            if (model.Count() == 0)
            {
                model = await friendBlockListRepo.GetAllUserFriendBlocks(userId1);

                if (model.Count() == 1)
                {
                    Assert.IsTrue(true);

                }
                else
                {
                    Assert.IsTrue(false);
                }
            }
            else
            {
                Assert.IsTrue(false);
            }


 

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllFriendAsync_GetFriends_Checkforfriends(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);
            await friendListManager.RequestFriendAsync(userId1, userId2);

            await friendListManager.ConfirmFriendAsync(userId1, userId2);

            IEnumerable<FriendListModel> models = await friendListManager.GetAllFriendAsync(userId1);
            if (models == null)
            {
                Assert.IsTrue(false);

            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);

            }

            foreach(var friend in models)
            {
                if(friend.userId == userId2)
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
        [DataRow(1, 2)]
        public async Task GetMutualFriends_GetMutualFriends_CheckMutualFriends(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);
            await friendListManager.RequestFriendAsync(userId1, userId2);

            await friendListManager.ConfirmFriendAsync(userId1, userId2);

            await friendListManager.RequestFriendAsync(userId1, 3);

            await friendListManager.ConfirmFriendAsync(userId1, 3);

            await friendListManager.RequestFriendAsync(userId2, 3);

            await friendListManager.ConfirmFriendAsync(userId2, 3);

            IEnumerable<FriendListModel> models = await friendListManager.GetMutualFriends(userId1, userId2);
            if (models == null)
            {
                Assert.IsTrue(false);

            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);

            }

            foreach (var friend in models)
            {
                if (friend.userId == 3)
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
        [DataRow(1, 2)]
        public async Task GetAllFriendRequestAsync_GetFriendRequests_Checkforrequestss(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);
            await friendListManager.RequestFriendAsync(userId1, userId2);

            IEnumerable<FriendListModel> models = await friendListManager.GetAllRequestsAsync(userId1);
            if (models == null)
            {
                Assert.IsTrue(false);

            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);

            }

            foreach (var friend in models)
            {
                if (friend.userId == userId2)
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
        [DataRow(1, 2)]
        public async Task GetAllFriendRequestsOutgoingAsync_GetFriendRequests_Checkforrequestss(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);
            await friendListManager.RequestFriendAsync(userId1, userId2);

            IEnumerable<FriendListModel> models = await friendListManager.GetAllRequestsOutgoingAsync(userId2);
            if (models == null)
            {
                Assert.IsTrue(false);

            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);

            }

            foreach (var friend in models)
            {
                if (friend.userId == userId1)
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
        [DataRow(1, 2)]
        public async Task RemoveFriendAsync_FriendRemoved_FriendRemoved(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);

            await friendListManager.RequestFriendAsync(userId1, userId2);

            await friendListManager.ConfirmFriendAsync(userId1, userId2);

            await friendListManager.RemoveFriendAsync(userId2, userId1);

            IEnumerable<FriendsListJunctionModel> model = await friendListRepo.GetAllUserFriends(userId1);
            if(model == null)
            {
                Assert.IsTrue(false);
            }
            if (model.Count() == 0)
            {
                Assert.IsTrue(true);

            }
            else
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllBlocksAsync_GetAllBlocks_BlocksRecieved(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);

            await friendListManager.BlockFriendAsync(userId1, userId2);



            IEnumerable<FriendListModel> models = await friendListManager.GetAllBlocksAsync(userId1);
            if(models == null)
            {
                Assert.IsTrue(false);
            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            foreach(var model in models)
            {
                if(model.userId == userId2)
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
        [DataRow(1, 2)]
        public async Task GetAllBlockingUserAsync_GetAllBlockings_BlocksRecieved(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);

            await friendListManager.BlockFriendAsync(userId1, userId2);



            IEnumerable<FriendListModel> models = await friendListManager.GetAllBlockingUserAsync(userId1);
            if (models == null)
            {
                Assert.IsTrue(false);
            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            foreach (var model in models)
            {
                if (model.userId == userId2)
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
        [DataRow(1, 2)]
        public async Task GetFriendStatusUserIdAsync_GetStatus_StatusBlocked(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);

            await friendListManager.BlockFriendAsync(userId1, userId2);

            string status = await friendListManager.GetFriendStatusUserIdAsync(userId1, userId2);

            if(status == null)
            {
                Assert.IsTrue(false);
            }
            if(status == "Blocked")
            {
                Assert.IsTrue(true);

            }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetFriendStatusUserIdAsync_GetStatus_StatusRequested(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);

            await friendListManager.RequestFriendAsync(userId1, userId2);

            string status = await friendListManager.GetFriendStatusUserIdAsync(userId1, userId2);

            if (status == null)
            {
                Assert.IsTrue(false);
            }
            if (status == "Requested")
            {
                Assert.IsTrue(true);

            }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetFriendStatusUserIdAsync_GetStatus_StatusFriends(int userId1, int userId2)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);

            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IFriendListManager friendListManager = new FriendListManager(friendListRepo, friendRequestListRepo, userAccountRepository, friendBlockListRepo, publicUserProfileRepo);

            await friendListManager.RequestFriendAsync(userId1, userId2);

            await friendListManager.ConfirmFriendAsync(userId1, userId2);

            string status = await friendListManager.GetFriendStatusUserIdAsync(userId1, userId2);

            if (status == null)
            {
                Assert.IsTrue(false);
            }
            if (status == "Friends")
            {
                Assert.IsTrue(true);

            }
        }


    }
 }
