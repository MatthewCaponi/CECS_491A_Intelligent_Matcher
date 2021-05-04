using DataAccess.Repositories;
using Exceptions;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<List<WebUserProfileModel>> GetAllUsers()
        {
            try
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
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }

        public async Task<WebUserProfileModel> GetUserProfile(int id)
        {
            try
            {
                var userProfileModel = await _userProfileRepository.GetUserProfileById(id);
                var webUserProfileModel = ModelConverterService.ConvertTo(userProfileModel, new WebUserProfileModel());

                return webUserProfileModel;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }

        public async Task<WebUserProfileModel> GetUserProfileByAccountId(int accountId)
        {
            try
            {
                var userProfileModel = await _userProfileRepository.GetUserProfileByAccountId(accountId);
                if (userProfileModel == null)
                {
                    return null;
                }
                else
                {
                    var webUserProfileModel = ModelConverterService.ConvertTo(userProfileModel, new WebUserProfileModel());
                    return webUserProfileModel;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }

        public async Task<int> CreateUserProfile(WebUserProfileModel webUserProfileModel)
        {
            try
            {
                var userProfileModel = ModelConverterService.ConvertTo(webUserProfileModel, new UserProfileModel());
                return await _userProfileRepository.CreateUserProfile(userProfileModel);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> DeleteProfile(int accountId)
        {
            try
            {
                await _userProfileRepository.DeleteUserProfileByAccountId(accountId);
                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }
    }
}
