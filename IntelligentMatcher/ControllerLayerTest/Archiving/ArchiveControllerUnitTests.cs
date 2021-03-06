﻿using Archiving;
using BusinessModels;
using ControllerModels.ArchiveModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace ControllerLayerTest.Archiving
{
    //[TestClass]
    //public class ArchiveControllerUnitTests
    //{
    //    private readonly Mock<IArchiveManager> mockArchiveManager = new Mock<IArchiveManager>();

    //    #region Unit Tests
    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
    //    public async Task ArchiveLogFiles_ArchiveComplete_ReturnTrue(string startTime, string endTime)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = true;

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        var archiveResult = Result<bool>.Success(true);

    //        mockArchiveManager.Setup(x => x.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
    //            DateTimeOffset.Parse(endTime))).Returns(Task.FromResult(archiveResult));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.ArchiveLogFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoSuchFilesExist)]
    //    public async Task ArchiveLogFiles_ArchiveNotComplete_ReturnFalse(string startTime, string endTime, ErrorMessage error)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = false;
    //        expectedResult.ErrorMessage = error.ToString();

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        mockArchiveManager.Setup(x => x.ArchiveLogFiles(DateTimeOffset.Parse(startTime),
    //            DateTimeOffset.Parse(endTime))).Returns(Task.FromResult(Result<bool>.Failure(ErrorMessage.NoSuchFilesExist)));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.ArchiveLogFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //        Assert.IsTrue(actualResult.Value.ErrorMessage == expectedResult.ErrorMessage);
    //    }

    //    [DataTestMethod]
    //    [DataRow(ErrorMessage.Null)]
    //    public async Task ArchiveLogFiles_PassingNull_ReturnFalse(ErrorMessage error)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = false;
    //        expectedResult.ErrorMessage = error.ToString();

    //        var archiveModel = new ArchiveModel();

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.ArchiveLogFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //        Assert.IsTrue(actualResult.Value.ErrorMessage == expectedResult.ErrorMessage);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User_Logging")]
    //    public async Task ArchiveLogFilesByCategory_ArchiveComplete_ReturnTrue(string startTime, string endTime, string category)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = true;

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;
    //        archiveModel.Category = category;

    //        mockArchiveManager.Setup(x => x.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime),
    //            DateTimeOffset.Parse(endTime), category)).Returns(Task.FromResult(Result<bool>.Success(true)));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.ArchiveLogFilesByCategory(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", "User_Logging", ErrorMessage.NoSuchFilesExist)]
    //    public async Task ArchiveLogFilesByCategory_ArchiveNotComplete_ReturnFalse(string startTime, string endTime, string category,
    //        ErrorMessage error)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = false;
    //        expectedResult.ErrorMessage = error.ToString();

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;
    //        archiveModel.Category = category;

    //        mockArchiveManager.Setup(x => x.ArchiveLogFilesByCategory(DateTimeOffset.Parse(startTime),
    //            DateTimeOffset.Parse(endTime), category)).Returns(Task.FromResult(Result<bool>.Failure(ErrorMessage.NoSuchFilesExist)));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.ArchiveLogFilesByCategory(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //        Assert.IsTrue(actualResult.Value.ErrorMessage == expectedResult.ErrorMessage);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.Null)]
    //    public async Task ArchiveLogFilesByCategory_PassingNull_ReturnFalse(string startTime, string endTime, ErrorMessage error)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = false;
    //        expectedResult.ErrorMessage = error.ToString();

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.ArchiveLogFilesByCategory(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //        Assert.IsTrue(actualResult.Value.ErrorMessage == expectedResult.ErrorMessage);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
    //    public async Task RecoverLogFiles_RecoverComplete_ReturnTrue(string startTime, string endTime)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = true;

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        mockArchiveManager.Setup(x => x.RecoverLogFiles(DateTimeOffset.Parse(startTime),
    //            DateTimeOffset.Parse(endTime))).Returns(Task.FromResult(Result<bool>.Success(true)));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.RecoverLogFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoSuchFilesExist)]
    //    public async Task RecoverLogFiles_RecoverNotComplete_ReturnFalse(string startTime, string endTime, ErrorMessage error)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = false;
    //        expectedResult.ErrorMessage = error.ToString();

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        mockArchiveManager.Setup(x => x.RecoverLogFiles(DateTimeOffset.Parse(startTime),
    //            DateTimeOffset.Parse(endTime))).Returns(Task.FromResult(Result<bool>.Failure(ErrorMessage.NoSuchFilesExist)));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.RecoverLogFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //        Assert.IsTrue(actualResult.Value.ErrorMessage == expectedResult.ErrorMessage);
    //    }

    //    [DataTestMethod]
    //    [DataRow(ErrorMessage.Null)]
    //    public async Task RecoverLogFiles_PassingNull_ReturnFalse(ErrorMessage error)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = false;
    //        expectedResult.ErrorMessage = error.ToString();

    //        var archiveModel = new ArchiveModel();

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.RecoverLogFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //        Assert.IsTrue(actualResult.Value.ErrorMessage == expectedResult.ErrorMessage);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00")]
    //    public async Task DeleteArchivedFiles_DeleteComplete_ReturnTrue(string startTime, string endTime)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = true;

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        mockArchiveManager.Setup(x => x.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
    //            DateTimeOffset.Parse(endTime))).Returns(Task.FromResult(Result<bool>.Success(true)));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.DeleteArchivedFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2007 7:13:50 PM +00:00", "3/28/2008 7:13:50 PM +00:00", ErrorMessage.NoSuchFilesExist)]
    //    public async Task DeleteArchivedFiles_DeleteNotComplete_ReturnFalse(string startTime, string endTime, ErrorMessage error)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = false;
    //        expectedResult.ErrorMessage = error.ToString();

    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        mockArchiveManager.Setup(x => x.DeleteArchivedFiles(DateTimeOffset.Parse(startTime),
    //            DateTimeOffset.Parse(endTime))).Returns(Task.FromResult(Result<bool>.Failure(ErrorMessage.NoSuchFilesExist)));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.DeleteArchivedFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //        Assert.IsTrue(actualResult.Value.ErrorMessage == expectedResult.ErrorMessage);
    //    }

    //    [DataTestMethod]
    //    [DataRow(ErrorMessage.Null)]
    //    public async Task DeleteArchivedFiles_PassingNull_ReturnFalse(ErrorMessage error)
    //    {
    //        // Arrange
    //        var expectedResult = new ArchiveResultModel();

    //        expectedResult.Success = false;
    //        expectedResult.ErrorMessage = error.ToString();

    //        var archiveModel = new ArchiveModel();

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.DeleteArchivedFiles(archiveModel);

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Success == expectedResult.Success);
    //        Assert.IsTrue(actualResult.Value.ErrorMessage == expectedResult.ErrorMessage);
    //    }


    //    [DataTestMethod]
    //    [DataRow("Recovered", "Test_Logs", "User_Logging")]
    //    public async Task GetCategories_SubFoldersGet_ReturnList(string sub1, string sub2, string sub3)
    //    {
    //        // Arrange
    //        List<string> expectedResult = new List<string>();

    //        expectedResult.Add(sub1);
    //        expectedResult.Add(sub2);
    //        expectedResult.Add(sub3);

    //        mockArchiveManager.Setup(x => x.GetCategories()).Returns(Task.FromResult(Result<List<string>>.Success(expectedResult)));

    //        ArchiveController archiveController = new ArchiveController(mockArchiveManager.Object);

    //        // Act
    //        var actualResult = await archiveController.GetCategories();

    //        // Assert
    //        Assert.IsTrue(actualResult.Value.Count == expectedResult.Count);
    //    }
    //    #endregion
    //}
}
