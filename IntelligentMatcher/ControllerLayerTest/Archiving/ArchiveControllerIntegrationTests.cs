using Archiving;
using ControllerModels.ArchiveModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Archiving;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace ControllerLayerTest.Archiving
{
    //[TestClass]
    //public class ArchiveControllerIntegrationTests
    //{
    //    private readonly IArchiveService archiveService = new ArchiveService();
    //    private readonly IFolderHandlerService folderHandlerService = new FolderHandlerService();

    //    #region Non-Functional Tests
    //    [DataTestMethod]
    //    [DataRow("3/28/2017 7:13:50 PM +00:00", "3/28/2028 7:13:50 PM +00:00", 5000)]
    //    public async Task ArchiveLogFiles_ExecuteLessThan5Seconds(string startTime, string endTime, long expectedMaxExecutionTime)
    //    {
    //        // Arrange
    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);
    //        ArchiveController archiveController = new ArchiveController(archiveManager);

    //        // Act
    //        var timer = Stopwatch.StartNew();
    //        var actualResult = await archiveController.ArchiveLogFiles(archiveModel);
    //        timer.Stop();

    //        var actualExecutionTime = timer.ElapsedMilliseconds;
    //        Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

    //        // Assert
    //        Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2017 7:13:50 PM +00:00", "3/28/2028 7:13:50 PM +00:00", "User_Logging", 5000)]
    //    public async Task ArchiveLogFilesByCategory_ExecuteLessThan5Seconds(string startTime, string endTime, string category,
    //        long expectedMaxExecutionTime)
    //    {
    //        // Arrange
    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;
    //        archiveModel.Category = category;

    //        IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);
    //        ArchiveController archiveController = new ArchiveController(archiveManager);

    //        // Act
    //        var timer = Stopwatch.StartNew();
    //        var actualResult = await archiveController.ArchiveLogFilesByCategory(archiveModel);
    //        timer.Stop();

    //        var actualExecutionTime = timer.ElapsedMilliseconds;
    //        Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

    //        // Assert
    //        Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2017 7:13:50 PM +00:00", "3/28/2028 7:13:50 PM +00:00", 5000)]
    //    public async Task RecoverLogFiles_ExecuteLessThan5Seconds(string startTime, string endTime, long expectedMaxExecutionTime)
    //    {
    //        // Arrange
    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);
    //        ArchiveController archiveController = new ArchiveController(archiveManager);

    //        // Act
    //        var timer = Stopwatch.StartNew();
    //        var actualResult = await archiveController.RecoverLogFiles(archiveModel);
    //        timer.Stop();

    //        var actualExecutionTime = timer.ElapsedMilliseconds;
    //        Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

    //        // Assert
    //        Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
    //    }

    //    [DataTestMethod]
    //    [DataRow("3/28/2017 7:13:50 PM +00:00", "3/28/2028 7:13:50 PM +00:00", 5000)]
    //    public async Task DeleteArchivedFiles_ExecuteLessThan5Seconds(string startTime, string endTime, long expectedMaxExecutionTime)
    //    {
    //        // Arrange
    //        var archiveModel = new ArchiveModel();

    //        archiveModel.StartDate = startTime;
    //        archiveModel.EndDate = endTime;

    //        IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);
    //        ArchiveController archiveController = new ArchiveController(archiveManager);

    //        // Act
    //        var timer = Stopwatch.StartNew();
    //        var actualResult = await archiveController.DeleteArchivedFiles(archiveModel);
    //        timer.Stop();

    //        var actualExecutionTime = timer.ElapsedMilliseconds;
    //        Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

    //        // Assert
    //        Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
    //    }

    //    [DataTestMethod]
    //    [DataRow(5000)]
    //    public async Task GetCategories_ExecuteLessThan5Seconds(long expectedMaxExecutionTime)
    //    {
    //        // Arrange
    //        IArchiveManager archiveManager = new ArchiveManager(archiveService, folderHandlerService);
    //        ArchiveController archiveController = new ArchiveController(archiveManager);

    //        // Act
    //        var timer = Stopwatch.StartNew();
    //        var actualResult = await archiveController.GetCategories();
    //        timer.Stop();

    //        var actualExecutionTime = timer.ElapsedMilliseconds;
    //        Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

    //        // Assert
    //        Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
    //    }
    //    #endregion
    //}
}
