using BusinessModels.UserAccessControl;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServices
{
    public interface ITokenBuilderService
    {
        public string CreateToken(JwtPayloadModel jwtPayloadModel);
    }
}