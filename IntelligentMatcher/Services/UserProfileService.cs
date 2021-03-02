using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.App_Specific_Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services
{
    public class UserProfileService
    {
        private IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<List<Models.WebUserProfileModel>> GetAllUsers()
        {
            var userProfiles = await _userProfileRepository.GetAllUserProfiles();

            var webUserProfiles = userProfiles.Select(x => new WebUserProfileModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Surname = x.Surname,
                DateOfBirth = x.DateOfBirth,
                UserAccountId = x.UserAccountId
            }).ToList();

            return webUserProfiles;
        }     

        public async Task<WebUserProfileModel> GetUser(int id)
        {
            var userProfileModel = await _userProfileRepository.GetUserProfileByAccountId(id);

            var webUserProfileModel = new WebUserProfileModel();
            var propOne = userProfileModel.GetType().GetProperties();
            foreach (var item in propOne)
            {
                webUserProfileModel.GetType().GetProperty(item.Name).SetValue(item.GetValue(userProfileModel, null), null);
            }

            return webUserProfileModel;          
        }

        public async Task<int> CreateUser(WebUserProfileModel webUserProfileModel)
        {
            var propOne = webUserProfileModel.GetType().GetProperties();
            var userProfileModel = new UserProfileModel();
            foreach (var item in propOne)
            {
                userProfileModel.GetType().GetProperty(item.Name).SetValue(item.GetValue(webUserProfileModel, null), null);
            }

            return await _userProfileRepository.CreateUserProfile(userProfileModel);
        }

        public async Task<bool> DeleteProfile(int accountId)
        {
            await _userProfileRepository.DeleteUserProfile(accountId);
            return true;
        }

    }
}
