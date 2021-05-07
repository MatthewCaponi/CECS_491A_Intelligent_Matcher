using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PublicUserProfile
{
    public interface IPublicUserProfileManager
    {
        Task<bool> CeatePublicUserProfileAsync(PublicUserProfileModel model);

        Task<bool> EditPublicUserProfileAsync(PublicUserProfileModel model);

        Task<PublicUserProfileModel> GetUserProfileAsync(int userId);
         
        Task<bool> EditUserProfilePictureAsync(PublicUserProfileModel model);

        Task<bool> SetUserOfflineAsync(int userId);

        Task<bool> SetUserOnlineAsync(int userId);
     }
}
