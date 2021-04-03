using BusinessModels;
using Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace BusinessLayerUnitTests.WebApi
{
    [TestClass]
    public class LoginControllerTests
    {
        [DataTestMethod]
        [DataRow(1, "TestUser1", "TestPassword1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "127.0.0.1")]
        public async Task Login_GotWebUserAccountModel_ReturnWebUserAccountModel(int accountId,
            string username, string password, string emailAddress, string accountType,
            string accountStatus, string creationDate, string updationDate, string ipAddress)
        {
            // Arrange
            // Setting up each dependency of LoginController as a Mock
            Mock<ILoginManager> mockLoginManager = new Mock<ILoginManager>();

            var webUserAccountModel = new WebUserAccountModel();

            webUserAccountModel.Id = accountId;
            webUserAccountModel.Username = username;
            webUserAccountModel.EmailAddress = emailAddress;
            webUserAccountModel.AccountType = accountType;
            webUserAccountModel.AccountStatus = accountStatus;
            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

            var expectedResult = new Result<WebUserAccountModel>();
            expectedResult.Success = true;
            expectedResult.SuccessValue = webUserAccountModel;

            mockLoginManager.Setup(x => x.Login(username, password, ipAddress)).Returns(Task.FromResult(expectedResult));
        }
    }
}
