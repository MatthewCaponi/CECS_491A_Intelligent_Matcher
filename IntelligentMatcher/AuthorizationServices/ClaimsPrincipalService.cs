using BusinessModels;
using BusinessModels.UserAccessControl;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServices
{
    public class ClaimsPrincipalService
    {
        private readonly IUserScopeClaimRepository _userScopeClaimRepository;

        public ClaimsPrincipalService(IUserScopeClaimRepository userScopeClaimRepository)
        {
            _userScopeClaimRepository = userScopeClaimRepository;
        }
        public async Task<Result<ClaimsPrincipal>> GetUserClaimsPrincipal(int id, string accountType)
        {
            await _userScopeClaimRepository.GetUserScopeClaimById(id);
        }

        public async Task<Result<List<ClaimsPrincipal>>> GetAllUserClaimsPrincipals(int id)
        {

        }

        public async Task<Result<int>> CreateClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {

        }

        public async Task<Result<bool>> AddClaim(UserClaimModel userClaimModel, string scope)
        {

        }

        public async Task<Result<bool>> AddScope(string scope)
        {

        }

        public async Task<Result<bool>> UpdateClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {

        }

        public async Task<Result<bool>> DeleteClaimsPrincipal(int claimsPrincipalId)
        {

        }


    }
}
