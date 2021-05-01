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

namespace BusinessLayerUnitTests.PublicUserProfile
{
    [TestClass]
    
    public class PublicUserProfileServiceUnitTests
    {
        
        [DataTestMethod]
        [DataRow(1)]
        public async Task createPublicUserProfileAsync_UserProfileCreated_SuccessfulCreation(int userId)
        {

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                PublicUserProfileModel profile = await publicUserProfileRepo.GetPublicProfilebyUserId(userId);

                Assert.IsTrue(true);
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
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                Assert.IsTrue(true);

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
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo);
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
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);


                await publicUserProfileService.SetUserOnlineAsync(userId);


                await publicUserProfileService.SetUserOfflineAsync(userId);

                Assert.IsTrue(true);

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
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo);
            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;





            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);


                await publicUserProfileService.SetUserOnlineAsync(userId);

                Assert.IsTrue(true);

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
            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Age = age;

                await publicUserProfileService.UpdateProfileAgeAsync(model);

                Assert.IsTrue(true);

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

            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;


            try
            {

                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Description = description;

                await publicUserProfileService.UpdateProfileDescriptionAsync(model);


                Assert.IsTrue(true);
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

            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;

            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Ethnicity = testValue;

                await publicUserProfileService.UpdateProfileEthnicityAsync(model);
    
                Assert.IsTrue(true);
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


            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Gender = testValue;

                await publicUserProfileService.UpdateProfileGenderAsync(model);


                Assert.IsTrue(true);
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


            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Goals = testValue;

                await publicUserProfileService.UpdateProfileGoalsAsync(model);

        

                Assert.IsTrue(true);
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


            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Height = testValue;

                await publicUserProfileService.UpdateProfileHeightAsync(model);

   

                Assert.IsTrue(true);
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


            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Hobbies = testValue;

                await publicUserProfileService.UpdateProfileHobbiesAsync(model);


                Assert.IsTrue(true);
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


            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Intrests = testValue;

                await publicUserProfileService.UpdateProfileIntrestsAsync(model);

 
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


            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Jobs = testValue;

                await publicUserProfileService.UpdateProfileJobsAsync(model);

       
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


            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.SexualOrientation = testValue;

                await publicUserProfileService.UpdateProfileSexualOrientationAsync(model);


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


            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();

            IPublicUserProfileService publicUserProfileService = new PublicUserProfileService(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();
            model.UserId = userId;
            try
            {
                await publicUserProfileService.CeatePublicUserProfileAsync(model);
                model.Visibility = testValue;

                await publicUserProfileService.UpdateProfileVisibilityAsync(model);

     
            }
            catch
            {
                Assert.IsTrue(false);
            }
            
        }
    }
}
