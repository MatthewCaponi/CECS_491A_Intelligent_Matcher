using BusinessModels;
using BusinessModels.UserAccessControl;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccessControlServices;
using ClaimModel = Models.User_Access_Control.ClaimModel;

namespace AuthorizationServices
{
    public class ClaimsPrincipalService
    {
        private readonly IUserScopeClaimRepository _userScopeClaimRepository;
        private readonly IScopeClaimRepository _scopeClaimRepository;
        private readonly IClaimRepository _claimRepository;
        private readonly IScopeRepository _scopeRepository;
        private readonly IClaimsService _claimService;
        private readonly IScopeService _scopeService;

        public ClaimsPrincipalService(IUserScopeClaimRepository userScopeClaimRepository, IScopeClaimRepository scopeClaimRepository, IClaimRepository claimRepository,
            IScopeRepository scopeRepository, IClaimsService claimService, IScopeService scopeService)
        {
            _userScopeClaimRepository = userScopeClaimRepository;
            _scopeClaimRepository = scopeClaimRepository;
            _claimRepository = claimRepository;
            _scopeRepository = scopeRepository;
            _claimService = claimService;
            _scopeService = scopeService;
        }
        public async Task<Result<ClaimsPrincipal>> GetUserClaimsPrincipal(int id, string accountType)
        {
            var userScopeClaims = await _userScopeClaimRepository.GetAllUserScopeClaimsByAccountIdAndRole(id, accountType);


        }

        public async Task<Result<List<ClaimsPrincipal>>> GetAllUserClaimsPrincipals(int id)
        {

        }

        public async Task<Result<int>> CreateClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            // get equivelant scopes from scopeService
            var scopes = claimsPrincipal.Scopes;
            var claims = claimsPrincipal.Claims;

            var allDalScopes = await _scopeRepository.GetAllScopes();
            var allDalClaims = await _claimRepository.GetAllClaims();

            var dalScopes = allDalScopes.Where(x => x.Name == (scopes.Where(y => y == x.Name).FirstOrDefault())).ToList();
            var dalClaims = allDalClaims.Where(x => x.Type == (claims.Where(y => y.Type == x.Type).FirstOrDefault().Type)).ToList();

            foreach (var scope in dalScopes)
            {
                var scopeClaims = (await _scopeClaimRepository.GetAllScopeClaimsByScopeId(scope.Id)).ToList();

                foreach (var scopeClaim in scopeClaims)
                {

                    await _claimRepository.UpdateClaim(new ClaimModel()
                    {
                        Id = scopeClaim.ClaimId,
                        Type = scopeClaim
                    })
                }

                //scopeClaims.ForEach(async x => await _userScopeClaimRepository.CreateUserScopeClaim(new UserScopeClaimModel()
                //{
                //    UserAccountId = claimsPrincipal.UserId,
                //    ScopeClaimId = x.Id,
                //    Role = claimsPrincipal.Role
                //}));


                scopeClaims.ForEach(x)
            }

            var allBlScopes = await _scopeService.GetAllScopes();
            var allBlClaims = await _claimService.GetAllClaims();

            var blScopes = allBlScopes.Where(x => x.Name == (scopes.Where(y => y == x.Name).FirstOrDefault())).ToList();
            var blClaims = allBlClaims.Where(x => x.Type == (claims.Where(y => y.Type == x.Type).FirstOrDefault().Type)).ToList();

            foreach (var blScope in blScopes)
            {
                await _userScopeClaimRepository.CreateUserScopeClaim(new UserScopeClaimModel()
                {
                    UserAccountId = claimsPrincipal.UserId,

                });
            }

            allBlScopes.Where(x => x.Name == )
            // get equivelant claims from claims service
            // get equivelant scopeclaims 
            // build a list of UserScopeClaims
            // insert
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
