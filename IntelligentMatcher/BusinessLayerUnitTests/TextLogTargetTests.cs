using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BusinessLayerUnitTests
{
    [TestClass]
    public class TextLogTargetTests
    {
        [TestInitialize()]
       public void Init()
        {

          
        }

        [TestCleanup()]
        public void CleanUp()
        {

        }

        

        [DataTestMethod]
        [DataRow("TextLogTest", EventName.UserEvent)]
        public void LogToTarget_WriteUserEventToTextLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new TextLogTarget();
            string fileName = eventName + DateTime.Today.Date.ToString() + ".txt";
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            // Act
            logTarget.LogToTarget(expectedMessage, eventName);

            // Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Assert.IsTrue(actualMessage == expectedMessage);

        }
    }
}
