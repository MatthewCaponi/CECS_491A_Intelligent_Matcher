using System;
using System.Collections.Generic;
using System.Text;
using Models;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;
using Services;

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
            return await _publicUserProfileService.ChangeProfilePictureAsync(model);

        }

        public async Task<bool> SetUserOnlineAsync(int userId)
        {
            return await _publicUserProfileService.SetUserOnlineAsync(userId);
        }

        public async Task<bool> SetUserOfflineAsync(int userId)
        {
            return await _publicUserProfileService.SetUserOfflineAsync(userId);
        }

        public async Task<bool> EditPublicUserProfileAsync(PublicUserProfileModel model)
        {
            bool finalResult = true;
            if(model.Age < 1000)
            {
                bool result = await _publicUserProfileService.UpdateProfileAgeAsync(model);
                if(result == false)
                {
                    finalResult = false;
                }
            }
            if(model.Description.Length <= 1000)
            {
                bool result = await _publicUserProfileService.UpdateProfileDescriptionAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.Ethnicity.Length <= 100)
            {
                bool result = await _publicUserProfileService.UpdateProfileEthnicityAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.Gender == "Male" || model.Gender == "Female")
            {
                bool result = await _publicUserProfileService.UpdateProfileGenderAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.Goals.Length <= 1000)
            {
                bool result = await _publicUserProfileService.UpdateProfileGoalsAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.Height.Length <= 1000)
            {
                bool result = await _publicUserProfileService.UpdateProfileHeightAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.Hobbies.Length <= 1000)
            {
                bool result = await _publicUserProfileService.UpdateProfileHobbiesAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.Intrests.Length <= 1000)
            {
                bool result = await _publicUserProfileService.UpdateProfileIntrestsAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.Jobs.Length <= 1000)
            {
                bool result = await _publicUserProfileService.UpdateProfileJobsAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.SexualOrientation.Length <= 100)
            {
                bool result = await _publicUserProfileService.UpdateProfileSexualOrientationAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }
            if (model.Visibility == "Public" || model.Visibility == "Private" || model.Visibility == "Friends")
            {
                bool result = await _publicUserProfileService.UpdateProfileVisibilityAsync(model);
                if (result == false)
                {
                    finalResult = false;
                }
            }

            return finalResult;
        }

        public async Task<bool> CeatePublicUserProfileAsync(PublicUserProfileModel model)
        {
            model.Status = "Offline";
            model.Visibility = "Public";

            return await _publicUserProfileService.CeatePublicUserProfileAsync(model);


        }


        public async Task<PublicUserProfileModel> GetUserProfileAsync(int userId)
        {
            return await _publicUserProfileService.GetUserProfileAsync(userId);
        }

    }
}
