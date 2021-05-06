using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Archiving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayerUnitTests.Archiving
{
    [TestClass]
    public class ArchiveServiceTests
    {
        private readonly IArchiveService archiveService = new ArchiveService();

        [DataTestMethod]
        [DataRow("TestLog1.txt", "TestLog2.txt", "TestLog3.txt")]
        public void ArchiveLogFiles_LogsArchived_ReturnTrue(string file1, string file2, string file3)
        {
            // Arrange
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).FullName;
            string textDirectory = $"{projectDirectory}\\logs";

            string logPath1 = Path.Combine(textDirectory, file1);
            string logPath2 = Path.Combine(textDirectory, file2);
            string logPath3 = Path.Combine(textDirectory, file3);

            string builtMessage = "Test Log";

            if (!File.Exists(logPath1))
            {
                using (StreamWriter writer = File.CreateText(logPath1))
                {
                    writer.WriteLine(builtMessage);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(logPath1))
                {
                    writer.WriteLine(builtMessage);
                }
            }

            if (!File.Exists(logPath2))
            {
                using (StreamWriter writer = File.CreateText(logPath2))
                {
                    writer.WriteLine(builtMessage);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(logPath2))
                {
                    writer.WriteLine(builtMessage);
                }
            }

            if (!File.Exists(logPath3))
            {
                using (StreamWriter writer = File.CreateText(logPath3))
                {
                    writer.WriteLine(builtMessage);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(logPath3))
                {
                    writer.WriteLine(builtMessage);
                }
            }

            List<string> files = new List<string>();

            files.Add(logPath1);
            files.Add(logPath2);
            files.Add(logPath3);

            // Act
            var result = archiveService.ArchiveLogFiles(files);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArchiveLogFiles_EmptyList_ReturnFalse()
        {
            // Arrange
            List<string> files = new List<string>();

            // Act
            var result = archiveService.ArchiveLogFiles(files);

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("TestLog1.txt", "TestLog2.txt", "TestLog3.txt")]
        public void RecoverLogFiles_LogsRecovered_ReturnTrue(string file1, string file2, string file3)
        {
            // Arrange
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).FullName;

            string textDirectory = $"{projectDirectory}\\logs";

            string logPath1 = Path.Combine(textDirectory, file1);
            string logPath2 = Path.Combine(textDirectory, file2);
            string logPath3 = Path.Combine(textDirectory, file3);

            string builtMessage = "Test Log";

            if (!File.Exists(logPath1))
            {
                using (StreamWriter writer = File.CreateText(logPath1))
                {
                    writer.WriteLine(builtMessage);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(logPath1))
                {
                    writer.WriteLine(builtMessage);
                }
            }

            if (!File.Exists(logPath2))
            {
                using (StreamWriter writer = File.CreateText(logPath2))
                {
                    writer.WriteLine(builtMessage);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(logPath2))
                {
                    writer.WriteLine(builtMessage);
                }
            }

            if (!File.Exists(logPath3))
            {
                using (StreamWriter writer = File.CreateText(logPath3))
                {
                    writer.WriteLine(builtMessage);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(logPath3))
                {
                    writer.WriteLine(builtMessage);
                }
            }

            List<string> files = new List<string>();

            files.Add(logPath1);
            files.Add(logPath2);
            files.Add(logPath3);

            var archiveResult = archiveService.ArchiveLogFiles(files);

            string zipFile = $"{ (DateTime.Today.Date).ToString(@"yyyy-MM-dd")}.zip";
            string targetPath = $"{projectDirectory}\\archivedLogs";

            string zipPath = Path.Combine(targetPath, zipFile);

            List<string> zipFiles = new List<string>();

            zipFiles.Add(zipPath);

            // Act
            var recoverResult = archiveService.RecoverLogFiles(zipFiles);

            // Assert
            Assert.IsTrue(recoverResult);
        }

        [TestMethod]
        public void RecoverLogFiles_EmptyList_ReturnFalse()
        {
            // Arrange
            List<string> files = new List<string>();

            // Act
            var result = archiveService.RecoverLogFiles(files);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
