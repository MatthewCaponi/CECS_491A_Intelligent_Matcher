using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;


namespace BusinessLayerUnitTests
{
    [TestClass]
    public class ConsoleLogTargetTests
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
        [DataRow("TextLogTest", EventName.NetworkEvent)]
        public void LogToTarget_WriteNetworkEventToConsoleLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange

            var currentConsoleOut = Console.Out;

            ConsoleLogTarget target = new ConsoleLogTarget();


            using (var consoleOutput = new ConsoleOutputChecker())
            {
                target.LogToTarget(expectedMessage, eventName);
                expectedMessage = "New " + eventName.ToString() + ": " + expectedMessage;
                Assert.AreEqual(expectedMessage, consoleOutput.GetOuput());
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);


        }
        [DataTestMethod]
        [DataRow("TextLogTest", EventName.UserEvent)]
        public void LogToTarget_WriteUserEventToConsoleLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange

            var currentConsoleOut = Console.Out;

            ConsoleLogTarget target = new ConsoleLogTarget();


            using (var consoleOutput = new ConsoleOutputChecker())
            {
                target.LogToTarget(expectedMessage, eventName);
                expectedMessage = "New " + eventName.ToString() + ": " + expectedMessage;
                Assert.AreEqual(expectedMessage, consoleOutput.GetOuput());
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);


        }

        [DataTestMethod]
        [DataRow("TextLogTest", EventName.SecurityEvent)]
        public void LogToTarget_WriteSecurityEventToConsoleLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            // Arrange

            var currentConsoleOut = Console.Out;

            ConsoleLogTarget target = new ConsoleLogTarget();


            using (var consoleOutput = new ConsoleOutputChecker())
            {
                target.LogToTarget(expectedMessage, eventName);
                expectedMessage = "New " + eventName.ToString() + ": " + expectedMessage;
                Assert.AreEqual(expectedMessage, consoleOutput.GetOuput());
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);


        }


    }
}
