using BusinessModels.UserAccessControl;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityServices
{
    public interface ITokenService
    {
        string CreateToken(List<UserClaimModel> userClaims);
        bool ValidateToken(string token);
        List<UserClaimModel> ExtractClaims(string token);
    }
}