using BusinessModels;
using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class LoginAttemptsServiceIntegrationTests
    {
        #region Test Setup
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 10;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                LoginAttemptsModel loginAttemptsModel = new LoginAttemptsModel();
                loginAttemptsModel.Id = i;
                loginAttemptsModel.IpAddress = "127.0.0." + i;
                loginAttemptsModel.LoginCounter = i;
                loginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse("3/28/2007 7:13:50 PM +00:00");

                await loginAttemptsRepository.CreateLoginAttempts(loginAttemptsModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);
            var loginAttempts = await loginAttemptsRepository.GetAllLoginAttempts();

            foreach (var loginAttempt in loginAttempts)
            {
                await loginAttemptsRepository.DeleteLoginAttemptsById(loginAttempt.Id);
            }
            await DataAccessTestHelper.ReseedAsync("LoginAttempts", 0, connectionString, dataGateway);
        }
        #endregion

        #region Integration Tests AddIpAddress
        [DataTestMethod]
        [DataRow("127.0.0.11", 0)]
        public async Task AddIpAddress_IpDoesntExist_ReturnTrue(string ipAddress, int loginCounter)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);

            var loginAttemptsModel = new LoginAttemptsModel();
            var expectedResult = true;

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(loginAttemptsRepository);

            // Act
            var actualResult = await loginAttemptsService.AddIpAddress(ipAddress, loginCounter);

            // Assert
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion

        #region Integration Tests GetLoginAttemptsByIpAddress
        [DataTestMethod]
        [DataRow(1, "127.0.0.1", 1, "3/28/2007 7:13:50 PM +00:00")]
        public async Task GetLoginAttemptsByIpAddress_IpExists_ReturnBusinessLoginAttemptsModel(int id, string ipAddress,
            int loginCounter, string suspensionEndtime)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);

            var expectedResult = new BusinessLoginAttemptsModel();

            expectedResult.Id = id;
            expectedResult.IpAddress = ipAddress;
            expectedResult.LoginCounter = loginCounter;
            expectedResult.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndtime);

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(loginAttemptsRepository);

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
        [DataRow("127.0.0.15")]
        public async Task GetLoginAttemptsByIpAddress_IpDoesNotExists_ReturnNull(string ipAddress)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(loginAttemptsRepository);

            // Act
            var actualResult = await loginAttemptsService.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsNull(actualResult);
        }
        #endregion

        #region Integration Tests IncrementLoginCounterByIpAddress
        [DataTestMethod]
        [DataRow("127.0.0.1")]
        public async Task IncrementLoginCounterByIpAddress_IpExists_ReturnTrue(string ipAddress)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);

            var oldLoginAttemptsModel = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);
            var expectedResult = true;

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(loginAttemptsRepository);

            // Act
            var actualResult = await loginAttemptsService.IncrementLoginCounterByIpAddress(ipAddress);
            var newLoginAttemptsModel = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue
                (
                    actualResult == expectedResult &&
                    newLoginAttemptsModel.LoginCounter == (oldLoginAttemptsModel.LoginCounter + 1)
                );
        }
        #endregion

        #region Integration Tests ResetLoginCounterByIpAddress
        [DataTestMethod]
        [DataRow("127.0.0.1")]
        public async Task ResetLoginCounterByIpAddress_IpExists_ReturnTrue(string ipAddress)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);

            var expectedResult = true;

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(loginAttemptsRepository);

            // Act
            var actualResult = await loginAttemptsService.ResetLoginCounterByIpAddress(ipAddress);
            var newLoginAttemptsModel = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue
                (
                    actualResult == expectedResult &&
                    newLoginAttemptsModel.LoginCounter == 0
                );
        }
        #endregion

        #region Integration Tests SetSuspensionEndTimeByIpAddress
        [DataTestMethod]
        [DataRow("127.0.0.1", 1)]
        public async Task SetSuspensionEndTimeByIpAddress_IpExists_ReturnTrue(string ipAddress, int suspensionHours)
        {
            // Arrange
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);

            var expectedResult = true;

            ILoginAttemptsService loginAttemptsService = new LoginAttemptsService(loginAttemptsRepository);

            // Act
            var actualResult = await loginAttemptsService.SetSuspensionEndTimeByIpAddress(ipAddress, suspensionHours);

            // Assert
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion
    }
}
