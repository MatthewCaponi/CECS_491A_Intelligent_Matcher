using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserAccessControlServices
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<List<BusinessModels.UserAccessControl.ResourceModel>> GetAllResources()
        {
            var resources = await _resourceRepository.GetAllResources();

            List<BusinessModels.UserAccessControl.ResourceModel> resourceList =
                new List<BusinessModels.UserAccessControl.ResourceModel>();

            foreach (var dataResourceModel in resources)
            {
                var resourceModel = ModelConverterService.ConvertTo(dataResourceModel,
                    new BusinessModels.UserAccessControl.ResourceModel());

                resourceList.Add(resourceModel);
            }

            return resourceList;
        }

        public async Task<BusinessModels.UserAccessControl.ResourceModel> GetResource(int id)
        {
            var dataResource = await _resourceRepository.GetResourceById(id);
            var resource = ModelConverterService.ConvertTo(dataResource, new BusinessModels.UserAccessControl.ResourceModel());

            return resource;
        }

        public async Task<int> RegisterResource(BusinessModels.UserAccessControl.ResourceModel resourceModel)
        {
            var dataResource = ModelConverterService.ConvertTo(resourceModel, new Models.User_Access_Control.ResourceModel());
            var resourceId = await _resourceRepository.CreateResource(dataResource);

            return resourceId;
        }

        public async Task<int> RemoveResource(int id)
        {
            var changesMade = await _resourceRepository.DeleteResource(id);

            return changesMade;
        }
    }
}
