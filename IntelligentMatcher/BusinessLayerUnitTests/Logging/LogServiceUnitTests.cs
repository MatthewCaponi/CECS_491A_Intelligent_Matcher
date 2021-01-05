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
    public class LogServiceUnitTests
    {
        [DataTestMethod]
        [DataRow(EventName.UserEvent, "101.191.143.47", 12, UserProfileModel.AccountType.User, "User succesfully created")]
        public void LogTrace_UserLogToText_CorrectMessage(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType, string message)
        {
            // Arrange
            ILoggingEvent loggingEvent = new UserLoggingEvent(eventName, ipAddress, userId, accountType);
            List<ILogTarget> targets = new List<ILogTarget>();
            targets.Add(new TextLogTarget());
            ILogService logService = new LogService<UserProfileModel>(targets);

            // Act
            logService.LogTrace(loggingEvent, message);
 
            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Text);
            Debug.WriteLine("Message: " + actualMessage);
            StringAssert.Contains(actualMessage, message);
            StringAssert.Contains(actualMessage, ipAddress);
        }

        [DataTestMethod]
        [DataRow(EventName.UserEvent, "101.191.143.47", 12, UserProfileModel.AccountType.User, "User succesfully created")]
        public void LogTrace_UserLogToJSON_CorrectMessage(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType, string message)
        {
            // Arrange
            ILoggingEvent loggingEvent = new UserLoggingEvent(eventName, ipAddress, userId, accountType);
            List<ILogTarget> targets = new List<ILogTarget>();
            targets.Add(new JsonLogTarget());
            ILogService logService = new LogService<UserProfileModel>(targets);

            // Act
            logService.LogTrace(loggingEvent, message);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Json);
            Debug.WriteLine("Message: " + actualMessage);
            StringAssert.Contains(actualMessage, message);
        }


        [DataTestMethod]
        [DataRow(EventName.SecurityEvent, 12, "Security Issue")]
        public void LogTrace_UserLogToJSON_Security(EventName eventName, int userId, string message)
        {
            // Arrange

            Uri google = new Uri("http://www.google.com/");

            ILoggingEvent loggingEvent = new SecurityLoggingEvent(eventName, userId, google);
            List<ILogTarget> targets = new List<ILogTarget>();
            targets.Add(new JsonLogTarget());
            ILogService logService = new LogService<UserProfileModel>(targets);

            // Act
            logService.LogTrace(loggingEvent, message);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Json);
            Debug.WriteLine("Message: " + actualMessage);
            StringAssert.Contains(actualMessage, message);
        }


        [DataTestMethod]
        [DataRow(EventName.NetworkEvent, "101.191.143.47", 12 ,"home", "Network Issue", "Chrome")]
        public void LogTrace_UserLogToJSON_Network(EventName eventName, string ipAddress, int userId, string pageRequest, string message, string userAgent)
        {
            // Arrange
            Uri google = new Uri("http://www.google.com/");

            ILoggingEvent loggingEvent = new NetworkLoggingEvent(eventName, userId, ipAddress, pageRequest, "sampleUrl", userAgent);
            List<ILogTarget> targets = new List<ILogTarget>();
            targets.Add(new JsonLogTarget());
            ILogService logService = new LogService<UserProfileModel>(targets);

            // Act
            logService.LogTrace(loggingEvent, message);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Json);
            Debug.WriteLine("Message: " + actualMessage);
            StringAssert.Contains(actualMessage, message);
        }

        [DataTestMethod]
        [DataRow(EventName.UserEvent, "101.191.143.47", 12, UserProfileModel.AccountType.User, "User succesfully created")]

        public void LogTrace_UserLogToConsole_CorrectMessage(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType, string message)
        {
            // Arrange
            ILoggingEvent loggingEvent = new UserLoggingEvent(eventName, ipAddress, userId, accountType);
            List<ILogTarget> targets = new List<ILogTarget>();
            targets.Add(new ConsoleLogTarget());
            ILogService logService = new LogService<UserProfileModel>(targets);

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
