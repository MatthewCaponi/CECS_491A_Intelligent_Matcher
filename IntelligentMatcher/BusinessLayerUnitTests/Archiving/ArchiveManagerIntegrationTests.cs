using Archiving;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Archiving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayerUnitTests.Archiving
{
    [TestClass]
    public class ArchiveManagerIntegrationTests
    {
        private readonly IArchiveService archiveService = new ArchiveService();
        private readonly ILogService logService = new LogService();

        #region Integration Tests
        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.All)]
        public void ArchiveLogFiles_LogsArchived_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            IArchiveManager archiveManager = new ArchiveManager(archiveService);

            // Act
            var result = archiveManager.ArchiveLogFiles(startTime, endTime);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All)]
        public void ArchiveLogFiles_NoValidLogs_ReturnFalse(string startTime, string endTime, string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            IArchiveManager archiveManager = new ArchiveManager(archiveService);

            // Act
            var result = archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.All, "User_Logging")]
        public void ArchiveLogFilesByCategory_LogsArchived_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget, string category)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), category);

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            IArchiveManager archiveManager = new ArchiveManager(archiveService);

            // Act
            var result = archiveManager.ArchiveLogFilesByCategory(startTime, endTime, category);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All,
            "User_Logging")]
        public void ArchiveLogFilesByCategory_TimeRangeNotValid_ReturnFalse(string startTime, string endTime, string message,
            LogLevel logLevel, LogTarget logTarget, string category)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), category);

            IArchiveManager archiveManager = new ArchiveManager(archiveService);

            // Act
            var result = archiveManager.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime),
                category);

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All,
            "Error_Logging")]
        public void ArchiveLogFilesByCategory_CategoryNotExist_ReturnFalse(string startTime, string endTime, string message,
            LogLevel logLevel, LogTarget logTarget, string category)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            IArchiveManager archiveManager = new ArchiveManager(archiveService);

            // Act
            var result = archiveManager.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime),
                category);

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.All)]
        public void DeleteArchivedFiles_ArchiveDeleted_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            IArchiveManager archiveManager = new ArchiveManager(archiveService);
            var archiveResult = archiveManager.ArchiveLogFiles(startTime, endTime);

            // Act
            var deleteResult = archiveManager.DeleteArchivedFiles(startTime, endTime);

            // Assert
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All)]
        public void DeleteArchivedFiles_NoValidZips_ReturnFalse(string startTime, string endTime, string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            IArchiveManager archiveManager = new ArchiveManager(archiveService);

            var archiveResult = archiveManager.ArchiveLogFiles(DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddDays(1));

            // Act
            var deleteResult = archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(deleteResult);
        }

        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.All)]
        public void RecoverLogFiles_LogsRecovered_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            IArchiveManager archiveManager = new ArchiveManager(archiveService);
            var archiveResult = archiveManager.ArchiveLogFiles(startTime, endTime);

            // Act
            var recoverResult = archiveManager.RecoverLogFiles(startTime, endTime);

            // Assert
            Assert.IsTrue(recoverResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.All)]
        public void RecoverLogFiles_NoValidZips_ReturnFalse(string startTime, string endTime, string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            IArchiveManager archiveManager = new ArchiveManager(archiveService);

            var archiveResult = archiveManager.ArchiveLogFiles(DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddDays(1));

            // Act
            var recoverResult = archiveManager.RecoverLogFiles(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(recoverResult);
        }
        #endregion
    }
}
