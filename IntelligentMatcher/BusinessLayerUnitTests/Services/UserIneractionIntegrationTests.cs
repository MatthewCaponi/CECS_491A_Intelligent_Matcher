using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Models;
using DataAccess;
using DataAccess.Repositories;
using System.Globalization;
using DataAccessUnitTestes;
using Security;
using UserAccountSettings;
using System.Linq;
using FriendList;
using PublicUserProfile;
using Services;
using UserManagement.Services;
using IntelligentMatcher.Services;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]

    public class UserIneractionIntegrationTests
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


            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);

            IEnumerable<UserReportsModel> reports = await userReportsRepo.GetAllReports();
            foreach (var report in reports)
            {

                await userReportsRepo.DeleteReportById(report.Id);
            }

            await DataAccessTestHelper.ReseedAsync("UserReports", 0, connectionString, dataGateway);

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
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);


            PublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileService);

            PublicUserProfileModel publicUserProfileModel = new PublicUserProfileModel();

            publicUserProfileModel.UserId = i;

            await publicUserProfileManager.CeatePublicUserProfileAsync(publicUserProfileModel);

            UserAccountSettingsModel userAccountSettingsModel = new UserAccountSettingsModel();
            userAccountSettingsModel.Id = 0;
            userAccountSettingsModel.UserId = 1;
            userAccountSettingsModel.FontSize = 12;
            userAccountSettingsModel.FontStyle = "Time New Roman";
            userAccountSettingsModel.ThemeColor = "White";

            IPasswordValidationService authenticationService = new PasswordValidationService(userAccountRepository);
            IAccountSettingsService userAccountSettingsManager = new AccountSettingsService(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);


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
            await publicUserProfileManager.CeatePublicUserProfileAsync(publicUserProfileModel);

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
            await publicUserProfileManager.CeatePublicUserProfileAsync(publicUserProfileModel);
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

            IUserReportsRepo userReportsRepo  = new UserReportsRepo(dataGateway, connectionString);

            IEnumerable<UserReportsModel> reports = await userReportsRepo.GetAllReports();
            foreach (var report in reports)
            {

                await userReportsRepo.DeleteReportById(report.Id);
            }

            await DataAccessTestHelper.ReseedAsync("UserReports", 0, connectionString, dataGateway);

        }


        [DataTestMethod]
        [DataRow(1, 2, "I am creating a report")]
        public async Task CreateReportAsync_CreateReport_ReportCreated(int userId1, int userId2, string report)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            UserReportsModel model = new UserReportsModel();
            model.ReportedId = userId1;
            model.ReportingId = userId2;
            model.Report = report;
            try
            {

        
                await userInteractionService.CreateReportAsync(model);

                IEnumerable<UserReportsModel> userReports = await userReportsRepo.GetAllReports();

                if(userReports == null)
                {
                    Assert.IsTrue(false);
                }
                if(userReports.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach(var userReport in userReports)
                {
                    if(userReport.Report == report && userReport.ReportedId == userId1 && userReport.ReportingId == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task CreateFriendshipAsync_CreateFriendship_FriendshipCreated(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            try
            {

            
                await userInteractionService.CreateFriendshipAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await friendListRepo.GetAllFriends();

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach (var friendsListJunctionModel in friendsListJunctionModels)
                {
                    if (friendsListJunctionModel.User1Id == userId1 && friendsListJunctionModel.User2Id == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllUserFriendRequests_CreatRequest_RequestCreated(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            try
            {

            
                await userInteractionService.CreateFriendRequestAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllUserFriendRequests(userId1);

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach (var friendsListJunctionModel in friendsListJunctionModels)
                {
                    if (friendsListJunctionModel.User1Id == userId1 && friendsListJunctionModel.User2Id == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllUserFriendRequestsOutgoing_CreatRequest_RequestCreated(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            try
            {
                await userInteractionService.CreateFriendRequestAsync(userId2, userId1);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllUserFriendRequestsOutgoing(userId1);

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach (var friendsListJunctionModel in friendsListJunctionModels)
                {
                    if (friendsListJunctionModel.User1Id == userId1 && friendsListJunctionModel.User2Id == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task CreateFriendRequestAsync_CreatRequest_RequestCreated(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            try
            {


                await userInteractionService.CreateFriendRequestAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await friendRequestListRepo.GetAllFriendRequests();

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach (var friendsListJunctionModel in friendsListJunctionModels)
                {
                    if (friendsListJunctionModel.User1Id == userId1 && friendsListJunctionModel.User2Id == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch   
            {
                Assert.IsTrue(false);
            }

        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllUserFriends_CreatRequest_RequestCreated(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);


            try
            {
                await userInteractionService.CreateFriendshipAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllUserFriends(userId2);

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach (var friendsListJunctionModel in friendsListJunctionModels)
                {
                    if (friendsListJunctionModel.User1Id == userId1 && friendsListJunctionModel.User2Id == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch     
                {
                    Assert.IsTrue(false);
                }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task CreateBlockAsync_CreatBlock_BlockCreated(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);


            try
            {


                await userInteractionService.CreateBlockAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await friendBlockListRepo.GetAllFriendBlocks();

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach (var friendsListJunctionModel in friendsListJunctionModels)
                {
                    if (friendsListJunctionModel.User1Id == userId1 && friendsListJunctionModel.User2Id == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllUserFriendBlocks_CreatBlock_BlockCreated(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);


            try
            {


                await userInteractionService.CreateBlockAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllUserFriendBlocks(userId1);

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach (var friendsListJunctionModel in friendsListJunctionModels)
                {
                    if (friendsListJunctionModel.User1Id == userId1 && friendsListJunctionModel.User2Id == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllBlockingUsers_CreatBlock_BlockCreated(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);


            try
            {


                await userInteractionService.CreateBlockAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllBlockingUsers(userId1);

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(false);
                }
                foreach (var friendsListJunctionModel in friendsListJunctionModels)
                {
                    if (friendsListJunctionModel.User1Id == userId1 && friendsListJunctionModel.User2Id == userId2)
                    {
                        Assert.IsTrue(true);

                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }



        [DataTestMethod]
        [DataRow(1, 2, "I am creating a report")]
        public async Task RemoveReportAsync_RemoveReport_ReportRemoved(int userId1, int userId2, string report)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);


            UserReportsModel model = new UserReportsModel();
            model.ReportedId = userId1;
            model.ReportingId = userId2;
            model.Report = report;
            try
            {

            
                await userInteractionService.CreateReportAsync(model);

                await userInteractionService.RemoveReportAsync(1);

                IEnumerable<UserReportsModel> userReports = await userReportsRepo.GetAllReports();

                if (userReports == null)
                {
                    Assert.IsTrue(false);
                }
                if (userReports.Count() == 0)
                {
                    Assert.IsTrue(true);
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RemoveFriendAsync_RemoveFriend_FriendshipRemoved(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            try
            {

            
                await userInteractionService.CreateFriendshipAsync(userId1, userId2);
                await userInteractionService.RemoveFriendAsync(userId1, userId2);


                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await friendListRepo.GetAllFriends();

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(true);
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RemoveFriendRequestAsync_RemoveRequest_RequestRemoved(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            try
            {

           
                await userInteractionService.CreateFriendRequestAsync(userId1, userId2);

                await userInteractionService.RemoveFriendRequestAsync(userId1, userId2);


                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await friendRequestListRepo.GetAllFriendRequests();

                if (friendsListJunctionModels == null)
                {
                    Assert.IsTrue(false);
                }
                if (friendsListJunctionModels.Count() == 0)
                {
                    Assert.IsTrue(true);
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RemoveFriendBlockAsync_RemoveBlock_BlockRemoved(int userId1, int userId2)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);


            try
            {

            
            await userInteractionService.CreateBlockAsync(userId1, userId2);

            await userInteractionService.RemoveFriendBlockAsync(userId1, userId2);


            IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await friendBlockListRepo.GetAllFriendBlocks();

            if (friendsListJunctionModels == null)
            {
                Assert.IsTrue(false);
            }
            if (friendsListJunctionModels.Count() == 0)
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
