﻿using Logging;
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
    public class PublicUserProfileIntegrationTests
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
            model.Gender = gender;
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
        [DataRow(1, "Description", "Job", "Goal", 21, "Male", "Ethnicity", "SexualOrientation", "Height", "Public", "Online", "Photo", "Intrests", "Hobbies")]
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

