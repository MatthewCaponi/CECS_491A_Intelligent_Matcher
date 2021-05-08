using BusinessModels;
using BusinessModels.UserAccessControl;
using System.Threading.Tasks;

namespace AuthorizationResolutionSystem
{
    public interface IAuthorizationResolutionManager
    {
        bool Authorize(string token, AccessPolicyModel accessPolicy);
    }
}