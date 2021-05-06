using Archiving;
using ControllerModels.ArchiveModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Archiving;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WebApi.Controllers;

namespace ControllerLayerTest.Archiving
{
    [TestClass]
    public class ArchiveControllerIntegrationTests
    {
        private readonly IArchiveService archiveService = new ArchiveService();

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow("3/28/2017 7:13:50 PM +00:00", "3/28/2028 7:13:50 PM +00:00", 5000)]
        public void ArchiveLogFiles_ExecuteLessThan5Seconds(string startTime, string endTime, long expectedMaxExecutionTime)
        {
            // Arrange
            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            IArchiveManager archiveManager = new ArchiveManager(archiveService);
            ArchiveController archiveController = new ArchiveController(archiveManager);

            // Act
            var timer = Stopwatch.StartNew();
            var actualResult = archiveController.ArchiveLogFiles(archiveModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }

        [DataTestMethod]
        [DataRow("3/28/2017 7:13:50 PM +00:00", "3/28/2028 7:13:50 PM +00:00", 5000)]
        public void RecoverLogFiles_ExecuteLessThan5Seconds(string startTime, string endTime, long expectedMaxExecutionTime)
        {
            // Arrange
            var archiveModel = new ArchiveModel();

            archiveModel.StartDate = startTime;
            archiveModel.EndDate = endTime;

            IArchiveManager archiveManager = new ArchiveManager(archiveService);
            ArchiveController archiveController = new ArchiveController(archiveManager);

            // Act
            var timer = Stopwatch.StartNew();
            var actualResult = archiveController.RecoverLogFiles(archiveModel);
            timer.Stop();

            var actualExecutionTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualExecutionTime);

            // Assert
            Assert.IsTrue(actualExecutionTime <= expectedMaxExecutionTime);
        }
        #endregion
    }
}
