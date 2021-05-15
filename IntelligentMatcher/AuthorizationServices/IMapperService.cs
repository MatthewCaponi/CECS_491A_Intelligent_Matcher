using BusinessModels.UserAccessControl;
using System;
using System.Threading.Tasks;

namespace UserAccessControlServices
{
    public interface IMapperService
    {
        Task<string> MapUserAccessToken(ClaimsPrincipal claimsPrincipal);
        Task<string> MapUserIdToken(ClaimsPrincipal claimsPrincipal);
    }
}