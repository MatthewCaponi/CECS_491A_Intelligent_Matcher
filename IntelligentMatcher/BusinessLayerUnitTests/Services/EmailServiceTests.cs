using BusinessModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class EmailServiceTests
    {
        #region Functional Tests
        [DataTestMethod]
        [DataRow("shariffshaan@gmail.com", "support@infinimuse.com", true, "Test", "This is a test.",
            "This is a test!", "outbound", "Test Email", true)]
        public async Task SendEmail_EmailSent_Success(string recipient, string sender, bool trackOpens,
            string subject, string textBody, string htmlBody, string messageStream, string tag, bool expectedResult)
        {
            //Arrange
            EmailModel emailModel = new EmailModel();

            emailModel.Recipient = recipient;
            emailModel.Sender = sender;
            emailModel.TrackOpens = trackOpens;
            emailModel.Subject = subject;
            emailModel.TextBody = textBody;
            emailModel.HtmlBody = htmlBody;
            emailModel.MessageStream = messageStream;
            emailModel.Tag = tag;

            EmailService emailService = new EmailService();
            //Act
            var actualResult = await emailService.SendEmail(emailModel);
            //Asset
            Assert.IsTrue(actualResult == expectedResult);
        }

        [DataTestMethod]
        [DataRow("BadEmail", "support@infinimuse.com", true, "Test", "This is a test.",
            "This is a test!", "outbound", "Test Email", false)]
        public async Task SendEmail_EmailSent_EmailNotSent(string recipient, string sender, bool trackOpens,
            string subject, string textBody, string htmlBody, string messageStream, string tag, bool expectedResult)
        {
            //Arrange
            EmailModel emailModel = new EmailModel();

            emailModel.Recipient = recipient;
            emailModel.Sender = sender;
            emailModel.TrackOpens = trackOpens;
            emailModel.Subject = subject;
            emailModel.TextBody = textBody;
            emailModel.HtmlBody = htmlBody;
            emailModel.MessageStream = messageStream;
            emailModel.Tag = tag;

            EmailService emailService = new EmailService();
            //Act
            var actualResult = await emailService.SendEmail(emailModel);
            //Asset
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion

        #region Non-Functional Tests
        [DataTestMethod]
        [DataRow("shariffshaan@gmail.com", "support@infinimuse.com", true, "Test", "This is a test.",
            "This is a test!", "outbound", "Test Email", 5000)]
        public async Task SendEmail_EmailSent_LessThan5Seconds(string recipient, string sender, bool trackOpens,
            string subject, string textBody, string htmlBody, string messageStream, string tag,
            int expectedTime)
        {
            //Arrange
            EmailModel emailModel = new EmailModel();

            emailModel.Recipient = recipient;
            emailModel.Sender = sender;
            emailModel.TrackOpens = trackOpens;
            emailModel.Subject = subject;
            emailModel.TextBody = textBody;
            emailModel.HtmlBody = htmlBody;
            emailModel.MessageStream = messageStream;
            emailModel.Tag = tag;

            EmailService emailService = new EmailService();
            //Act
            var timer = Stopwatch.StartNew();
            await emailService.SendEmail(emailModel);
            var actualTime = timer.ElapsedMilliseconds;
            Debug.WriteLine("Actual Execution Time: " + actualTime);
            //Assert
            Assert.IsTrue(actualTime <= expectedTime);
        }
        #endregion
    }
}
