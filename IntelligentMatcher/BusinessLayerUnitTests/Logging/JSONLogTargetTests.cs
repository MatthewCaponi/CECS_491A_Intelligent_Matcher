using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace BusinessLayerUnitTests
{
    [TestClass]
    public class JOSNLogTargetTests
    {
        [DataTestMethod]
        [DataRow("LALA", EventName.UserEvent)]
        public void LogToTarget_WriteUserEventToJSONLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new JsonLogTarget();

            // Act
            logTarget.LogToTarget(expectedMessage, eventName);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Json);
            StringAssert.Contains(actualMessage, expectedMessage.ToString());
        }

        [DataTestMethod]
        [DataRow("LALA", EventName.SecurityEvent)]
        public void LogToTarget_WriteSecurityEventToJSONLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new JsonLogTarget();

            // Act
            logTarget.LogToTarget(expectedMessage, eventName);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Json);
            StringAssert.Contains(actualMessage, expectedMessage.ToString());
        }

        [DataTestMethod]
        [DataRow("LALA", EventName.NetworkEvent)]
        public void LogToTarget_WriteNetworkEventToJSONLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new JsonLogTarget();

            // Act
            logTarget.LogToTarget(expectedMessage, eventName);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Json);
            Debug.WriteLine(expectedMessage);
            StringAssert.Contains(actualMessage, expectedMessage.ToString());
        }
    }
}
