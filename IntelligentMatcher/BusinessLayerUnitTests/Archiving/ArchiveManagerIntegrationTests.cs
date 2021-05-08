using Archiving;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Archiving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.Archiving
{
    [TestClass]
    public class ArchiveManagerIntegrationTests
    {
        private readonly IArchiveService archiveService = new ArchiveService();
        private readonly IFolderHandlerService folderHandlerService = new FolderHandlerService();
        private readonly ILogService logService = new LogService();

        #region Integration Tests
        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.All)]
        public async Task ArchiveLogFiles_LogsArchived_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);

            // Act
            var result = await archiveManager.ArchiveLogFiles(startTime, endTime);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All)]
        public async Task ArchiveLogFiles_NoValidLogs_ReturnFalse(string startTime, string endTime, string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);

            // Act
            var result = await archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.All, "User_Logging")]
        public async Task ArchiveLogFilesByCategory_LogsArchived_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget, string category)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), category);

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);

            // Act
            var result = await archiveManager.ArchiveLogFilesByCategory(startTime, endTime, category);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All,
            "User_Logging")]
        public async Task ArchiveLogFilesByCategory_TimeRangeNotValid_ReturnFalse(string startTime, string endTime, string message,
            LogLevel logLevel, LogTarget logTarget, string category)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), category);

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);

            // Act
            var result = await archiveManager.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime), category);

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All,
            "Error_Logging")]
        public async Task ArchiveLogFilesByCategory_CategoryNotExist_ReturnFalse(string startTime, string endTime, string message,
            LogLevel logLevel, LogTarget logTarget, string category)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);

            // Act
            var result = await archiveManager.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime), category);

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.All)]
        public async Task DeleteArchivedFiles_ArchiveDeleted_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);
            var archiveResult = await archiveManager.ArchiveLogFiles(startTime, endTime);

            // Act
            var deleteResult = await archiveManager.DeleteArchivedFiles(startTime, endTime);

            // Assert
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All)]
        public async Task DeleteArchivedFiles_NoValidZips_ReturnFalse(string startTime, string endTime, string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);

            var archiveResult = await archiveManager.ArchiveLogFiles(DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddDays(1));

            // Act
            var deleteResult = await archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(deleteResult);
        }

        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.All)]
        public async Task RecoverLogFiles_LogsRecovered_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);
            var archiveResult = await archiveManager.ArchiveLogFiles(startTime, endTime);

            // Act
            var recoverResult = await archiveManager.RecoverLogFiles(startTime, endTime);

            // Assert
            Assert.IsTrue(recoverResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All)]
        public async Task RecoverLogFiles_NoValidZips_ReturnFalse(string startTime, string endTime, string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);

            var archiveResult = await archiveManager.ArchiveLogFiles(DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddDays(1));

            // Act
            var recoverResult = await archiveManager.RecoverLogFiles(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(recoverResult);
        }

        [DataTestMethod]
        [DataRow("Recovered", "Test_Logs", "User_Logging")]
        public async Task GetCategories_GetAllCategories_ReturnList(string sub1, string sub2, string sub3)
        {
            // Arrange
            List<string> expectedResult = new List<string>();

            expectedResult.Add(sub1);
            expectedResult.Add(sub2);
            expectedResult.Add(sub3);

            IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);

            // Act
            var actualResult = await archiveManager.GetCategories();

            // Assert
            Assert.IsTrue(actualResult.Count == expectedResult.Count);
        }
        #endregion
    }
}
