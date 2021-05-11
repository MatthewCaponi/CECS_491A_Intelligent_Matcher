using DataAccess.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PublicUserProfileService : IPublicUserProfileService
    {
        IPublicUserProfileRepo _publicUserProfileRepo;
        IValidationService _validationService;
        public PublicUserProfileService(IPublicUserProfileRepo publicUserProfileRepo, IValidationService validationService)
        {
            _publicUserProfileRepo = publicUserProfileRepo;
            _validationService = validationService;
        }


        public async Task<bool> CeatePublicUserProfileAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
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
            return false;
        }

        public async Task<PublicUserProfileModel> GetUserProfileAsync(int userId)
        {
            if (_validationService.IsNull(userId))
            {

                try
                {
                    return await _publicUserProfileRepo.GetPublicProfilebyUserId(userId);
                }
                catch
                {
                    return null;
                }
            }
            return null;
    
        }

        public async Task<bool> SetUserOnlineAsync(int userId)
        {
            if (_validationService.IsNull(userId))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateStatus(userId, "Online");
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> SetUserOfflineAsync(int userId)
        {
            if (_validationService.IsNull(userId))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateStatus(userId, "Offline");
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;

                }
            }
            return false;

        }


        public async Task<bool> ChangeProfilePictureAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdatePhoto(model.UserId, model.Photo);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }



        public async Task<bool> UpdateProfileAgeAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateAge(model.UserId, model.Age);
                    if(result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;


        }
        public async Task<bool> UpdateProfileDescriptionAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateDescription(model.UserId, model.Description);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
 
        }

        public async Task<bool> UpdateProfileEthnicityAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateEthnicity(model.UserId, model.Ethnicity);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;


        }
        public async Task<bool> UpdateProfileGenderAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateGender(model.UserId, model.Gender);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;


        }
        public async Task<bool> UpdateProfileGoalsAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateGoals(model.UserId, model.Goals);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> UpdateProfileHobbiesAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateHobbies(model.UserId, model.Hobbies);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;


        }

        public async Task<bool> UpdateProfileIntrestsAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateIntrests(model.UserId, model.Intrests);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> UpdateProfileHeightAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateHeight(model.UserId, model.Height);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> UpdateProfileJobsAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {

                    int result = await _publicUserProfileRepo.UpdateJobs(model.UserId, model.Jobs);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> UpdateProfileSexualOrientationAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateSexualOrientation(model.UserId, model.SexualOrientation);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> UpdateProfileVisibilityAsync(PublicUserProfileModel model)
        {
            if (_validationService.IsNull(model))
            {

                try
                {
                    int result = await _publicUserProfileRepo.UpdateVisibility(model.UserId, model.Visibility);
                    if (result != 1)
                    {
                        return false;

                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;


        }

    }
}
