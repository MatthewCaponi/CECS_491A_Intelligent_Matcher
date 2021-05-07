﻿using Archiving;
using BusinessModels;
using ControllerModels.ArchiveModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebApi.Controllers;

namespace ControllerLayerTest.Archiving
{
    [TestClass]
    public class ArchiveControllerUnitTests
    {
        private readonly Mock<IArchiveManager> mockArchiveManager = new Mock<IArchiveManager>();

        #region Unit Tests
        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void ArchiveLogFiles_ArchiveComplete_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = true;

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Returns(true);

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.ArchiveLogFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoSuchFilesExist)]
        public void ArchiveLogFiles_ArchiveNotComplete_ReturnFalse(string startTime, string endTime, ErrorMessage error)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;
            expectedResult.ErrorMessage = error.ToString();

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Returns(false);

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.ArchiveLogFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(ErrorMessage.Null)]
        public void ArchiveLogFiles_PassingNull_ReturnFalse(ErrorMessage error)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;
            expectedResult.ErrorMessage = error.ToString();

            var archiveModel = new ArchiveModel();

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.ArchiveLogFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void ArchiveLogFiles_IOException_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Throws(new IOException());

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.ArchiveLogFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User_Logging")]
        public void ArchiveLogFilesByCategory_ArchiveComplete_ReturnTrue(string startTime, string endTime, string category)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = true;

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;
            archiveModel.Category = category;

            mockArchiveManager.Setup(x => x.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime), category)).Returns(true);

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.ArchiveLogFilesByCategory(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User_Logging", ErrorMessage.NoSuchFilesExist)]
        public void ArchiveLogFilesByCategory_ArchiveNotComplete_ReturnFalse(string startTime, string endTime, string category,
            ErrorMessage error)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;
            expectedResult.ErrorMessage = error.ToString();

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;
            archiveModel.Category = category;

            mockArchiveManager.Setup(x => x.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime), category)).Returns(false);

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.ArchiveLogFilesByCategory(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.Null)]
        public void ArchiveLogFilesByCategory_PassingNull_ReturnFalse(string startTime, string endTime, ErrorMessage error)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;
            expectedResult.ErrorMessage = error.ToString();

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.ArchiveLogFilesByCategory(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User_Logging")]
        public void ArchiveLogFilesByCategory_IOException_ReturnFalse(string startTime, string endTime, string category)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;
            archiveModel.Category = category;

            mockArchiveManager.Setup(x => x.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime), category)).Throws(new IOException());

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.ArchiveLogFilesByCategory(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void RecoverLogFiles_RecoverComplete_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = true;

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.RecoverLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Returns(true);

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.RecoverLogFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoSuchFilesExist)]
        public void RecoverLogFiles_RecoverNotComplete_ReturnFalse(string startTime, string endTime, ErrorMessage error)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;
            expectedResult.ErrorMessage = error.ToString();

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.RecoverLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Returns(false);

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.RecoverLogFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(ErrorMessage.Null)]
        public void RecoverLogFiles_PassingNull_ReturnFalse(ErrorMessage error)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;
            expectedResult.ErrorMessage = error.ToString();

            var archiveModel = new ArchiveModel();

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.RecoverLogFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void RecoverLogFiles_IOException_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.RecoverLogFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Throws(new IOException());

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.RecoverLogFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void DeleteArchivedFiles_DeleteComplete_ReturnTrue(string startTime, string endTime)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = true;

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Returns(true);

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.DeleteArchivedFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoSuchFilesExist)]
        public void DeleteArchivedFiles_DeleteNotComplete_ReturnFalse(string startTime, string endTime, ErrorMessage error)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;
            expectedResult.ErrorMessage = error.ToString();

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Returns(false);

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.DeleteArchivedFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow(ErrorMessage.Null)]
        public void DeleteArchivedFiles_PassingNull_ReturnFalse(ErrorMessage error)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;
            expectedResult.ErrorMessage = error.ToString();

            var archiveModel = new ArchiveModel();

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.DeleteArchivedFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
            Assert.IsTrue(actualResult.ErrorMessage == expectedResult.ErrorMessage);
        }

        [DataTestMethod]
        [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
        public void DeleteArchivedFiles_IOException_ReturnFalse(string startTime, string endTime)
        {
            // Arrange
            var expectedResult = new ArchiveResultModel();

            expectedResult.Success = false;

            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            mockArchiveManager.Setup(x => x.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
                DateTimeOffset.Parse(endTime))).Throws(new IOException());

            ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

            // Act
            var actualResult = archiveController.DeleteArchivedFiles(archiveModel);

            // Assert
            Assert.IsTrue(actualResult.Success == expectedResult.Success);
        }
        #endregion
    }
}
