using BusinessModels.UserAccessControl;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServices
{
    public interface ITokenBuilderService
    {
        string CreateToken(JwtPayloadModel jwtPayloadModel, string secret, RsaSecurityKey key);
    }
}