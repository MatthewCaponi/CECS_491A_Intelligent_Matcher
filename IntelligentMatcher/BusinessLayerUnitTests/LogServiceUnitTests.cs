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

            string fileName = eventName + (DateTime.Today.Date).ToString(@"yyyy-MM-dd") + ".txt";
            string directory = "C:\\Users\\" + Environment.UserName + "\\logs\\" + eventName.ToString();
            string logPath = Path.Combine(directory, fileName);

            // Act
            logService.LogTrace(loggingEvent, message);
 
            //Assert
            string actualMessage = LogTargetHelper.ReadTestLog(logPath);
            Debug.WriteLine("Message: " + actualMessage);
            Assert.IsTrue(actualMessage != null);
        }

    }
}
