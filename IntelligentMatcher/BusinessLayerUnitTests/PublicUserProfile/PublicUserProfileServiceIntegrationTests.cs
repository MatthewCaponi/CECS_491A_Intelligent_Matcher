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
using Moq;
using Services;
using UserManagement.Services;
using IntelligentMatcher.Services;

namespace BusinessLayerUnitTests.PublicUserProfile
{
    [TestClass]

    public class PublicUserProfileServiceIntegrationTests
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
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                PublicUserProfileModel profile = await publicUserProfileRepo.GetPublicProfilebyUserId(userId);

                if (profile == null)
                {
                    Assert.IsTrue(false);
                }

                if (profile.UserId == userId)
                {
                    Assert.IsTrue(true);
                }
                else
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
        [DataRow(1)]
        public async Task GetUserProfileAsync_UserProfileGot_SuccessfulRecieved(int userId)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);



            PublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileService);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                PublicUserProfileModel profile = await publicUserProfileService.GetUserProfileAsync(userId);

                if (profile == null)
                {
                    Assert.IsTrue(false);
                }

                if (profile.UserId == userId)
                {
                    Assert.IsTrue(true);
                }
                else
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
        [DataRow(1, "Photo")]
        public async Task ChangeProfilePictureAsync_EditPhoto_PhotoSuccessfullyEdited(int userId, string photo)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();


            try
            {
                model.UserId = userId;
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Photo = photo;
                await publicUserProfileService.ChangeProfilePictureAsync(model);

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }




        }


        [DataTestMethod]
        [DataRow(1)]
        public async Task SetUserOfflineAsync_SetOffline_UserSetOffline(int userId)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);


                await publicUserProfileService.SetUserOnlineAsync(userId);


                await publicUserProfileService.SetUserOfflineAsync(userId);


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
            catch
            {
                Assert.IsTrue(false);
            }



        }


        [DataTestMethod]
        [DataRow(1)]
        public async Task SetUserOnlineAsync_SetOnline_UserSetOnline(int userId)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);


                await publicUserProfileService.SetUserOnlineAsync(userId);



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
            catch
            {
                Assert.IsTrue(false);
            }
            


        }



        [DataTestMethod]
        [DataRow(1, 33)]
        public async Task UpdateProfileAgeAsync_UpdateValue_ValueUpdated(int userId, int age)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Age = age;

                await publicUserProfileService.UpdateProfileAgeAsync(model);

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
                    if (profile.Age == age)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, "Description")]
        public async Task UpdateProfileDescriptionAsync_UpdateValue_ValueUpdated(int userId, string description)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);


            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;


            try
            {

                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Description = description;

                await publicUserProfileService.UpdateProfileDescriptionAsync(model);

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
                    if (profile.Description == description)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }


        [DataTestMethod]
        [DataRow(1, "TestValue")]
        public async Task UpdateProfileEthnicityAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;

            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Ethnicity = testValue;

                await publicUserProfileService.UpdateProfileEthnicityAsync(model);

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
                    if (profile.Ethnicity == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, "Male")]
        public async Task UpdateProfileGenderAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Gender = testValue;

                await publicUserProfileService.UpdateProfileGenderAsync(model);

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
                    if (profile.Gender == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }


        [DataTestMethod]
        [DataRow(1, "TestValue")]
        public async Task UpdateProfileGoalsAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Goals = testValue;

                await publicUserProfileService.UpdateProfileGoalsAsync(model);

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
                    if (profile.Goals == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, "TestValue")]
        public async Task UpdateProfileHeightAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Height = testValue;

                await publicUserProfileService.UpdateProfileHeightAsync(model);

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
                    if (profile.Height == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, "TestValue")]
        public async Task UpdateProfileHobbiesAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Hobbies = testValue;

                await publicUserProfileService.UpdateProfileHobbiesAsync(model);

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
                    if (profile.Hobbies == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, "TestValue")]
        public async Task UpdateProfileIntrestsAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Intrests = testValue;

                await publicUserProfileService.UpdateProfileIntrestsAsync(model);

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
                    if (profile.Intrests == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, "TestValue")]
        public async Task UpdateProfileJobsAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Jobs = testValue;

                await publicUserProfileService.UpdateProfileJobsAsync(model);

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
                    if (profile.Jobs == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, "TestValue")]
        public async Task UpdateProfileSexualOrientationAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.SexualOrientation = testValue;

                await publicUserProfileService.UpdateProfileSexualOrientationAsync(model);

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
                    if (profile.SexualOrientation == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }


        [DataTestMethod]
        [DataRow(1, "Public")]
        public async Task UpdateProfileVisibilityAsync_UpdateValue_ValueUpdated(int userId, string testValue)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo, validationService);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Visibility = testValue;

                await publicUserProfileService.UpdateProfileVisibilityAsync(model);

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
                    if (profile.Visibility == testValue)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }
    }
}
