using BusinessModels.UserAccessControl;
using IdentityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using UserAccessControlServices;

namespace AuthenticationSystem
{
    public class AttributeAssignmentService : IAttributeAssignmentService
    {
        private readonly ITokenService _tokenService;
        private readonly IAssignmentPolicyService _assignmentPolicyService;

        public AttributeAssignmentService(ITokenService tokenService, IAssignmentPolicyService assignmentPolicyService)
        {
            _tokenService = tokenService;
            _assignmentPolicyService = assignmentPolicyService;
        }

        public List<string> GetScopes(string token, AssignmentPolicyModel assignmentPolicyModel)
        {
            var claims = _tokenService.ExtractClaims(token);
            var claimsPrincipal = new ClaimsPrincipal()
            {
                Claims = claims,
                Scopes = null
            };

            var role = claims.Where(a => a.Type == "Role").FirstOrDefault();

            if (role == null)
            {
                return null;
            }

            var accountType = role.Value;

            var scopes = _assignmentPolicyService.ConfigureAssignmentPolicy(accountType);

            return scopes;
        }
    }
}
