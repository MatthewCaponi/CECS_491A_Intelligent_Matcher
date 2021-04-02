using BusinessModels;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class LoginAttemptsServiceTests
    {
        #region Unit Tests GetLoginAttemptsByIpAddress
        [DataTestMethod]
        [DataRow(1, "127.0.0.1", 1, "3/28/2007 7:13:50 PM +00:00")]
        public async Task GetLoginAttemptsByIpAddress_IpAddressFound_ReturnBusinessLoginAttemptsModel(int id, string ipAddress, 
            int loginCounter, string suspensionEndTime)
        {
            // Arrange
            // Setting up each dependency of LoginAttemptsService as a Mock
            Mock<ILoginAttemptsRepository> mockLoginAttemptsRepository = new Mock<ILoginAttemptsRepository>();

            var loginAttemptsModel = new LoginAttemptsModel();

            loginAttemptsModel.Id = id;
            loginAttemptsModel.IpAddress = ipAddress;
            loginAttemptsModel.LoginCounter = loginCounter;
            loginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndTime);

            var expectedResult = new BusinessLoginAttemptsModel();

            expectedResult.Id = id;
            expectedResult.IpAddress = ipAddress;
            expectedResult.LoginCounter = loginCounter;
            expectedResult.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndTime);

            mockLoginAttemptsRepository.Setup(x => x.GetLoginAttemptsByIpAddress(ipAddress)).Returns
                (Task.FromResult(loginAttemptsModel));

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(mockLoginAttemptsRepository.Object);

            // Act
            var actualResult = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue
                (
                    actualResult.Id == expectedResult.Id &&
                    actualResult.IpAddress == expectedResult.IpAddress &&
                    actualResult.LoginCounter == expectedResult.LoginCounter &&
                    actualResult.SuspensionEndTime == expectedResult.SuspensionEndTime
                );
        }

        [DataTestMethod]
        [DataRow(1, "127.0.0.13", 1, "3/28/2007 7:13:50 PM +00:00")]
        public async Task GetLoginAttemptsByIpAddress_IpAddressNotFoundFound_ReturNull(int id, string ipAddress,
            int loginCounter, string suspensionEndTime)
        {
            // Arrange
            // Setting up each dependency of LoginAttemptsService as a Mock
            Mock<ILoginAttemptsRepository> mockLoginAttemptsRepository = new Mock<ILoginAttemptsRepository>();

            LoginAttemptsModel loginAttemptsModel = null;

            mockLoginAttemptsRepository.Setup(x => x.GetLoginAttemptsByIpAddress(ipAddress)).Returns
                (Task.FromResult(loginAttemptsModel));

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(mockLoginAttemptsRepository.Object);

            // Act
            var actualResult = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsNull(actualResult);
        }
        #endregion
    }
}
