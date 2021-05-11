using System;
using System.Threading.Tasks;

namespace UserAccessControlServices
{
    public interface IMapperService
    {
        Task<String> MapUserAccessToken(int id);
        Task<String> MapUserIdToken(int id);
    }
}