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
        IPublicUserProfileService _publicUserProfileService;
        public PublicUserProfileManager(IPublicUserProfileService publicUserProfileService)
        {

            _publicUserProfileService = publicUserProfileService;
 
        }
        public async Task<bool> EditUserProfilePictureAsync(PublicUserProfileModel model)
        {
            await _publicUserProfileService.ChangeProfilePictureAsync(model);
            return true;

        }

        public async Task<bool> SetUserOnlineAsync(int userId)
        {
            await _publicUserProfileService.SetUserOnlineAsync(userId);
            return true;
        }

        public async Task<bool> SetUserOfflineAsync(int userId)
        {
            await _publicUserProfileService.SetUserOfflineAsync(userId);
            return true;
        }

        public async Task<bool> EditPublicUserProfileAsync(PublicUserProfileModel model)
        {
            if(model.Age < 1000)
            {
                await _publicUserProfileService.UpdateProfileAgeAsync(model);
            }
            if(model.Description.Length <= 1000)
            {
                await _publicUserProfileService.UpdateProfileDescriptionAsync(model);
            }
            if (model.Ethnicity.Length <= 100)
            {
                await _publicUserProfileService.UpdateProfileEthnicityAsync(model);
            }
            if (model.Gender == "Male" || model.Gender == "Female")
            {
                await _publicUserProfileService.UpdateProfileGenderAsync(model);
            }
            if (model.Goals.Length <= 1000)
            {
                await _publicUserProfileService.UpdateProfileGoalsAsync(model);
            }
            if (model.Height.Length <= 1000)
            {
                await _publicUserProfileService.UpdateProfileHeightAsync(model);
            }
            if (model.Hobbies.Length <= 1000)
            {
                await _publicUserProfileService.UpdateProfileHobbiesAsync(model);
            }
            if (model.Intrests.Length <= 1000)
            {
                await _publicUserProfileService.UpdateProfileIntrestsAsync(model);
            }
            if (model.Jobs.Length <= 1000)
            {
                await _publicUserProfileService.UpdateProfileJobsAsync(model);
            }
            if (model.SexualOrientation.Length <= 100)
            {
                await _publicUserProfileService.UpdateProfileSexualOrientationAsync(model);
            }
            if (model.Visibility == "Public" || model.Visibility == "Private" || model.Visibility == "Friends")
            {
                await _publicUserProfileService.UpdateProfileVisibilityAsync(model);
            }

            return true;
        }

        public async Task<bool> CeatePublicUserProfileAsync(PublicUserProfileModel model)
        {
            model.Status = "Offline";
            model.Visibility = "Public";

            await _publicUserProfileService.CeatePublicUserProfileAsync(model);
            return true;


        }


        public async Task<PublicUserProfileModel> GetUserProfileAsync(int userId)
        {
            return await _publicUserProfileService.GetUserProfileAsync(userId);
        }

    }
}
