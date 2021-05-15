using BusinessModels.UserAccessControl;
using Cross_Cutting_Concerns;
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
            var tokenClaims = new List<TokenClaimModel>();
            var scopes = claimsPrincipal.Scopes;
            foreach (var claim in claimsPrincipal.Claims)
            {
                new TokenClaimModel()
                {
                    Type = claim.Type,
                    Value = claim.Value
                };
            }
            var scopesClaim = new StringBuilder();

            var userAccountModel = await _userAccountService.GetUserAccount(claimsPrincipal.UserAccountId);

            foreach (var scope in scopes)
            {
                scopesClaim.Append(scope.Type);
            }

            tokenClaims.Add(new TokenClaimModel()
            {
                Type = "scopes",
                Value = scopesClaim.ToString()
            });

            tokenClaims.Add(new TokenClaimModel("iss", this.ToString()));
            tokenClaims.Add(new TokenClaimModel("sub", userAccountModel.Username));
            tokenClaims.Add(new TokenClaimModel("aud", userAccountModel.Username));
            tokenClaims.Add(new TokenClaimModel("exp", "20"));
            tokenClaims.Add(new TokenClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
            tokenClaims.Add(new TokenClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));

            var accessToken = _tokenService.CreateToken(tokenClaims);
            return accessToken;
        }

        public async Task<string> MapUserIdToken(ClaimsPrincipal claimsPrincipal)
        {
            var tokenClaims = new List<TokenClaimModel>();

            var userAccountModel = await _userAccountService.GetUserAccount(claimsPrincipal.UserAccountId);
            var profile = await _userProfileService.GetUserProfileByAccountId(claimsPrincipal.UserAccountId);
           
            tokenClaims.Add(new TokenClaimModel("id", claimsPrincipal.UserAccountId.ToString()));
            tokenClaims.Add(new TokenClaimModel("Scope", "id"));
            tokenClaims.Add(new TokenClaimModel("iss", this.ToString()));
            tokenClaims.Add(new TokenClaimModel("sub", userAccountModel.Username));
            tokenClaims.Add(new TokenClaimModel("aud", userAccountModel.Username));
            tokenClaims.Add(new TokenClaimModel("exp", "20"));
            tokenClaims.Add(new TokenClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
            tokenClaims.Add(new TokenClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
            tokenClaims.Add(new TokenClaimModel("firstName", profile.FirstName));
            tokenClaims.Add(new TokenClaimModel("lastName", profile.Surname));
            tokenClaims.Add(new TokenClaimModel("birthdate", profile.DateOfBirth.ToString()));
            tokenClaims.Add(new TokenClaimModel("username", userAccountModel.Username));
            tokenClaims.Add(new TokenClaimModel("accountType", userAccountModel.AccountType));
            tokenClaims.Add(new TokenClaimModel("accountStatus", userAccountModel.AccountStatus));

            var idToken = _tokenService.CreateToken(tokenClaims);

            return idToken;
        }
    }
}