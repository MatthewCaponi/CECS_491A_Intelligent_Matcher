using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using DataAccess.Repositories.LoginTrackerRepositories;
using DataAccess.Repositories.PageVisitTrackerRepositories;
using DataAccess.Repositories.SearchTrackerRepositories;
using DataAccessUnitTestes;
using IntelligentMatcher.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using PublicUserProfile;
using Security;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccountSettings;
using UserAnalysisManager;
using UserManagement.Services;

namespace BusinessLayerUnitTests.UserAnalysisDashboard
{
    [TestClass]

    public class UserAnalysis_FriendListTrackerTests
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

            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
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

    

        }




        [DataTestMethod]
        public async Task TrackFriendNumber_ReturnAvgFriends()
        {
            //arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ILoginTrackerRepo loginTrackerRepo = new LoginTrackerRepo(dataGateway, connectionString);
            IPageVisitTrackerRepo pageVisitTrackerRepo = new PageVisitTrackerRepo(dataGateway, connectionString);
            ISearchTrackerRepo searchTrackerRepo = new SearchTrackerRepo(dataGateway, connectionString);
            IUserAnalysisService userAnalysisService = new UserAnalysisService(friendListRepo, listingRepository, userAccountRepository,
        loginTrackerRepo, pageVisitTrackerRepo, friendRequestListRepo, searchTrackerRepo);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            await userInteractionService.CreateFriendshipAsync(1, 2);
            try
            {
                int avarageFriends = await userAnalysisService.GetAvgFriends();
                if (avarageFriends != 1)
                {
                    Assert.IsTrue(false);
                }
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);

            }

        }

        [DataTestMethod]
        public async Task TrackFriendNumber_FriendRequest_GivenDay()
        {
            //arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ILoginTrackerRepo loginTrackerRepo = new LoginTrackerRepo(dataGateway, connectionString);
            IPageVisitTrackerRepo pageVisitTrackerRepo = new PageVisitTrackerRepo(dataGateway, connectionString);
            ISearchTrackerRepo searchTrackerRepo = new SearchTrackerRepo(dataGateway, connectionString);
            IUserAnalysisService userAnalysisService = new UserAnalysisService(friendListRepo, listingRepository, userAccountRepository,
        loginTrackerRepo, pageVisitTrackerRepo, friendRequestListRepo, searchTrackerRepo);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            await userInteractionService.CreateFriendRequestAsync(1, 2);
        
                int avarageFriends = await userAnalysisService.FriendRequest_GivenDay(DateTimeOffset.UtcNow);
                if (avarageFriends != 1)
                {
                    Assert.IsTrue(false);
                }
                Assert.IsTrue(true);
        }

        [DataTestMethod]
        public async Task TrackFriendNumber_GetLast12MonthsFriendRequestTrend()
        {
            //arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ILoginTrackerRepo loginTrackerRepo = new LoginTrackerRepo(dataGateway, connectionString);
            IPageVisitTrackerRepo pageVisitTrackerRepo = new PageVisitTrackerRepo(dataGateway, connectionString);
            ISearchTrackerRepo searchTrackerRepo = new SearchTrackerRepo(dataGateway, connectionString);
            IUserAnalysisService userAnalysisService = new UserAnalysisService(friendListRepo, listingRepository, userAccountRepository,
        loginTrackerRepo, pageVisitTrackerRepo, friendRequestListRepo, searchTrackerRepo);
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);

            await userInteractionService.CreateFriendRequestAsync(1, 2);

            IEnumerable<int> avarageFriends = await userAnalysisService.GetLast12MonthsFriendRequestTrend();
            if (avarageFriends == null)
            {
                Assert.IsTrue(false);
            }
            if (avarageFriends.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            if (avarageFriends.Count() != 366)
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(true);

        }


    }
}
