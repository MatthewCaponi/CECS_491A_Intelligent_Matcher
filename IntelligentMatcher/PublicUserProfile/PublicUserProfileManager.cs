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

        public async Task<bool> editPublicUserProfileAsync(PublicUserProfileModel model)
        {
            await _publicUserProfileRepo.UpdateAge(model.UserId, model.Age);
            await _publicUserProfileRepo.UpdateDescription(model.UserId, model.Description);
            await _publicUserProfileRepo.UpdateEthnicity(model.UserId, model.Ethnicity);
            await _publicUserProfileRepo.UpdateGender(model.UserId, model.Gender);
            await _publicUserProfileRepo.UpdateGoals(model.UserId, model.Goals);
            await _publicUserProfileRepo.UpdateHeight(model.UserId, model.Height);
            await _publicUserProfileRepo.UpdateHobbies(model.UserId, model.Hobbies);
            await _publicUserProfileRepo.UpdateIntrests(model.UserId, model.Intrests);
            await _publicUserProfileRepo.UpdateJobs(model.UserId, model.Jobs);
            await _publicUserProfileRepo.UpdatePhoto(model.UserId, model.Photo);
            await _publicUserProfileRepo.UpdateSexualOrientation(model.UserId, model.SexualOrientation);
            await _publicUserProfileRepo.UpdateVisibility(model.UserId, model.Visibility);
            await _publicUserProfileRepo.UpdateStatus(model.UserId, model.Status);

            return true;
        }

        public async Task<bool> createPublicUserProfileAsync(PublicUserProfileModel model)
        {
            await _publicUserProfileRepo.CreatePublicProfile(model);
            return true;


        }


        public async Task<PublicUserProfileModel> GetUserProfile(int userId)
        {
            return await _publicUserProfileRepo.GetPublicProfilebyUserId(userId);
        }

    }
}
