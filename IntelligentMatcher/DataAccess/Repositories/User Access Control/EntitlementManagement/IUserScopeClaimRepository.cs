﻿using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IUserScopeClaimRepository
    {
        Task<int> CreateUserScopeClaim(UserScopeClaimModel model);
        Task<int> DeleteUserScopeClaim(int id);
        Task<IEnumerable<UserScopeClaimModel>> GetAllUserUserScopeClaims();
        Task<UserScopeClaimModel> GetUserScopeClaimById(int id);
        Task<int> UpdateUserScopeClaim(UserScopeClaimModel model);
    }
}