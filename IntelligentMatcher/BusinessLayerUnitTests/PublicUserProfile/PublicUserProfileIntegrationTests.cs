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
using PublicUserProfile;
namespace BusinessLayerUnitTests.PublicUserProfile
{
    [TestClass]
    public class PublicUserProfileIntegrationTests
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


            await cryptographyService.newPasswordEncryptAsync("Password", 1);

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

        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task createPublicUserProfileAsync_UserProfileCreated_SuccessfulCreation(int userId)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            await publicUserProfileManager.createPublicUserProfileAsync(model);

            IEnumerable<PublicUserProfileModel> models = await publicUserProfileRepo.GetAllPublicProfiles();

            if (models == null)
            {
                Assert.IsTrue(false);
            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            foreach(var profile in models)
            {
                if(profile.UserId == userId)
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
        [DataRow(1, "Description", "Job", "Goal", 21, "Gender", "Ethnicity", "SexualOrientation", "Height", "Visible", "Online", "Photo", "Intrests", "Hobbies")]
        public async Task editPublicUserProfileAsync_EditProfile_ProfileSuccessfullyEdited(int userId, string description, string job, string goals, int age, string gender, string ethnicity, string sexualOrientation, string height, string visibility, string status, string photo, string intrests, string hobbies)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            await publicUserProfileManager.createPublicUserProfileAsync(model);

            model.Description = description;
            model.Jobs = job;
            model.Goals = goals;
            model.Age = age;
            model.Gender = gender;
            model.Ethnicity = ethnicity;
            model.SexualOrientation = sexualOrientation;
            model.Height = height;
            model.Visibility = visibility;
            model.Status = status;
            model.Photo = photo;
            model.Intrests = intrests;
            model.Hobbies = hobbies;

            await publicUserProfileManager.editPublicUserProfileAsync(model);


            IEnumerable<PublicUserProfileModel> models = await publicUserProfileRepo.GetAllPublicProfiles();

            if (models == null)
            {
                Assert.IsTrue(false);
            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            foreach (var profile in models)
            {
                if (profile.Description == description && profile.Hobbies == hobbies && profile.Jobs == job && profile.Goals == goals && profile.Age == age && profile.Gender == gender && profile.Ethnicity == ethnicity && profile.SexualOrientation == sexualOrientation && profile.Height == height && profile.Visibility == visibility &&   profile.Intrests == intrests)
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
        [DataRow(1, "Photo")]
        public async Task editUserProfilePicture_EditPhoto_PhotoSuccessfullyEdited(int userId, string photo)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            await publicUserProfileManager.createPublicUserProfileAsync(model);


            model.Photo = photo;


            await publicUserProfileManager.editUserProfilePicture(model);


            IEnumerable<PublicUserProfileModel> models = await publicUserProfileRepo.GetAllPublicProfiles();

            if (models == null)
            {
                Assert.IsTrue(false);
            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            foreach (var profile in models)
            {
                if (profile.Photo == photo )
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
        [DataRow(1)]
        public async Task setUserOffline_SetOffline_UserSetOffline(int userId)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            await publicUserProfileManager.createPublicUserProfileAsync(model);


            await publicUserProfileManager.setUserOnline(userId);


            await publicUserProfileManager.setUserOffline(userId);


            IEnumerable<PublicUserProfileModel> models = await publicUserProfileRepo.GetAllPublicProfiles();

            if (models == null)
            {
                Assert.IsTrue(false);
            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            foreach (var profile in models)
            {
                if (profile.Status == "Offline")
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
        [DataRow(1)]
        public async Task setUserOnline_SetOnline_UserSetOnline(int userId)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            await publicUserProfileManager.createPublicUserProfileAsync(model);


            await publicUserProfileManager.setUserOnline(userId);



            IEnumerable<PublicUserProfileModel> models = await publicUserProfileRepo.GetAllPublicProfiles();

            if (models == null)
            {
                Assert.IsTrue(false);
            }
            if (models.Count() == 0)
            {
                Assert.IsTrue(false);
            }
            foreach (var profile in models)
            {
                if (profile.Status == "Online")
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
        [DataRow(1, "Description", "Job", "Goal", 21, "Gender", "Ethnicity", "SexualOrientation", "Height", "Visible", "Online", "Photo", "Intrests", "Hobbies")]
        public async Task GetUserProfile_GetsProfile_ProfileGetSuccess(int userId, string description, string job, string goals, int age, string gender, string ethnicity, string sexualOrientation, string height, string visibility, string status, string photo, string intrests, string hobbies)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            await publicUserProfileManager.createPublicUserProfileAsync(model);

            model.Description = description;
            model.Jobs = job;
            model.Goals = goals;
            model.Age = age;
            model.Gender = gender;
            model.Ethnicity = ethnicity;
            model.SexualOrientation = sexualOrientation;
            model.Height = height;
            model.Visibility = visibility;
            model.Status = status;
            model.Photo = photo;
            model.Intrests = intrests;
            model.Hobbies = hobbies;

            await publicUserProfileManager.editPublicUserProfileAsync(model);


            PublicUserProfileModel profile = await publicUserProfileManager.GetUserProfile(userId);

       
            if (profile.Description == description && profile.Hobbies == hobbies && profile.Jobs == job && profile.Goals == goals && profile.Age == age && profile.Gender == gender && profile.Ethnicity == ethnicity && profile.SexualOrientation == sexualOrientation && profile.Height == height && profile.Visibility == visibility &&  profile.Intrests == intrests)
            {
                Assert.IsTrue(true);
             }
             else
             {
                 Assert.IsTrue(false);
              }
           }


        }


    }

