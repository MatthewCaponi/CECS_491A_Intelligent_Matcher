using UserManagement.Models;

namespace Security
{
    public interface ITokenService
    {
        string CreateToken(WebUserAccountModel webUser);
    }
}