using Archiving;
using BusinessModels;
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
        private readonly Mock<IFolderHandlerService> mockFolderHandlerService = new Mock<IFolderHandlerService>();

        #region Unit Tests
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task ArchiveLogFiles_ArchiveSuccess_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Returns(Task.FromResult(true));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var archiveResult = await archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsTrue(archiveResult.WasSuccessful);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task ArchiveLogFiles_ArchiveFailure_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Returns(Task.FromResult(false));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var archiveResult = await archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(archiveResult.WasSuccessful);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.FileError)]
        public async Task ArchiveLogFiles_IOException_ReturnFalse(string startTime, string endTime, ErrorMessage error)
        {
            // Arrange
            mockArchiveService.Setup(x => x.ArchiveLogFiles(new List<string>())).Throws(new IOException());

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var archiveResult = await archiveManager.ArchiveLogFiles(DateTimeOffset.Parse(startTime), DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(archiveResult.WasSuccessful);
            Assert.IsTrue(archiveResult.ErrorMessage == error);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task DeleteArchivedFiles_DeleteSuccess_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.DeleteArchivedFiles(new List<string>())).Returns(Task.FromResult(true));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var deleteResult = await archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsTrue(deleteResult.WasSuccessful);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task DeleteArchivedFiles_DeleteFailure_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.DeleteArchivedFiles(new List<string>())).Returns(Task.FromResult(false));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var deleteResult = await archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(deleteResult.WasSuccessful);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.FileError)]
        public async Task DeleteArchivedFiles_IOException_ReturnFalse(string startTime, string endTime, ErrorMessage error)
        {
            // Arrange
            mockArchiveService.Setup(x => x.DeleteArchivedFiles(new List<string>())).Throws(new IOException());

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var deleteResult = await archiveManager.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(deleteResult.WasSuccessful);
            Assert.IsTrue(deleteResult.ErrorMessage == error);

        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task RecoverLogFiles_RecoverSuccess_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.RecoverLogFiles(new List<string>())).Returns(Task.FromResult(true));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var recoverResult = await archiveManager.RecoverLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsTrue(recoverResult.WasSuccessful);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public async Task RecoverLogFiles_RecoverFailure_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            mockArchiveService.Setup(x => x.RecoverLogFiles(new List<string>())).Returns(Task.FromResult(false));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var recoverResult = await archiveManager.RecoverLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(recoverResult.WasSuccessful);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.FileError)]
        public async Task RecoverLogFiles_IOException_ReturnFalse(string startTime, string endTime, ErrorMessage error)
        {
            // Arrange
            mockArchiveService.Setup(x => x.RecoverLogFiles(new List<string>())).Throws(new IOException());

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            var actualResult = false;

            // Act
            var recoverResult = await archiveManager.RecoverLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime));

            // Assert
            Assert.IsFalse(recoverResult.WasSuccessful);
            Assert.IsTrue(recoverResult.ErrorMessage == error);

        }

        [DataTestMethod]
        [DataRow("Recovered", "Test_Logs", "User_Logging")]
        public async Task GetCategories_SubFoldersGet_ReturnList(string sub1, string sub2, string sub3)
        {
            // Arrange
            List<string> expectedResult = new List<string>();

            expectedResult.Add(sub1);
            expectedResult.Add(sub2);
            expectedResult.Add(sub3);

            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).FullName;
            string targetPath = $"{projectDirectory}\\logs";

            mockFolderHandlerService.Setup(x => x.GetSubFolders(targetPath)).Returns(Task.FromResult(expectedResult));

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var actualResult = await archiveManager.GetCategories();

            // Assert
            Assert.IsTrue(actualResult.SuccessValue.Count == expectedResult.Count);
        }

        [TestMethod]
        public async Task GetCategories_IOException_ReturnEmptyList()
        {
            // Arrange
            List<string> expectedResult = new List<string>();

            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).FullName;
            string targetPath = $"{projectDirectory}\\logs";

            mockFolderHandlerService.Setup(x => x.GetSubFolders(targetPath)).Throws(new IOException());

            IArchiveManager archiveManager = new ArchiveManager(mockArchiveService.Object, mockFolderHandlerService.Object);

            // Act
            var actualResult = await archiveManager.GetCategories();

            // Assert
            Assert.IsTrue(actualResult.SuccessValue.Count == expectedResult.Count);
        }
        #endregion
    }
}
