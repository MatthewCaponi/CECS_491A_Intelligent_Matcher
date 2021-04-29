using BusinessModels.UserAccessControl;
using System.Collections.Generic;

namespace IdentityServices
{
    public interface ITokenService
    {
        string CreateToken(List<UserClaimModel> userClaims);
        public bool JwtSecurityToken(string token);
    }
}