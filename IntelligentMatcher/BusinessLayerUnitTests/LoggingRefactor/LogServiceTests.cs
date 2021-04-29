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
        [DataRow("User Logged In", LogLevel.info, LogTarget.Text)]
        [DataRow("New User Added", LogLevel.warning, LogTarget.Json)]
        public void LogService_NoException(string message, LogLevel logLevel, LogTarget logTarget)
        {
            // Arrange
            ILogService logService = new LogService();

            //Act
            try
            {
                logService.Log(message, logTarget, logLevel, this.ToString());
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Assert.IsTrue(false);       
            }
        }

        [DataTestMethod]
        [DataRow("User Validation Failed", "Foreign key constraint clashes", LogLevel.info, LogTarget.Text)]
        [DataRow("Null object was passed", "Value cannot be null", LogLevel.warning, LogTarget.Json)]
        public void LogService_ExceptionThrown_ExceptionPrinted(string message, string exception, LogLevel logLevel, LogTarget logTarget)
        {
            // Arrange
            ILogService logService = new LogService();

            //Act
            try
            {
                try
                {
                    throw new NullReferenceException(exception);
                }
                catch (Exception e)
                {
                    throw new NullReferenceException(exception);
                }
            }
            catch (Exception e)
            {

                    logService.Log(message, logTarget, logLevel, e, this.ToString());
                    Assert.IsTrue(true);
            }
        }
    }
}
