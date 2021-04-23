using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Exceptions;
using Microsoft.Data.SqlClient;
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
            try
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
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("No Resources Found.", e.InnerException);
            }
            
        }

        public async Task<BusinessModels.UserAccessControl.ResourceModel> GetResource(int id)
        {
            try
            {
                var dataResource = await _resourceRepository.GetResourceById(id);
                var resource = ModelConverterService.ConvertTo(dataResource, new BusinessModels.UserAccessControl.ResourceModel());

                return resource;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Resource Not Found.", e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Resource was Null.", e.InnerException);
            }

        }

        public async Task<int> RegisterResource(BusinessModels.UserAccessControl.ResourceModel resourceModel)
        {
            try
            {
                var dataResource = ModelConverterService.ConvertTo(resourceModel, new Models.User_Access_Control.ResourceModel());
                var resourceId = await _resourceRepository.CreateResource(dataResource);

                return resourceId;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Resource could not be created.", e.InnerException);
            }
            
        }

        public async Task<bool> RemoveResource(int id)
        {
            try
            {
                var changesMade = await _resourceRepository.DeleteResource(id);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Resource could not be deleted.", e.InnerException);
            }
        }
    }
}
