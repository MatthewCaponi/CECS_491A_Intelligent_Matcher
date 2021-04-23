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
namespace BusinessLayerUnitTests.PublicUserProfile
{
    [TestClass]
    public class PublicUserProfileUnitTests
    {

        [DataTestMethod]
        [DataRow(1)]
        public async Task createPublicUserProfileAsync_UserProfileCreated_SuccessfulCreation(int userId)
        {
            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo.Object);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;
            try
            {
                await publicUserProfileManager.createPublicUserProfileAsync(model);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }




        }

        [DataTestMethod]
        [DataRow(1, "Description", "Job", "Goal", 21, "Gender", "Ethnicity", "SexualOrientation", "Height", "Visible", "Online", "Photo", "Intrests", "Hobbies")]
        public async Task editPublicUserProfileAsync_EditProfile_ProfileSuccessfullyEdited(int userId, string description, string job, string goals, int age, string gender, string ethnicity, string sexualOrientation, string height, string visibility, string status, string photo, string intrests, string hobbies)
        {

            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo.Object);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            await publicUserProfileManager.createPublicUserProfileAsync(model);

            model.Description = description;
            model.Jobs = job;
            model.Goals = goals;
            model.Age = age;
            model.Ethnicity = ethnicity;
            model.SexualOrientation = sexualOrientation;
            model.Height = height;
            model.Visibility = visibility;
            model.Status = status;
            model.Photo = photo;
            model.Intrests = intrests;
            model.Hobbies = hobbies;
            try
            {
                await publicUserProfileManager.editPublicUserProfileAsync(model);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }


        }

        [DataTestMethod]
        [DataRow(1, "Photo")]
        public async Task editUserProfilePicture_EditPhoto_PhotoSuccessfullyEdited(int userId, string photo)
        {

            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo.Object);

            PublicUserProfileModel model = new PublicUserProfileModel();


            try
            {
                model.UserId = userId;

                await publicUserProfileManager.createPublicUserProfileAsync(model);


                model.Photo = photo;


                await publicUserProfileManager.editUserProfilePicture(model);
                Assert.IsTrue(true);

            }
            catch
            {
                Assert.IsTrue(false);
            }






        }


        [DataTestMethod]
        [DataRow(1)]
        public async Task setUserOffline_SetOffline_UserSetOffline(int userId)
        {
            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo.Object);
            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;


            try
            {
                await publicUserProfileManager.createPublicUserProfileAsync(model);
                await publicUserProfileManager.setUserOnline(userId);
                await publicUserProfileManager.setUserOffline(userId);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }




        }


        [DataTestMethod]
        [DataRow(1)]
        public async Task setUserOnline_SetOnline_UserSetOnline(int userId)
        {

            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo.Object);

            PublicUserProfileModel model = new PublicUserProfileModel();

            model.UserId = userId;

            try
            {
                await publicUserProfileManager.createPublicUserProfileAsync(model);
                await publicUserProfileManager.setUserOnline(userId);
                Assert.IsTrue(true);

            }
            catch
            {
                Assert.IsTrue(false);
            }





        }

        [DataTestMethod]
        [DataRow(1, "Description", "Job", "Goal", 21, "Gender", "Ethnicity", "SexualOrientation", "Height", "Visible", "Online", "Photo", "Intrests", "Hobbies")]
        public async Task GetUserProfile_GetsProfile_ProfileGetSuccess(int userId, string description, string job, string goals, int age, string gender, string ethnicity, string sexualOrientation, string height, string visibility, string status, string photo, string intrests, string hobbies)
        {
            Mock<IPublicUserProfileRepo> publicUserProfileRepo = new Mock<IPublicUserProfileRepo>();
            IPublicUserProfileManager publicUserProfileManager = new PublicUserProfileManager(publicUserProfileRepo.Object);

            PublicUserProfileModel model = new PublicUserProfileModel();


            try
            {
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
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }
    }
}

