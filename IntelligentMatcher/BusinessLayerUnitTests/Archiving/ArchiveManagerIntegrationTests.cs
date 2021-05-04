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
        private static readonly IArchiveService archiveService = new ArchiveService();
        private static readonly ILogService logService = new LogService();
        private static readonly IArchiveManager archiveManager = new ArchiveManager(archiveService);

        #region Integration Tests
        [DataTestMethod]
        [DataRow("User Logged In", LogLevel.info, LogTarget.Text)]
        public void ArchiveLogFiles_LogsArchived_ReturnTrue(string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");

            var startTime = DateTimeOffset.UtcNow.AddDays(-1);
            var endTime = DateTimeOffset.UtcNow.AddDays(1);

            // Act
            var result = archiveManager.ArchiveLogFiles(startTime, endTime);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User Logged In", LogLevel.info, LogTarget.Text)]
        public void ArchiveLogFiles_NoValidLogs_ReturnFalse(string startTime, string endTime, string message, LogLevel logLevel,
            LogTarget logTarget)
        {
            // Arrange
            logService.Log(message, logTarget, logLevel, this.ToString(), "Test_Logs");
            logService.Log(message, logTarget, logLevel, this.ToString(), "User_Logging");

            // Act
            var result = archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(result);
        }
        #endregion
    }
}
