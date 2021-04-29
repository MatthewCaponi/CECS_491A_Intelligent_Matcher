using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BusinessLayerUnitTests.LoggingRefactor
{
    [TestClass]
    public class LogServiceTests
    {
        [DataTestMethod]
        [DataRow("User Validation Failed", LogLevel.info, LogTarget.Text)]
        [DataRow("New User Added", LogLevel.warning, LogTarget.Json)]
        public void LogService_NoException(string message, LogLevel logLevel, LogTarget logTarget)
        {
            // Arrange
            ILogService logService = new LogService();

            //Act
            try
            {
                logService.Log(message, logTarget, logLevel);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Assert.IsTrue(false);       
            }
        }
    }
}
