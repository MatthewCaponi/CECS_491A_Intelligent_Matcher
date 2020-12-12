using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace BusinessLayerUnitTests
{
    [TestClass]
    public class FactoryTargetTest
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
        [DataRow(EventName.UserEvent, "101.191.143.47", 12, UserProfileModel.AccountType.User, "User succesfully created")]

        public void LogToTarget_WriteNetworkEventToJSONLog_ReadTextSucccessful(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType, string message)
        {
            // Arrange

            ILogServiceFactory logServiceFactory = new LogSeviceFactory();
            logServiceFactory.AddTarget(TargetType.Json);
            ILogService logService = logServiceFactory.CreateLogService<FactoryTargetTest>();
            ILoggingEvent loggingEvent = new UserLoggingEvent(eventName, ipAddress, userId, accountType);
            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);

            // Act
            logService.LogTrace(loggingEvent, message);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Debug.WriteLine("Message: " + actualMessage);
            StringAssert.Contains(actualMessage, message);
            StringAssert.Contains(actualMessage, ipAddress);
        }






        [DataTestMethod]
        [DataRow(EventName.UserEvent, "101.191.143.47", 12, UserProfileModel.AccountType.User, "User succesfully created")]
        public void LogTrace_UserLogToText_CorrectMessage(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType, string message)
        {
            // Arrange
            ILoggingEvent loggingEvent = new UserLoggingEvent(eventName, ipAddress, userId, accountType);
            ILogServiceFactory logServiceFactory = new LogSeviceFactory();
            logServiceFactory.AddTarget(TargetType.Text);
            ILogService logService = logServiceFactory.CreateLogService<FactoryTargetTest>();

            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);

            // Act
            logService.LogTrace(loggingEvent, message);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Debug.WriteLine("Message: " + actualMessage);
            StringAssert.Contains(actualMessage, message);
            StringAssert.Contains(actualMessage, ipAddress);
        }


 

      

        [DataTestMethod]
        [DataRow(EventName.UserEvent, "101.191.143.47", 12, UserProfileModel.AccountType.User, "User succesfully created")]

        public void LogTrace_UserLogToConsole_CorrectMessage(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType, string message)
        {
            // Arrange
            ILoggingEvent loggingEvent = new UserLoggingEvent(eventName, ipAddress, userId, accountType);
            ILogServiceFactory logServiceFactory = new LogSeviceFactory();
            logServiceFactory.AddTarget(TargetType.Console);
            ILogService logService = logServiceFactory.CreateLogService<FactoryTargetTest>();

            var currentConsoleOut = Console.Out;

            // Act


            using (var consoleOutput = new ConsoleOutputChecker())
            {
                logService.LogTrace(loggingEvent, message);
                Debug.WriteLine("Console Output: " + consoleOutput.GetOuput());

                StringAssert.Contains(consoleOutput.GetOuput(), message);
            }


            //Assert

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

    }
}