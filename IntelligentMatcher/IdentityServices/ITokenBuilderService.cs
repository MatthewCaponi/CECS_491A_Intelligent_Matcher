using BusinessModels.UserAccessControl;

namespace IdentityServices
{
    public interface ITokenBuilderService
    {
        public string CreateToken(JwtPayloadModel jwtPayloadModel);
    }
}