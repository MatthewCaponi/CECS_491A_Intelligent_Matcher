using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Archiving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.Archiving
{
    [TestClass]
    public class FolderHandlerServiceTests
    {
        private readonly ILogService logService = new LogService();
        private readonly IFolderHandlerService folderHandlerService = new FolderHandlerService();

        [DataTestMethod]
        [DataRow("Recovered", "Test_Logs", "User_Logging", "User Logged In", LogLevel.info, LogTarget.All)]
        public async Task GetSubFolders_SubFoldersGet_ReturnList(string sub1, string sub2, string sub3, string message,
            LogLevel logLevel, LogTarget logTarget)
        {
            // Arrange
            List<string> expectedResult = new List<string>();

            expectedResult.Add(sub1);
            expectedResult.Add(sub2);
            expectedResult.Add(sub3);

            logService.Log(message, logTarget, logLevel, this.ToString(), sub1);
            logService.Log(message, logTarget, logLevel, this.ToString(), sub2);
            logService.Log(message, logTarget, logLevel, this.ToString(), sub3);

            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).FullName;
            string targetPath = $"{projectDirectory}\\logs";

            // Act
            var actualResult = await folderHandlerService.GetSubFolders(targetPath);

            // Assert
            Assert.IsTrue(actualResult.Count == expectedResult.Count);
        }

        [TestMethod]
        public async Task GetSubFolders_DirectoryDoesNotExist_ReturnNull()
        {
            // Arrange
            List<string> expectedResult = new List<string>();

            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).FullName;
            string targetPath = $"{projectDirectory}\\badFolder";

            // Act
            var actualResult = await folderHandlerService.GetSubFolders(targetPath);

            // Assert
            Assert.IsTrue(actualResult.Count == expectedResult.Count);
        }
    }
}
