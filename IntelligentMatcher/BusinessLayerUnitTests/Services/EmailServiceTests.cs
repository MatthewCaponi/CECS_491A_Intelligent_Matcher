using BusinessModels;
using DataAccess;
using DataAccess.Repositories;
using IntelligentMatcher.Services;
using Microsoft.Extensions.Configuration;
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
        [DataRow("matt@infinimuse.com", "support@infinimuse.com", true, "Test", "This is a test.",
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

            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

            _testConfigKeys.Add("Sender", "support@infinimuse.com");
            _testConfigKeys.Add("TrackOpens", "true");
            _testConfigKeys.Add("Subject", "Welcome!");
            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
            _testConfigKeys.Add("MessageStream", "outbound");
            _testConfigKeys.Add("Tag", "Welcome");
            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

            IConfiguration configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
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

            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

            _testConfigKeys.Add("Sender", "support@infinimuse.com");
            _testConfigKeys.Add("TrackOpens", "true");
            _testConfigKeys.Add("Subject", "Welcome!");
            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
            _testConfigKeys.Add("MessageStream", "outbound");
            _testConfigKeys.Add("Tag", "Welcome");
            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

            IConfiguration configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
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

            IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();

            _testConfigKeys.Add("Sender", "support@infinimuse.com");
            _testConfigKeys.Add("TrackOpens", "true");
            _testConfigKeys.Add("Subject", "Welcome!");
            _testConfigKeys.Add("TextBody", "Welcome to InfiniMuse!");
            _testConfigKeys.Add("MessageStream", "outbound");
            _testConfigKeys.Add("Tag", "Welcome");
            _testConfigKeys.Add("HtmlBody", "Thank you for registering! Please confirm your account with the link: <a href='{0}'>Confirm Your Account!</a><strong>Once confirmed you will have access to the features.</strong>");

            IConfiguration configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();
            EmailService emailService = new EmailService(new UserAccountRepository
             (new SQLServerGateway(), new ConnectionStringData()), new AccountVerificationRepo
             (new SQLServerGateway(), new ConnectionStringData()), new UserAccountService(new UserAccountRepository
                 (new SQLServerGateway(), new ConnectionStringData())), configuration);
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
