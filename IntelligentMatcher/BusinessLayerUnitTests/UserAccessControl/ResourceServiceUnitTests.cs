using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserAccessControlServices;

namespace BusinessLayerUnitTests.UserAccessControl
{
    [TestClass]
    public class ResourceServiceUnitTests
    {
        private readonly Mock<IResourceRepository> mockResourceRepository = new Mock<IResourceRepository>();

        #region Unit Tests
        [DataTestMethod]
        [DataRow(1, "TestResource1")]
        public async Task GetResource_ResourceModelFound_ReturnResourceModel(int id, string name)
        {
            // Arrange
            var resourceModel = new Models.User_Access_Control.ResourceModel();

            resourceModel.Id = id;
            resourceModel.Name = name;

            var expectedResult = new BusinessModels.UserAccessControl.ResourceModel();

            expectedResult.Id = id;
            expectedResult.Name = name;

            mockResourceRepository.Setup(x => x.GetResourceById(id)).Returns(Task.FromResult(resourceModel));

            IResourceService resourceService = new ResourceService(mockResourceRepository.Object);

            // Act
            var actualResult = await resourceService.GetResource(id);

            // Assert
            Assert.IsTrue(actualResult.Id == expectedResult.Id);
            Assert.IsTrue(actualResult.Name == expectedResult.Name);

        }
        #endregion
    }
}
