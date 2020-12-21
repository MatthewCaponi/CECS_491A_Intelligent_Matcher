using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BusinessLayerUnitTests
{
    [TestClass]
    public class TextLogTargetTests
    {
        [DataTestMethod]
        [DataRow("TextLogTest", EventName.UserEvent)]
        public void LogToTarget_WriteUserEventToTextLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new TextLogTarget();

            // Act
            logTarget.LogToTarget(expectedMessage, eventName);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Text);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());
        }

        [DataTestMethod]
        [DataRow("TextLogTest", EventName.SecurityEvent)]
        public void LogToTarget_WriteSecurityEventToTextLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new TextLogTarget();

            // Act
            logTarget.LogToTarget(expectedMessage, eventName);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Text);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());
        }

        [DataTestMethod]
        [DataRow("TextLogTest", EventName.NetworkEvent)]
        public void LogToTarget_WriteNetworkEventToTextLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new TextLogTarget();

            // Act
            logTarget.LogToTarget(expectedMessage, eventName);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Text);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());
        }
    }
}
