﻿using BusinessModels;
using BusinessModels.UserAccessControl;
using Cross_Cutting_Concerns;
using IdentityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccessControlServices;

namespace AuthorizationResolutionSystem
{
    public class AuthorizationResolutionManager : IAuthorizationResolutionManager
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationResolutionManager(ITokenService tokenService, IAuthorizationService authorizationService)
        {
            _tokenService = tokenService;
            _authorizationService = authorizationService;
        }

        public bool Authorize(string token, AccessPolicyModel accessPolicy)
        {
            var claims = _tokenService.ExtractClaims(token);
            var scopes = claims.Where(a => a.Type == "scopes").FirstOrDefault().Value.Split(',').ToList();
            var userScopes = new List<UserScopeModel>();
            foreach (var scope in scopes)
            {
                userScopes.Add(new UserScopeModel()
                {
                    Type = scope,
                    UserAccountId = -1
                });
            }
            var claimsPrincipal = new ClaimsPrincipal()
            {
                Claims = claims,
                Scopes = userScopes
            };

            return _authorizationService.ValidateAccessPolicy(claimsPrincipal, accessPolicy);
        }
    }
}
