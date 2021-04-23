using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserAccessControlServices
{
    public interface IResourceService
    {
        Task<List<BusinessModels.UserAccessControl.ResourceModel>> GetAllResources();
        Task<BusinessModels.UserAccessControl.ResourceModel> GetResource(int id);
        Task<int> RegisterResource(BusinessModels.UserAccessControl.ResourceModel resourceModel);
        Task<bool> RemoveResource(int id);
    }
}
