using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PublicUserProfile
{
    public interface IPublicUserProfileManager
    {
        Task<bool> createPublicUserProfileAsync(PublicUserProfileModel model);

        Task<bool> editPublicUserProfileAsync(PublicUserProfileModel model);

        Task<PublicUserProfileModel> GetUserProfile(int userId);
         
        Task<bool> editUserProfilePicture(PublicUserProfileModel model);

        Task<bool> setUserOffline(int UserId);

        Task<bool> setUserOnline(int UserId);
     }
}
