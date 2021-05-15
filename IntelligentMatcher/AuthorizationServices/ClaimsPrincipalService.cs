using BusinessModels;
using BusinessModels.UserAccessControl;
using Cross_Cutting_Concerns;
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
using ScopeModel = Models.User_Access_Control.ScopeModel;
using UserClaimModel = BusinessModels.UserAccessControl.UserClaimModel;
using UserScopeModel = BusinessModels.UserAccessControl.UserScopeModel;
using DALUserScopeModel = Models.User_Access_Control.UserScopeModel;
using DALUserClaimModel = Models.User_Access_Control.UserClaimModel;

namespace AuthorizationServices
{
    public class ClaimsPrincipalService : IClaimsPrincipalService
    {
        private readonly IUserScopeClaimRepository _userScopeClaimRepository;
        private readonly IScopeClaimRepository _scopeClaimRepository;
        private readonly IClaimRepository _claimRepository;
        private readonly IScopeRepository _scopeRepository;
        private readonly IClaimsService _claimService;
        private readonly IScopeService _scopeService;
        private readonly IUserScopeRepository _userScopeRepository;
        private readonly IUserClaimRepository _userClaimRepository;

        public ClaimsPrincipalService(IUserScopeClaimRepository userScopeClaimRepository, IScopeClaimRepository scopeClaimRepository, IClaimRepository claimRepository,
            IScopeRepository scopeRepository, IClaimsService claimService, IScopeService scopeService, IUserScopeRepository userScopeRepository, IUserClaimRepository userClaimRepository)
        {
            _userScopeClaimRepository = userScopeClaimRepository;
            _scopeClaimRepository = scopeClaimRepository;
            _claimRepository = claimRepository;
            _scopeRepository = scopeRepository;
            _claimService = claimService;
            _scopeService = scopeService;
            _userScopeRepository = userScopeRepository;
            _userClaimRepository = userClaimRepository;
        }
        public async Task<Result<ClaimsPrincipal>> GetUserClaimsPrincipal(int id, string role)
        {
            var userScopeClaims = (await _userScopeClaimRepository.GetAllUserScopeClaimsByAccountIdAndRole(id, role)).ToList();

            List<UserScopeModel> blScopes = new List<UserScopeModel>();
            List<UserClaimModel> blClaims = new List<UserClaimModel>();

            foreach (var userScopeClaim in userScopeClaims)
            {
                var dalScope = await _userScopeRepository.GetUserScopeByScopeId(userScopeClaim.UserScopeId);
                var dalClaim = await _userClaimRepository.GetUserClaimByUserClaimId(userScopeClaim.UserClaimId);
                var scope = new UserScopeModel()
                {
                    Id = dalScope.Id,
                    Type = dalScope.Type,
                    UserAccountId = id
                };

                var claim = new UserClaimModel()
                {
                    Type = dalClaim.Type,
                    UseraAccountId = id
                };

                blScopes.Add(scope);
                blClaims.Add(claim);
            }

            var claimsPrincipal = new ClaimsPrincipal()
            {
                UserAccountId = id,
                Role = role,
                Scopes = blScopes,
                Claims = blClaims
            };

            return Result<ClaimsPrincipal>.Success(claimsPrincipal);
        }

        public async Task<Result<bool>> CreateClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            var scopes = claimsPrincipal.Scopes;
            var claims = claimsPrincipal.Claims;

            scopes.ForEach(async x => await _userScopeRepository.CreateScope(new DALUserScopeModel
            {
                Type = x.Type,
                UserAccountId = claimsPrincipal.UserAccountId
            }));

            claims.ForEach(async x => await _userClaimRepository.CreateUserClaim(new DALUserClaimModel()
            {
                Type = x.Type,
                Value = x.Value,
                UserAccountId = claimsPrincipal.UserAccountId
            }));

            var allDalScopes = await _scopeRepository.GetAllScopes();
            var allDalClaims = await _claimRepository.GetAllClaims();

            var dalScopes = new List<ScopeModel>();

            foreach (var dalScope in allDalScopes)
            {
                foreach(var scope in scopes)
                {
                    if (dalScope.Type.ToUpper() == scope.Type.ToUpper())
                    {
                        dalScopes.Add(dalScope);
                    }
                }
            }

            var dalClaims = new List<ClaimModel>();

            foreach (var dalClaim in allDalClaims)
            {
                foreach (var claim in claims)
                {
                    if (dalClaim.Type.ToUpper() == claim.Type.ToUpper())
                    {
                        dalClaims.Add(dalClaim);
                    }
                }
            }

