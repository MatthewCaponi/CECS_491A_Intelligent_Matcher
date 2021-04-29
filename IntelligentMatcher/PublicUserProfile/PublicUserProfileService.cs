using DataAccess.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PublicUserProfile
{
    public class PublicUserProfileService : IPublicUserProfileService
    {
        IPublicUserProfileRepo _publicUserProfileRepo;
        public PublicUserProfileService(IPublicUserProfileRepo publicUserProfileRepo)
        {
            _publicUserProfileRepo = publicUserProfileRepo;
        }


        public async Task<bool> CeatePublicUserProfileAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.CreatePublicProfile(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PublicUserProfileModel> GetUserProfileAsync(int userId)
        {
          
           return await _publicUserProfileRepo.GetPublicProfilebyUserId(userId);
    
        }

        public async Task<bool> SetUserOnlineAsync(int userId)
        {
            try
            {
                await _publicUserProfileRepo.UpdateStatus(userId, "Online");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SetUserOfflineAsync(int userId)
        {
            try
            {
                await _publicUserProfileRepo.UpdateStatus(userId, "Offline");
                return true;
            }
            catch
            {
                return false;

            }
        }


        public async Task<bool> ChangeProfilePictureAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdatePhoto(model.UserId, model.Photo);
                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<bool> UpdateProfileAgeAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateAge(model.UserId, model.Age);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> UpdateProfileDescriptionAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateDescription(model.UserId, model.Description);
                   return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileEthnicityAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateEthnicity(model.UserId, model.Ethnicity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateProfileGenderAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateGender(model.UserId, model.Gender);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateProfileGoalsAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateGoals(model.UserId, model.Goals);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileHobbiesAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateHobbies(model.UserId, model.Hobbies);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileIntrestsAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateIntrests(model.UserId, model.Intrests);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileHeightAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateHeight(model.UserId, model.Intrests);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileJobsAsync(PublicUserProfileModel model)
        {
            try
            {

                await _publicUserProfileRepo.UpdateJobs(model.UserId, model.Jobs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileSexualOrientationAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateSexualOrientation(model.UserId, model.SexualOrientation);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileVisibilityAsync(PublicUserProfileModel model)
        {
            try
            {
                await _publicUserProfileRepo.UpdateVisibility(model.UserId, model.Visibility);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
