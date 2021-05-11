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
        public async Task<string> MapUserIdToken(int id)
        {
            var profile = await _userProfileService.GetUserProfileByAccountId(id);
            var account = await _userAccountService.GetUserAccount(id);
            var idToken = _tokenService.CreateToken(new List<UserClaimModel>()
                    {
                        new UserClaimModel("id", id.ToString()),
                        new UserClaimModel("Scope", "id"),
                            new UserClaimModel("iss", this.ToString()),
                            new UserClaimModel("sub", account.Username ),
                            new UserClaimModel("aud", account.Username),
                            new UserClaimModel("exp", "1"),
                            new UserClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                            new UserClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                            new UserClaimModel("firstName", profile.FirstName),
                            new UserClaimModel("lastName", profile.Surname),
                            new UserClaimModel("birthdate", profile.DateOfBirth.ToString()),
                            new UserClaimModel("username", account.Username),
                            new UserClaimModel("accountType", account.AccountType),
                            new UserClaimModel("accountStatus", account.AccountStatus)
                                });

            return idToken;
        }

        public async Task<string> MapUserAccessToken(int id)
        {
            var account = await _userAccountService.GetUserAccount(id);
            var scopes = _assignmentPolicyService.ConfigureAssignmentPolicy(account.AccountType);

            StringBuilder delimitedScopes = new StringBuilder();
            foreach (var scope in scopes)
            {
                delimitedScopes.Append(scope + ",");
            }

            var accessToken = _tokenService.CreateToken(new List<UserClaimModel>()
                    {
                          new UserClaimModel("scopes", delimitedScopes.ToString()),
                            new UserClaimModel("role", account.AccountType),
                            new UserClaimModel("id", account.Id.ToString()),
                            new UserClaimModel("iss", this.ToString()),
                            new UserClaimModel("sub", account.Username),
                            new UserClaimModel("aud", account.Username),
                            new UserClaimModel("exp", "30"),
                            new UserClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                            new UserClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                                });

            return accessToken;
        }
    }
}