            var dalUserScopes = await _userScopeRepository.GetAllUserScopesByUserAccountId(claimsPrincipal.UserAccountId);
            var dalUserClaims = await _userClaimRepository.GetAllUserClaimsByUserAccountId(claimsPrincipal.UserAccountId);

            var allBlScopes = await _scopeService.GetAllScopes();
            var allBlClaims = await _claimService.GetAllClaims();

            var blScopes = new List<BusinessModels.UserAccessControl.ScopeModel>();
            foreach (var blScope in allBlScopes)
            {
                foreach (var dalScope in dalUserScopes)
                {
                    if (blScope.Type.ToUpper() == dalScope.Type.ToUpper())
                    {
                        blScopes.Add(blScope);
                    }
                }
            }
            var blClaims = new List<BusinessModels.UserAccessControl.ClaimModel>();
            foreach (var blClaim in allBlClaims)
            {
                foreach (var dalClaim in dalClaims)
                {
                    if (blClaim.Type.ToUpper() == dalClaim.Type.ToUpper())
                    {
                        blClaims.Add(blClaim);
                    }
                }
            }

            foreach (var dalUserScope in dalUserScopes)
            {
                var requiredClaims = blScopes.Where(x => x.Type == dalUserScope.Type).FirstOrDefault().Claims;
                var requiredDALClaims = dalUserClaims.Where(x => x.Type == (requiredClaims.Where(y => y.Type == x.Type).FirstOrDefault().Type)).ToList();

                foreach (var dalUserClaim in dalUserClaims)
                {
                    foreach (var requiredClaim in requiredClaims)
                    {
                        if (requiredClaim.Type.ToUpper() == dalUserClaim.Type.ToUpper())
                        {
                            requiredDALClaims.Add(dalUserClaim);
                        }
                    }
                }

                requiredDALClaims.ForEach(async x => await _userScopeClaimRepository.CreateUserScopeClaim(new UserScopeClaimModel
                {
                    UserAccountId = claimsPrincipal.UserAccountId,
                    UserScopeId = dalUserScope.Id,
                    UserClaimId = x.Id,
                    Role = claimsPrincipal.Role
                }));
            }

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> AddUserClaim(UserClaimModel userClaimModel, string scope, string description)
        {
            // check to see if the desired scope exists
            var dalUserClaims = await _userClaimRepository.GetAllUserClaimsByUserAccountId(userClaimModel.UseraAccountId);
            var claimMatch = (dalUserClaims.Where(x => x.Type == userClaimModel.Type).FirstOrDefault());

            if (claimMatch != null)
            {
                return Result<bool>.Failure(ErrorMessage.UserAlreadyContainsClaim);
            }

            var dalScopes = (await _scopeRepository.GetAllScopes()).ToList();
            var scopeMatch = dalScopes.Where(x => x.Type == scope).FirstOrDefault();
            int scopeId = -1;
            if (scopeMatch == null)
            {
                scopeId = await _scopeRepository.CreateScope(new ScopeModel()
                {
                    Type = scope,
                    Description = description,
                    IsDefault = false
                });
            }
            else
            {
                scopeId = scopeMatch.Id;
            }

            var claimId = await _userClaimRepository.CreateUserClaim(ModelConverterService.ConvertTo(userClaimModel, new DALUserClaimModel()));

            await _scopeClaimRepository.CreateScopeClaim(new ScopeClaimModel()
            {
                ScopeId = scopeId,
                ClaimId = claimId
            });

            return Result<bool>.Success(true);

        }

        public async Task<Result<bool>> AddScope(string scope, int accountId)
        {
            var dalUserScopes = await _userScopeRepository.GetAllUserScopesByUserAccountId(accountId);
            var userScopeMatch = dalUserScopes.Where(x => x.Type == scope).FirstOrDefault();

            if (userScopeMatch != null)
            {
                return Result<bool>.Failure(ErrorMessage.UserAlreadyContainsScope);
            }

            await _userScopeRepository.CreateScope(new DALUserScopeModel()
            {
                Type = scope,
                UserAccountId = accountId
            });

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            await DeleteClaimsPrincipal(claimsPrincipal);
            var newAccountId = await CreateClaimsPrincipal(claimsPrincipal);

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            var userScopeClaims = (await _userScopeClaimRepository.GetAllUserScopeClaimsByAccountIdAndRole(claimsPrincipal.UserAccountId, claimsPrincipal.Role)).ToList();

            userScopeClaims.ForEach(async x => await _userScopeClaimRepository.DeleteUserScopeClaim(x.Id));

            return Result<bool>.Success(true);
        }


    }
}
