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

            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);



            // Act
            logTarget.LogToTarget(expectedMessage, eventName);


            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());

        }




        [DataTestMethod]
        [DataRow("TextLogTest", EventName.SecurityEvent)]
        public void LogToTarget_WriteSecurityEventToTextLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new TextLogTarget();

            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);



            // Act
            logTarget.LogToTarget(expectedMessage, eventName);


            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());

        }

        [DataTestMethod]
        [DataRow("TextLogTest", EventName.NetworkEvent)]
        public void LogToTarget_WriteNetworkEventToTextLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new TextLogTarget();

            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);



            // Act
            logTarget.LogToTarget(expectedMessage, eventName);


            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());

        }




        [DataTestMethod]
        [DataRow("TextLogTest", EventName.UserEvent)]
        public void LogToTarget_WriteUserEventToJSONLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new JsonLogTarget();

            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);



            // Act
            logTarget.LogToTarget(expectedMessage, eventName);


            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());

        }




        [DataTestMethod]
        [DataRow("TextLogTest", EventName.SecurityEvent)]
        public void LogToTarget_WriteSecurityEventToJSONLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new JsonLogTarget();

            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);



            // Act
            logTarget.LogToTarget(expectedMessage, eventName);


            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());

        }

        [DataTestMethod]
        [DataRow("TextLogTest", EventName.NetworkEvent)]
        public void LogToTarget_WriteNetworkEventToJSONLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new JsonLogTarget();

            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);



            // Act
            logTarget.LogToTarget(expectedMessage, eventName);


            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Assert.IsTrue(actualMessage == expectedMessage.ToString());

        }


        [DataTestMethod]
        [DataRow("TextLogTest", EventName.NetworkEvent)]
        public void LogToTarget_WriteNetworkEventToConsoleLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange
            ILogTarget logTarget = new ConsoleLogTarget();


            // 


            logTarget.LogToTarget(expectedMessage, eventName);
            string actualMessage = expectedMessage;
            //Assert
            Assert.IsTrue(actualMessage == expectedMessage.ToString());

        }
    }
}
