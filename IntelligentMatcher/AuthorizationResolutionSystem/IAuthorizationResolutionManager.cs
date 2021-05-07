using BusinessModels;
using System.Threading.Tasks;

namespace AuthorizationResolutionSystem
{
    public interface IAuthorizationResolutionManager
    {
        bool Authorize(string token);
    }
}