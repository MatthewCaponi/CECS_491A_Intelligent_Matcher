using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

/*
namespace BusinessLayerUnitTests
{
    [TestClass]
    public class FactoryTargetTest
    {
        [DataTestMethod]
        [DataRow(EventName.NetworkEvent, "101.191.143.47", 12, UserProfileModel.AccountType.User, "Network log succesfully created")]

        public void LogToTarget_WriteNetworkEventToJSONLog_ReadTextSucccessful(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType, string message)
        {
            // Arrange
            ILogServiceFactory logServiceFactory = new LogSeviceFactory();
            logServiceFactory.AddTarget(TargetType.Json);
            ILogService logService = logServiceFactory.CreateLogService<FactoryTargetTest>();
            ILoggingEvent loggingEvent = new NetworkLoggingEvent(eventName, userId, ipAddress,
                "samplePageRequest", "sampleURL", "sampleUserAgent");

            // Act
            logService.LogTrace(loggingEvent, message);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Json);
            Debug.WriteLine("Message: " + actualMessage);
            StringAssert.Contains(actualMessage, message.ToString());
            StringAssert.Contains(actualMessage, ipAddress.ToString());
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

            // Act
            logService.LogTrace(loggingEvent, message);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Text);
            Debug.WriteLine("Message: " + actualMessage);
            StringAssert.Contains(actualMessage, message);
            StringAssert.Contains(actualMessage, ipAddress);
        }

        [DataTestMethod]
        [DataRow(EventName.UserEvent, "101.191.143.47", 12, UserProfileModel.AccountType.User, "User succesfully created", "Exception thrown")]
        public void LogTrace_UserLogToTextException_CorrectMessage(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType, string message, string exceptionMessage)
        {
            // Arrange
            ILoggingEvent loggingEvent = new UserLoggingEvent(eventName, ipAddress, userId, accountType);
            ILogServiceFactory logServiceFactory = new LogSeviceFactory();
            logServiceFactory.AddTarget(TargetType.Text);
            ILogService logService = logServiceFactory.CreateLogService<FactoryTargetTest>();
            IOException e = new IOException(exceptionMessage);

            // Act
            logService.LogWarning(loggingEvent, e, message);

            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(eventName, TargetType.Text);
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
}*/