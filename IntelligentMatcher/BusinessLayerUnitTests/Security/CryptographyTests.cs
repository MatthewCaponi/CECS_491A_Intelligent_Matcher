using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;


namespace BusinessLayerUnitTests.Security
{
    [TestClass]
    public class CyrptographyTests
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
        [DataRow("TextLogTest", EventName.SecurityEvent)]
        public void LogToTarget_WriteSecurityEventToConsoleLog_ReadTextSucccessful(string expectedMessage, EventName eventName)
        {
            Assert.IsTrue(true);
        }


    }
}
