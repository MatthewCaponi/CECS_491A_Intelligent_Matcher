using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
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
        [DataRow("DebugLogTest", EventName.UserEvent)]
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
            Debug.WriteLine("Message: " + actualMessage);
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






    }
}
