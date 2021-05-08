using BusinessModels.UserAccessControl;
using IdentityServices;
using System;

namespace AuthenticationSystem
{
    public class AuthenticationManager
    {
        private readonly ITokenService _tokenService;

        public AuthenticationManager(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public bool Authenticate(string token, AssignmentPolicyModel assignmentPolicyModel)
        {
            var claims = _tokenService.ExtractClaims(token);

        }
    }
}
