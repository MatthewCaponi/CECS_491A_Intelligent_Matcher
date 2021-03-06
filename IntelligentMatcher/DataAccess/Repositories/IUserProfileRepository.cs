﻿using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UserProfileModel>> GetAllUserProfiles();
        Task<UserProfileModel> GetUserProfileById(int id);
        Task<UserProfileModel> GetUserProfileByAccountId(int accountId);
        Task<int> CreateUserProfile(UserProfileModel model);
        Task<int> DeleteUserProfileByAccountId(int id);         
    }
}