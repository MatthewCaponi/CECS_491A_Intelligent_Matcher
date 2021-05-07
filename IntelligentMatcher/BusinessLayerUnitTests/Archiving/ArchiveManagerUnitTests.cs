using Archiving;
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
    public class ArchiveManagerUnitTests
    {
        private readonly Mock<IArchiveService> mockArchiveService = new Mock<IArchiveService>();

        #region Unit Tests
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task ArchiveLogFiles_ArchiveSuccess_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Returns(Task.FromResult(true));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            // Act
            var archiveResult = await archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsTrue(archiveResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task ArchiveLogFiles_ArchiveFailure_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Returns(Task.FromResult(false));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            // Act
            var archiveResult = await archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(archiveResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task ArchiveLogFiles_IOException_ReturnException(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Throws(new IOException());

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            var actualResult = false;

            // Act
            try
            {
                var archiveResult = await archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
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

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task DeleteArchivedFiles_DeleteSuccess_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.DeleteArchivedFiles(new List<string>())).Returns(Task.FromResult(true));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            // Act
            var deleteResult = await archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task DeleteArchivedFiles_DeleteFailure_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.DeleteArchivedFiles(new List<string>())).Returns(Task.FromResult(false));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            // Act
            var deleteResult = await archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(deleteResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task DeleteArchivedFiles_IOException_ReturnException(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.DeleteArchivedFiles(new List<string>())).Throws(new IOException());

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            var actualResult = false;

            // Act
            try
            {
                var deleteResult = await archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
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

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task RecoverLogFiles_RecoverSuccess_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.RecoverLogFiles(new List<string>())).Returns(Task.FromResult(true));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            // Act
            var recoverResult = await archiveManager.RecoverLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsTrue(recoverResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task RecoverLogFiles_RecoverFailure_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.RecoverLogFiles(new List<string>())).Returns(Task.FromResult(false));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            // Act
            var recoverResult = await archiveManager.RecoverLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(recoverResult);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task RecoverLogFiles_IOException_ReturnException(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.RecoverLogFiles(new List<string>())).Throws(new IOException());

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object);

            var actualResult = false;

            // Act
            try
            {
                var recoverResult = await archiveManager.RecoverLogFiles(DateTimeOffset.Parse(startTime),
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
