using Models.User_Access_Control;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public interface IResourceRepository
    {
        Task<int> CreateResource(ResourceModel model);
        Task<int> DeleteResource(int id);
        Task<IEnumerable<ResourceModel>> GetAllResources();
        Task<ResourceModel> GetResourceById(int id);
        Task<int> UpdateResource(ResourceModel model);
    }
}