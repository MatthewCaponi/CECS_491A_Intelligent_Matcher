﻿using DataAccess.Repositories;
using Models;
using Services;
using System.Collections.Generic;
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

        public async Task<List<WebUserProfileModel>> GetAllUsers()
        {
            var userProfiles = await _userProfileRepository.GetAllUserProfiles();
            List<WebUserProfileModel> webUserProfiles = new List<WebUserProfileModel>();
            foreach (var userProfileModel in userProfiles)
            {
                var webUserProfileModel = ModelConverterService.ConvertTo(userProfileModel, new WebUserProfileModel());
                webUserProfiles.Add(webUserProfileModel);
            }

            return webUserProfiles;
        }     

        public async Task<WebUserProfileModel> GetUser(int id)
        {
            var userProfileModel = await _userProfileRepository.GetUserProfileById(id);
            var webUserProfileModel = ModelConverterService.ConvertTo(userProfileModel, new WebUserProfileModel());

            return webUserProfileModel;
        }

        public async Task<int> CreateUser(WebUserProfileModel webUserProfileModel)
        {
            var userProfileModel = ModelConverterService.ConvertTo(webUserProfileModel, new UserProfileModel());
            return await _userProfileRepository.CreateUserProfile(userProfileModel);
        }

        public async Task<bool> DeleteProfile(int accountId)
        {
            await _userProfileRepository.DeleteUserProfile(accountId);
            return true;
        }
    }
}