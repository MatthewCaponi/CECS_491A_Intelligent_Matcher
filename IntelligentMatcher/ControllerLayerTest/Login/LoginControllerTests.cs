//using BusinessModels;
//using ControllerModels;
//using Login;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using UserManagement.Models;
//using WebApi.Controllers;

//namespace BusinessLayerUnitTests.WebApi
//{
//    [TestClass]
//    public class LoginControllerTests
//    {
//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestPassword1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
//            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "127.0.0.1")]
//        public async Task Login_GotWebUserAccountModel_ReturnUserInformation(int accountId,
//            string username, string password, string emailAddress, string accountType,
//            string accountStatus, string creationDate, string updationDate, string ipAddress)
//        {
//            // Arrange
//            // Setting up each dependency of LoginController as a Mock
//            Mock<ILoginManager> mockLoginManager = new Mock<ILoginManager>();

//            var loginModel = new LoginModel();

//            loginModel.username = username;
//            loginModel.password = password;
//            loginModel.ipAddress = ipAddress;

//            var webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = webUserAccountModel;

//            mockLoginManager.Setup(x => x.Login(username, password, ipAddress)).Returns(Task.FromResult(expectedResult));

//            LoginController loginController = new LoginController(mockLoginManager.Object);

//            // Act
//            var actualResult = await loginController.Login(loginModel);

//            // Assert
//            Assert.IsTrue
//                (
//                    actualResult.Success == expectedResult.WasSuccessful &&
//                    actualResult.Username == expectedResult.SuccessValue.Username &&
//                    actualResult.AccountType == expectedResult.SuccessValue.AccountType.ToString() &&
//                    actualResult.AccountStatus == expectedResult.SuccessValue.AccountStatus.ToString()
//                );
//        }

//        [DataTestMethod]
//        [DataRow("TestUser1", "TestEmailAddress1", "3/28/2007 12:00:00 AM -07:00")]
//        public async Task ForgotUsername_GotUsername_ReturnUsername(string username, string emailAddress, string dateOfBirth)
//        {
//            // Arrange
//            // Setting up each dependency of LoginController as a Mock
//            Mock<ILoginManager> mockLoginManager = new Mock<ILoginManager>();

//            var forgotInformationModel = new ForgotInformationModel();

//            forgotInformationModel.emailAddress = emailAddress;
//            forgotInformationModel.dateOfBirth = dateOfBirth;

//            var expectedResult = new Result<string>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = username;

//            mockLoginManager.Setup(x => x.ForgotUsername(emailAddress, DateTimeOffset.Parse(dateOfBirth)))
//                .Returns(Task.FromResult(expectedResult));

//            LoginController loginController = new LoginController(mockLoginManager.Object);

//            // Act
//            var actualResult = await loginController.ForgotUsername(forgotInformationModel);

//            // Assert
//            Assert.IsTrue
//                (
//                    actualResult.Success == expectedResult.WasSuccessful &&
//                    actualResult.Username == expectedResult.SuccessValue
//                );
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
//            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "3/28/2007 12:00:00 AM -07:00")]
//        public async Task ForgotPasswordValidation_GotBusinessUserAccountCode_ReturnAccountId(int accountId,
//            string username, string emailAddress, string accountType, string accountStatus, string creationDate,
//            string updationDate, string dateOfBirth)
//        {
//            // Arrange
//            // Setting up each dependency of LoginController as a Mock
//            Mock<ILoginManager> mockLoginManager = new Mock<ILoginManager>();

//            var forgotInformationModel = new ForgotInformationModel();

//            forgotInformationModel.username = username;
//            forgotInformationModel.emailAddress = emailAddress;
//            forgotInformationModel.dateOfBirth = dateOfBirth;

//            var webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            var businessUserAccountCodeModel = new BusinessUserAccountCodeModel();

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = webUserAccountModel;

//            mockLoginManager.Setup(x => x.ForgotPasswordValidation(username, emailAddress, DateTimeOffset.Parse(dateOfBirth)))
//                .Returns(Task.FromResult(expectedResult));

//            LoginController loginController = new LoginController(mockLoginManager.Object);

//            // Act
//            var actualResult = await loginController.ForgotPasswordValidation(forgotInformationModel);

//            // Assert
//            Assert.IsTrue
//                (
//                    actualResult.Success == expectedResult.WasSuccessful &&
//                    actualResult.AccountId == expectedResult.SuccessValue.Id
//                );
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
//            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "ABC1")]
//        public async Task ForgotPasswordCodeInput_GotWebUserAccount_ReturnAccountId(int accountId,
//            string username, string emailAddress, string accountType, string accountStatus, string creationDate,
//            string updationDate, string code)
//        {
//            // Arrange
//            // Setting up each dependency of LoginController as a Mock
//            Mock<ILoginManager> mockLoginManager = new Mock<ILoginManager>();

//            var forgotPasswordCodeInputModel = new ForgotPasswordCodeInputModel();

//            forgotPasswordCodeInputModel.code = code;
//            forgotPasswordCodeInputModel.accountId = accountId;

//            var webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = webUserAccountModel;

//            mockLoginManager.Setup(x => x.ForgotPasswordCodeInput(code, accountId)).Returns(Task.FromResult(expectedResult));

//            LoginController loginController = new LoginController(mockLoginManager.Object);

//            // Act
//            var actualResult = await loginController.ForgotPasswordCodeInput(forgotPasswordCodeInputModel);

//            // Assert
//            Assert.IsTrue
//                (
//                    actualResult.Success == expectedResult.WasSuccessful &&
//                    actualResult.AccountId == expectedResult.SuccessValue.Id
//                );
//        }

//        [DataTestMethod]
//        [DataRow(1, "TestUser1", "TestEmailAddress1", "TestAccountType1", "TestAccountStatus1",
//            "3/28/2007 7:13:50 PM +00:00", "3/28/2007 7:13:50 PM +00:00", "TestPassword1")]
//        public async Task ResetPassword_GotWebUserAccount_ReturnAccountId(int accountId,
//            string username, string emailAddress, string accountType, string accountStatus, string creationDate,
//            string updationDate, string password)
//        {
//            // Arrange
//            // Setting up each dependency of LoginController as a Mock
//            Mock<ILoginManager> mockLoginManager = new Mock<ILoginManager>();

//            var resetPasswordModel = new ResetPasswordModel();

//            resetPasswordModel.password = password;
//            resetPasswordModel.accountId = accountId;

//            var webUserAccountModel = new WebUserAccountModel();

//            webUserAccountModel.Id = accountId;
//            webUserAccountModel.Username = username;
//            webUserAccountModel.EmailAddress = emailAddress;
//            webUserAccountModel.AccountType = accountType;
//            webUserAccountModel.AccountStatus = accountStatus;
//            webUserAccountModel.CreationDate = DateTimeOffset.Parse(creationDate);
//            webUserAccountModel.UpdationDate = DateTimeOffset.Parse(updationDate);

//            var expectedResult = new Result<WebUserAccountModel>();
//            expectedResult.WasSuccessful = true;
//            expectedResult.SuccessValue = webUserAccountModel;

//            mockLoginManager.Setup(x => x.ResetPassword(password, accountId)).Returns(Task.FromResult(expectedResult));

//            LoginController loginController = new LoginController(mockLoginManager.Object);

//            // Act
//            var actualResult = await loginController.ResetPassword(resetPasswordModel);

//            // Assert
//            Assert.IsTrue
//                (
//                    actualResult.Success == expectedResult.WasSuccessful &&
//                    actualResult.AccountId == expectedResult.SuccessValue.Id
//                );
//        }
//    }
//}
