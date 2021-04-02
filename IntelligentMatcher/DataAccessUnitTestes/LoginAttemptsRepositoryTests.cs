using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class LoginAttemptsRepositoryTests
    {
        #region Test Setup
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;

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

        #region Functional Tests
        [TestMethod]
        public async Task GetAllLoginAttempts_AtLeastTwoLoginAttemptsExist_ReturnsCorrectIds()
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            IEnumerable<LoginAttemptsModel> loginAttempts = await loginAttemptsRepository.GetAllLoginAttempts();

            // Assert
            int i = 1;
            foreach (LoginAttemptsModel loginAttempt in loginAttempts)
            {
                if (loginAttempt.Id == i)
                {
                    ++i;
                    continue;
                }

                Assert.IsTrue(false);
                return;
            }

            Assert.IsTrue(true);
        }

        [DataTestMethod]
        [DataRow(20)]
        public async Task GetAllLoginAttempts_AtLeastTwoLoginAttemptsExist_ReturnsCorrectNumberOfLoginAttempts
            (int numLoginAttempts)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            IEnumerable<LoginAttemptsModel> loginAttempts = await loginAttemptsRepository.GetAllLoginAttempts();

            // Assert
            int i = 1;
            foreach (LoginAttemptsModel loginAttempt in loginAttempts)
            {
                if (loginAttempt.Id == i)
                {
                    ++i;
                    continue;
                }
            }

            if (i == numLoginAttempts + 1)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(21, "127.0.0.21", 0, "3/28/2007 7:13:50 PM +00:00")]
        [DataRow(21, "127.0.0.22", 0, "3/28/2007 8:13:50 PM +00:00")]
        [DataRow(21, "127.0.0.23", 0, "3/28/2007 9:13:50 PM +00:00")]
        public async Task CreateLoginAttempts_IpAddressDoesntExist_LoginAttemptsIdExists
            (int expectedId, string ipAddress, int loginCounter, string suspensionEndTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData());

            LoginAttemptsModel loginAttemptsModel = new LoginAttemptsModel();

            loginAttemptsModel.Id = expectedId;
            loginAttemptsModel.IpAddress = ipAddress;
            loginAttemptsModel.LoginCounter = loginCounter;
            loginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndTime);

            // Act
            await loginAttemptsRepository.CreateLoginAttempts(loginAttemptsModel);
            var actualLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsById(expectedId);

            // Assert
            Assert.IsTrue(actualLoginAttempt.Id == expectedId);
        }

        [DataTestMethod]
        [DataRow(21, "127.0.0.21", 0, "3/28/2007 7:13:50 PM +00:00")]
        public async Task CreateLoginAttempts_IpAddressDoesntExist_DataIsAccurate
            (int expectedId, string expectedIpAddress, int expectedLoginCounter, string expectedSuspensionEndTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData());

            LoginAttemptsModel loginAttemptsModel = new LoginAttemptsModel();

            loginAttemptsModel.Id = expectedId;
            loginAttemptsModel.IpAddress = expectedIpAddress;
            loginAttemptsModel.LoginCounter = expectedLoginCounter;
            loginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse(expectedSuspensionEndTime);

            // Act
            await loginAttemptsRepository.CreateLoginAttempts(loginAttemptsModel);
            var actualLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsById(expectedId);

            // Assert
            Assert.IsTrue
                (
                    actualLoginAttempt.Id == expectedId &&
                    actualLoginAttempt.IpAddress == expectedIpAddress &&
                    actualLoginAttempt.LoginCounter == expectedLoginCounter &&
                    actualLoginAttempt.SuspensionEndTime == DateTimeOffset.Parse(expectedSuspensionEndTime)
                );
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetLoginAttemptsById_LoginAttemptExists_ReturnsLoginAttempts(int expectedId)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = 
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var loginAttemptModel = await loginAttemptsRepository.GetLoginAttemptsById(expectedId);
            var actualId = loginAttemptModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow(1, "127.0.0.1")]
        [DataRow(2, "127.0.0.2")]
        public async Task GetLoginAttemptsById_LoginAttemptExists_IpAddressCorrect(int expectedId, string expectedIpAddress)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var loginAttemptModel = await loginAttemptsRepository.GetLoginAttemptsById(expectedId);
            var actualIpAddress = loginAttemptModel.IpAddress;

            // Assert
            Assert.IsTrue(actualIpAddress == expectedIpAddress);
        }

        [DataTestMethod]
        [DataRow(1, "127.0.0.1")]
        [DataRow(2, "127.0.0.2")]
        public async Task GetLoginAttemptsByIpAddress_LoginAttemptExists_ReturnLoginAttempts
            (int expectedId, string expectedIpAddress)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var loginAttemptModel = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(expectedIpAddress);
            var actualId = loginAttemptModel.Id;

            // Assert
            Assert.IsTrue(actualId == expectedId);
        }

        [DataTestMethod]
        [DataRow("127.0.0.1")]
        [DataRow("127.0.0.2")]
        public async Task GetLoginAttemptsByIpAddress_LoginAttemptExists_IpAddressIsCorrect
            (string expectedIpAddress)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var loginAttemptModel = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(expectedIpAddress);
            var actualIpAddress = loginAttemptModel.IpAddress;

            // Assert
            Assert.IsTrue(actualIpAddress == expectedIpAddress);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task IncrementLoginCounterById_LoginCounterIsAccurate(int id)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var oldLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsById(id);
            await loginAttemptsRepository.IncrementLoginCounterById(id);
            var newLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsById(id);

            // Assert
            Assert.IsTrue(newLoginAttempt.LoginCounter == (oldLoginAttempt.LoginCounter + 1));
        }

        [DataTestMethod]
        [DataRow("127.0.0.1")]
        [DataRow("127.0.0.2")]
        [DataRow("127.0.0.3")]
        public async Task IncrementLoginCounterByIpAddress_LoginCounterIsAccurate(string ipAddress)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var oldLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);
            await loginAttemptsRepository.IncrementLoginCounterByIpAddress(ipAddress);
            var newLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(newLoginAttempt.LoginCounter == (oldLoginAttempt.LoginCounter + 1));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task ResetLoginCounterById_LoginCounterIsAccurate(int id)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await loginAttemptsRepository.ResetLoginCounterById(id);
            var newLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsById(id);

            // Assert
            Assert.IsTrue(newLoginAttempt.LoginCounter == 0);
        }

        [DataTestMethod]
        [DataRow("127.0.0.1")]
        [DataRow("127.0.0.2")]
        [DataRow("127.0.0.3")]
        public async Task ResetLoginCounterByIpAddress_LoginCounterIsAccurate(string ipAddress)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await loginAttemptsRepository.ResetLoginCounterByIpAddress(ipAddress);
            var newLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);

            // Assert
            Assert.IsTrue(newLoginAttempt.LoginCounter == 0);
        }

        [DataTestMethod]
        [DataRow(1, "3/28/2007 8:13:50 PM +00:00")]
        [DataRow(2, "3/28/2007 8:13:50 PM +00:00")]
        [DataRow(3, "3/28/2007 8:13:50 PM +00:00")]
        public async Task UpdateSuspensionEndTimeById_SuspensionEndtimeIsAccurate(int id, string expectedSuspensionEndTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await loginAttemptsRepository.UpdateSuspensionEndTimeById(id, 
                DateTimeOffset.Parse(expectedSuspensionEndTime));
            var newLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsById(id);
            var actualSuspensionEndTime = newLoginAttempt.SuspensionEndTime;

            // Assert
            Assert.IsTrue(actualSuspensionEndTime == DateTimeOffset.Parse(expectedSuspensionEndTime));
        }

        [DataTestMethod]
        [DataRow("127.0.0.1", "3/28/2007 8:13:50 PM +00:00")]
        [DataRow("127.0.0.2", "3/28/2007 8:13:50 PM +00:00")]
        [DataRow("127.0.0.3", "3/28/2007 8:13:50 PM +00:00")]
        public async Task UpdateSuspensionEndTimeByIpAddress_SuspensionEndtimeIsAccurate(string ipAddress,
            string expectedSuspensionEndTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await loginAttemptsRepository.UpdateSuspensionEndTimeByIpAddress(ipAddress,
                DateTimeOffset.Parse(expectedSuspensionEndTime));
            var newLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);
            var actualSuspensionEndTime = newLoginAttempt.SuspensionEndTime;

            // Assert
            Assert.IsTrue(actualSuspensionEndTime == DateTimeOffset.Parse(expectedSuspensionEndTime));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task DeleteLoginAttemptsById_AccountExists_AccountIsNull(int id)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository =
                new LoginAttemptsRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            await loginAttemptsRepository.DeleteLoginAttemptsById(id);
            var retrievedLoginAttempt = await loginAttemptsRepository.GetLoginAttemptsById(id);

            // Assert
            Assert.IsNull(retrievedLoginAttempt);
        }
        #endregion

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow(400)]
        public async Task GetAllLoginAttempts_AtLeastTwoLoginAttemptsExist_ExecutionTimeLessThan400Milliseconds
            (long expectedMaxExecutionTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await loginAttemptsRepository.GetAllLoginAttempts();
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(21, "127.0.0.21", 0, "3/28/2007 7:13:50 PM +00:00", 400)]
        public async Task CreateLoginAttempts_ExecutionTimeLessThan400Milliseconds
            (int loginAttemptId, string ipAddress, int loginCounter, string suspensionEndTime, long expectedMaxExecutionTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData());

            LoginAttemptsModel loginAttemptsModel = new LoginAttemptsModel();

            loginAttemptsModel.Id = loginAttemptId;
            loginAttemptsModel.IpAddress = ipAddress;
            loginAttemptsModel.LoginCounter = loginCounter;
            loginAttemptsModel.SuspensionEndTime = DateTimeOffset.Parse(suspensionEndTime);

            // Act
            var timer = Stopwatch.StartNew();
            await loginAttemptsRepository.CreateLoginAttempts(loginAttemptsModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, 400)]
        [DataRow(2, 400)]
        [DataRow(3, 400)]
        public async Task DeleteLoginAttemptsById_ExecutionTimeLessThan400Milliseconds
            (int id, long expectedMaxExecutionTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await loginAttemptsRepository.DeleteLoginAttemptsById(id);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, 400)]
        [DataRow(2, 400)]
        [DataRow(3, 400)]
        public async Task IncrementLoginCounterById_ExecutionTimeLessThan400Milliseconds
            (int id, long expectedMaxExecutionTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await loginAttemptsRepository.IncrementLoginCounterById(id);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, 400)]
        [DataRow(2, 400)]
        [DataRow(3, 400)]
        public async Task ResetLoginCounterById_ExecutionTimeLessThan400Milliseconds
            (int id, long expectedMaxExecutionTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await loginAttemptsRepository.ResetLoginCounterById(id);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow(1, "3/28/2008 7:13:50 PM +00:00", 400)]
        [DataRow(2, "3/28/2008 7:13:50 PM +00:00", 400)]
        [DataRow(3, "3/28/2008 7:13:50 PM +00:00", 400)]
        public async Task UpdateSuspensionEndtimeById_ExecutionTimeLessThan400Milliseconds
            (int id, string suspensionEndTime, long expectedMaxExecutionTime)
        {
            // Arrange
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository
                (new SQLServerGateway(), new ConnectionStringData());

            // Act
            var timer = Stopwatch.StartNew();
            await loginAttemptsRepository.UpdateSuspensionEndTimeById(id, DateTimeOffset.Parse(suspensionEndTime));
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }
        #endregion
    }
}
