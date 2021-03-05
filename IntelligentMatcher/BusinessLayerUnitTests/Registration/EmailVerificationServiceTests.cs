using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.Registration
{
    [TestClass]
    public class EmailVerificationServiceTests
    {
        [DataTestMethod]
        [DataRow("shariffshaan@gmail.com", true)]
        public async Task SendVerificationEmail_Success_EmailSent(string emailAddress, bool expectedResult)
        {
            //Arrange
            EmailVerificationService emailVerificationService = new EmailVerificationService();
            //Act
            var actualResult = await emailVerificationService.SendVerificationEmail(emailAddress);
            //Asset
            Assert.IsTrue(actualResult == expectedResult);
        }

        [DataTestMethod]
        [DataRow("BadEmail", false)]
        public async Task SendVerificationEmail_Failure_EmailNotSent(string emailAddress, bool expectedResult)
        {
            //Arrange
            EmailVerificationService emailVerificationService = new EmailVerificationService();
            //Act
            var actualResult = await emailVerificationService.SendVerificationEmail(emailAddress);
            //Asset
            Assert.IsTrue(actualResult == expectedResult);
        }
    }
}
