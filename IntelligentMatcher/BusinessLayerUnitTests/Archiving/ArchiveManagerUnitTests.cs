using Archiving;
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
    public class ArchiveManagerUnitTests
    {
        private static readonly Mock<IArchiveService> mockArchiveService = new Mock<IArchiveService>();

        #region Unit Tests
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void ArchiveLogFiles_ArchiveSuccess_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Returns(true);

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            // Act
            var archiveResult = archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsTrue(archiveResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void ArchiveLogFiles_ArchiveFailure_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Returns(false);

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            // Act
            var archiveResult = archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(archiveResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void ArchiveLogFiles_IOException_ReturnException(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Throws(new IOException());

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            var actualResult = false;

            // Act
            try
            {
                var archiveResult = archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));
            }
            catch (IOException)
            {
                actualResult = true;
            }
            finally
            {
                // Assert
                Assert.IsTrue(actualResult);
            }

        }
        #endregion
    }
}
