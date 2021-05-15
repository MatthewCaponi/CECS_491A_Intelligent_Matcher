using BusinessModels.UserAccessControl;
using IdentityServices;
using IntelligentMatcher.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;

namespace UserAccessControlServices
{
    public class MapperService : IMapperService
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IUserAccountService _userAccountService;
        private readonly ITokenService _tokenService;
        private readonly IAssignmentPolicyService _assignmentPolicyService;

        public MapperService(IUserProfileService userProfileService, IUserAccountService userAccountService, ITokenService tokenService, IAssignmentPolicyService assignmentPolicyService)
        {
            _userProfileService = userProfileService;
            _userAccountService = userAccountService;
            _tokenService = tokenService;
            _assignmentPolicyService = assignmentPolicyService;
        }

        public async Task<string> MapUserAccessToken(ClaimsPrincipal claimsPrincipal)
        {
            var scopes = claimsPrincipal.Scopes;
            var claims = claimsPrincipal.Claims;
            var scopesClaim = new StringBuilder();

            var userAccountModel = await _userAccountService.GetUserAccount(claimsPrincipal.UserAccountId);

            foreach (var scope in scopes)
            {
                scopesClaim.Append(scope.Type);
            }

            claims.Add(new UserClaimModel()
            {
                Type = "scopes",
                Value = scopesClaim.ToString(),
                UseraAccountId = claimsPrincipal.UserAccountId

            });

            claims.Add(new UserClaimModel("sub", userAccountModel.Username));
            claims.Add(new UserClaimModel("aud", userAccountModel.Username));
            claims.Add(new UserClaimModel("exp", "20"));
            claims.Add(new UserClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
            claims.Add(new UserClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));

            var accessToken = _tokenService.CreateToken(claims);
            return accessToken;
        }

        public async Task<string> MapUserIdToken(ClaimsPrincipal claimsPrincipal)
        {
            var claims = claimsPrincipal.Claims;
            var userAccountModel = await _userAccountService.GetUserAccount(claimsPrincipal.UserAccountId);
            var profile = await _userProfileService.GetUserProfileByAccountId(claimsPrincipal.UserAccountId);
           
            claims.Add(new UserClaimModel("id", claimsPrincipal.UserAccountId.ToString()));
            claims.Add(new UserClaimModel("Scope", "id"));
            claims.Add(new UserClaimModel("sub", userAccountModel.Username));
            claims.Add(new UserClaimModel("aud", userAccountModel.Username));
            claims.Add(new UserClaimModel("exp", "20"));
            claims.Add(new UserClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
            claims.Add(new UserClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
            claims.Add(new UserClaimModel("firstName", profile.FirstName));
            claims.Add(new UserClaimModel("lastName", profile.Surname));
            claims.Add(new UserClaimModel("birthdate", profile.DateOfBirth.ToString()));
            claims.Add(new UserClaimModel("username", userAccountModel.Username));
            claims.Add(new UserClaimModel("accountType", userAccountModel.AccountType));
            claims.Add(new UserClaimModel("accountStatus", userAccountModel.AccountStatus));

            var idToken = _tokenService.CreateToken(claims);

            return idToken;
        }
    }
}