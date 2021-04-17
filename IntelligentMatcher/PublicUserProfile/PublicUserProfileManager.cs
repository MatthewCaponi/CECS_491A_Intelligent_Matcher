using System;
using System.Collections.Generic;
using System.Text;
using Models;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;

namespace PublicUserProfile
{
    public class PublicUserProfileManager : IPublicUserProfileManager
    {
        IPublicUserProfileRepo _publicUserProfileRepo;
        public PublicUserProfileManager(IPublicUserProfileRepo publicUserProfileRepo)
        {

            _publicUserProfileRepo = publicUserProfileRepo;
 
        }
        public async Task<bool> editUserProfilePicture(PublicUserProfileModel model)
        {
            await _publicUserProfileRepo.UpdatePhoto(model.UserId, model.Photo);
            return true;

        }

        public async Task<bool> setUserOnline(int UserId)
        {
            await _publicUserProfileRepo.UpdateStatus(UserId, "Online");
            return true;
        }

        public async Task<bool> setUserOffline(int UserId)
        {
            await _publicUserProfileRepo.UpdateStatus(UserId, "Offline");
            return true;
        }

        public async Task<bool> editPublicUserProfileAsync(PublicUserProfileModel model)
        {
            if(model.Age < 1000)
            {
                await _publicUserProfileRepo.UpdateAge(model.UserId, model.Age);
            }
            if(model.Description.Length <= 1000)
            {
                await _publicUserProfileRepo.UpdateDescription(model.UserId, model.Description);
            }
            if(model.Ethnicity.Length <= 100)
            {
                await _publicUserProfileRepo.UpdateEthnicity(model.UserId, model.Ethnicity);
            }
            if(model.Gender == "Male" || model.Gender == "Female")
            {
                await _publicUserProfileRepo.UpdateGender(model.UserId, model.Gender);
            }
            if(model.Goals.Length <= 1000)
            {
                await _publicUserProfileRepo.UpdateGoals(model.UserId, model.Goals);
            }
            if(model.Height.Length <= 1000)
            {
                await _publicUserProfileRepo.UpdateHeight(model.UserId, model.Height);
            }
            if(model.Hobbies.Length <= 1000)
            {
                await _publicUserProfileRepo.UpdateHobbies(model.UserId, model.Hobbies);
            }
            if(model.Intrests.Length <= 1000)
            {
                await _publicUserProfileRepo.UpdateIntrests(model.UserId, model.Intrests);
            }
            if(model.Jobs.Length <= 1000)
            {
                await _publicUserProfileRepo.UpdateJobs(model.UserId, model.Jobs);
            }
            if(model.SexualOrientation.Length <= 100)
            {
                await _publicUserProfileRepo.UpdateSexualOrientation(model.UserId, model.SexualOrientation);
            }
            if(model.Visibility == "Public" || model.Visibility == "Private" || model.Visibility == "Friends")
            {
                await _publicUserProfileRepo.UpdateVisibility(model.UserId, model.Visibility);
            }

            return true;
        }

        public async Task<bool> createPublicUserProfileAsync(PublicUserProfileModel model)
        {
            model.Status = "Offline";
            model.Visibility = "Public";

            await _publicUserProfileRepo.CreatePublicProfile(model);
            return true;


        }


        public async Task<PublicUserProfileModel> GetUserProfile(int userId)
        {
            return await _publicUserProfileRepo.GetPublicProfilebyUserId(userId);
        }

    }
}
