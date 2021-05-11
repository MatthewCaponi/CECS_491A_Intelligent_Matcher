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
    public class MapperService
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IUserAccountService _userAccountService;
        private readonly ITokenService _tokenService;

        public MapperService(IUserProfileService userProfileService, IUserAccountService userAccountService, ITokenService tokenService)
        {
            _userProfileService = userProfileService;
            _userAccountService = userAccountService;
            _tokenService = tokenService;
        }
        public async Task<string> MapUserIdTokenAsync(int id)
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
    }
}
