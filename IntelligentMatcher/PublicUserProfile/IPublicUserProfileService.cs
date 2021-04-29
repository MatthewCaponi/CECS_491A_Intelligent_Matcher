using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PublicUserProfile
{
    public interface IPublicUserProfileService
    {
        Task<bool> SetUserOnlineAsync(int userId);

        Task<bool> SetUserOfflineAsync(int userId);
        Task<bool> CeatePublicUserProfileAsync(PublicUserProfileModel model);
        Task<bool> ChangeProfilePictureAsync(PublicUserProfileModel model);

        Task<PublicUserProfileModel> GetUserProfileAsync(int userId);

        Task<bool> UpdateProfileAgeAsync(PublicUserProfileModel model);
        Task<bool> UpdateProfileDescriptionAsync(PublicUserProfileModel model);

        Task<bool> UpdateProfileEthnicityAsync(PublicUserProfileModel model);
        Task<bool> UpdateProfileGenderAsync(PublicUserProfileModel model);
        Task<bool> UpdateProfileGoalsAsync(PublicUserProfileModel model);

        Task<bool> UpdateProfileHobbiesAsync(PublicUserProfileModel model);

        Task<bool> UpdateProfileIntrestsAsync(PublicUserProfileModel model);

        Task<bool> UpdateProfileJobsAsync(PublicUserProfileModel model);

        Task<bool> UpdateProfileSexualOrientationAsync(PublicUserProfileModel model);

        Task<bool> UpdateProfileVisibilityAsync(PublicUserProfileModel model);

        Task<bool> UpdateProfileHeightAsync(PublicUserProfileModel model);

    }
}
