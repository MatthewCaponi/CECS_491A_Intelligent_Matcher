using DataAccess.Repositories;
using IntelligentMatcher.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class ValidationServiceTests
    {
        [DataTestMethod]
        [DataRow(1, 20, "Test", 1, 1)]
        public async Task UserExists_UserDoesExist_ReturnsTrue(int id, int numAccounts, string namePrefix, int startingId,
           int idIncrementFactor)
        {
            // Arrange
            // Setting up each dependency of ValidationService as a Mock
            Mock<IUserProfileService> mockProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> mockAccountService = new Mock<IUserAccountService>();

            List<WebUserAccountModel> userAccounts = new List<WebUserAccountModel>();

            // Test helper to assist in instantiating a list of WebUserAccountModels
            var testHelper = new BusinessLayerTestHelper();

            /* Parameters:
             * numAccounts: How many would you like to generate?
             * namePrefix: What would you like to be the common prefix before every attribute?
             * startingId: What number would you like id to start from?
             * idIncrementFactor: How many numbers would you like each subsequent id to skip by?
             */
            userAccounts = testHelper.PopulateListOfAccountModels(numAccounts, namePrefix, startingId, idIncrementFactor);

            // This function reads as: If GetAllUserAccounts is called, then return a list of userAccounts
            mockAccountService.Setup(x => x.GetAllUserAccounts()).Returns(Task.FromResult(userAccounts));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IValidationService validationService = new ValidationService(mockAccountService.Object, mockProfileService.Object);

            // Act
            // Call the function that is being tested
            var actualResult = await validationService.UserExists(id);

            // Assert
            // This unit test will pass if it returns true
            Assert.IsTrue(actualResult);
        }

        [DataTestMethod]
        [DataRow(30, 20, "Test", 1, 1)]
        public async Task UserExists_UserDoesNotExist_ReturnsFalse(int id, int numAccounts, string namePrefix, int startingId,
           int idIncrementFactor)
        {
            // Arrange
            // Setting up each dependency of ValidationService as a Mock
            Mock<IUserProfileService> mockProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> mockAccountService = new Mock<IUserAccountService>();

            List<WebUserAccountModel> userAccounts = new List<WebUserAccountModel>();

            // Test helper to assist in instantiating a list of WebUserAccountModels
            var testHelper = new BusinessLayerTestHelper();

            /* Parameters:
              * numAccounts: How many would you like to generate?
              * namePrefix: What would you like to be the common prefix before every attribute?
              * startingId: What number would you like id to start from?
              * idIncrementFactor: How many numbers would you like each subsequent id to skip by?
              */
            userAccounts = testHelper.PopulateListOfAccountModels(numAccounts, namePrefix, startingId, idIncrementFactor);

            // This function reads as: If GetAllUserAccounts is called, then return a list of userAccounts
            mockAccountService.Setup(x => x.GetAllUserAccounts()).Returns(Task.FromResult(userAccounts));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IValidationService validationService = new ValidationService(mockAccountService.Object, mockProfileService.Object);

            // Act
            // Call the function that is being tested
            var actualResult = await validationService.UserExists(id);

            // Assert
            // This unit test will pass if it returns false
            Assert.IsFalse(actualResult);
        }

        [DataTestMethod]
        [DataRow("TestUsername1", 20, "Test", 1, 1)]
        public async Task UsernameExists_UserNameDoesExist_ReturnsTrue(string expectedUsername, int numAccounts, string namePrefix, int startingId,
           int idIncrementFactor)
        {
            // Arrange
            // Setting up each dependency of ValidationService as a Mock
            Mock<IUserProfileService> mockProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> mockAccountService = new Mock<IUserAccountService>();

            // Create a container for WebUserAccountModel instances
            List<WebUserAccountModel> userAccounts = new List<WebUserAccountModel>();

            // Test helper to assist in instantiating a list of WebUserAccountModels
            var testHelper = new BusinessLayerTestHelper();

            /* Parameters:
             * numAccounts: How many would you like to generate?
             * namePrefix: What would you like to be the common prefix before every attribute?
             * startingId: What number would you like id to start from?
             * idIncrementFactor: How many numbers would you like each subsequent id to skip by?
             */
            userAccounts = testHelper.PopulateListOfAccountModels(numAccounts, namePrefix, startingId, idIncrementFactor);

            // This function reads as: If GetAllUserAccounts is called, then return a list of userAccounts
            mockAccountService.Setup(x => x.GetAllUserAccounts()).Returns(Task.FromResult(userAccounts));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IValidationService validationService = new ValidationService(mockAccountService.Object, mockProfileService.Object);

            // Act
            // Call the function that is being tested
            var actualResult = await validationService.UsernameExists(expectedUsername);

            // Assert
            // This unit test will pass if it returns true
            Assert.IsTrue(actualResult);
        }

        [DataTestMethod]
        [DataRow("TestUsername100", 20, "Test", 1, 1)]
        public async Task UsernameExists_UserNameDoesNotExist_ReturnsFalse(string expectedUsername, int numAccounts, string namePrefix, int startingId,
           int idIncrementFactor)
        {
            // Arrange
            // Setting up each dependency of ValidationService as a Mock
            Mock<IUserProfileService> mockProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> mockAccountService = new Mock<IUserAccountService>();

            // Create a container for WebUserAccountModel instances
            List<WebUserAccountModel> userAccounts = new List<WebUserAccountModel>();

            // Test helper to assist in instantiating a list of WebUserAccountModels
            var testHelper = new BusinessLayerTestHelper();

            /* Parameters:
             * numAccounts: How many would you like to generate?
             * namePrefix: What would you like to be the common prefix before every attribute?
             * startingId: What number would you like id to start from?
             * idIncrementFactor: How many numbers would you like each subsequent id to skip by?
             */
            userAccounts = testHelper.PopulateListOfAccountModels(numAccounts, namePrefix, startingId, idIncrementFactor);

            // This function reads as: If GetAllUserAccounts is called, then return a list of userAccounts
            mockAccountService.Setup(x => x.GetAllUserAccounts()).Returns(Task.FromResult(userAccounts));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IValidationService validationService = new ValidationService(mockAccountService.Object, mockProfileService.Object);

            // Act
            // Call the function that is being tested
            var actualResult = await validationService.UsernameExists(expectedUsername);

            // Assert
            // This unit test will pass if it returns false
            Assert.IsFalse(actualResult);
        }

        [DataTestMethod]
        [DataRow("TestEmailAddress1", 20, "Test", 1, 1)]
        public async Task EmailExists_EmailDoesExist_ReturnsTrue(string expectedEmailAddress, int numAccounts, string namePrefix, int startingId,
           int idIncrementFactor)
        {
            // Arrange
            // Setting up each dependency of ValidationService as a Mock
            Mock<IUserProfileService> mockProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> mockAccountService = new Mock<IUserAccountService>();

            // Create a container for WebUserAccountModel instances
            List<WebUserAccountModel> userAccounts = new List<WebUserAccountModel>();

            // Test helper to assist in instantiating a list of WebUserAccountModels
            var testHelper = new BusinessLayerTestHelper();

            /* Parameters:
             * numAccounts: How many would you like to generate?
             * namePrefix: What would you like to be the common prefix before every attribute?
             * startingId: What number would you like id to start from?
             * idIncrementFactor: How many numbers would you like each subsequent id to skip by?
             */
            userAccounts = testHelper.PopulateListOfAccountModels(numAccounts, namePrefix, startingId, idIncrementFactor);

            // This function reads as: If GetAllUserAccounts is called, then return a list of userAccounts
            mockAccountService.Setup(x => x.GetAllUserAccounts()).Returns(Task.FromResult(userAccounts));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IValidationService validationService = new ValidationService(mockAccountService.Object, mockProfileService.Object);

            // Act
            // Call the function that is being tested
            var actualResult = await validationService.EmailExists(expectedEmailAddress);

            // Assert
            // This unit test will pass if it returns true
            Assert.IsTrue(actualResult);
        }

        [DataTestMethod]
        [DataRow("TestEmailAddress100", 20, "Test", 1, 1)]
        public async Task EmailExists_EmailDoesNotExist_ReturnsFalse(string expectedEmailAddress, int numAccounts, string namePrefix, int startingId,
           int idIncrementFactor)
        {
            // Arrange
            // Setting up each dependency of ValidationService as a Mock
            Mock<IUserProfileService> mockProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> mockAccountService = new Mock<IUserAccountService>();

            // Create a container for WebUserAccountModel instances
            List<WebUserAccountModel> userAccounts = new List<WebUserAccountModel>();

            // Test helper to assist in instantiating a list of WebUserAccountModels
            var testHelper = new BusinessLayerTestHelper();

            /* Parameters:
             * numAccounts: How many would you like to generate?
             * namePrefix: What would you like to be the common prefix before every attribute?
             * startingId: What number would you like id to start from?
             * idIncrementFactor: How many numbers would you like each subsequent id to skip by?
             */
            userAccounts = testHelper.PopulateListOfAccountModels(numAccounts, namePrefix, startingId, idIncrementFactor);

            // This function reads as: If GetAllUserAccounts is called, then return a list of userAccounts
            mockAccountService.Setup(x => x.GetAllUserAccounts()).Returns(Task.FromResult(userAccounts));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IValidationService validationService = new ValidationService(mockAccountService.Object, mockProfileService.Object);

            // Act
            // Call the function that is being tested
            var actualResult = await validationService.EmailExists(expectedEmailAddress);

            // Assert
            // This unit test will pass if it returns false
            Assert.IsFalse(actualResult);
        }

        [DataTestMethod]
        [DataRow(1, AccountStatus.Active)]
        public async Task UserIsActive_AccountStatusIsActive_ReturnsTrue(int id, AccountStatus expectedAccountStatus)
        {
            // Arrange
            // Setting up each dependency of ValidationService as a Mock
            Mock<IUserProfileService> mockProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> mockAccountService = new Mock<IUserAccountService>();

            // Instantiate WebUserAccountModel and set AccountStatus to active
            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = id;
            webUserAccountModel.AccountStatus = expectedAccountStatus.ToString();

            // This function reads as: If GetUserAccount is called, then return webUserAccountModel
            mockAccountService.Setup(x => x.GetUserAccount(id)).Returns(Task.FromResult(webUserAccountModel));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IValidationService validationService = new ValidationService(mockAccountService.Object, mockProfileService.Object);

            // Act
            // Call the function that is being tested
            var actualResult = await validationService.UserIsActive(id);

            // Assert
            // This unit test will pass if it returns true
            Assert.IsTrue(actualResult);
        }

        [DataTestMethod]
        [DataRow(1, AccountStatus.Inactive)]
        [DataRow(2, AccountStatus.Disabled)]
        [DataRow(3, AccountStatus.Suspended)]
        [DataRow(4, AccountStatus.Banned)]
        [DataRow(5, AccountStatus.Deleted)]
        public async Task UserIsActive_AccountStatusIsNotActive_ReturnsFalse(int id, AccountStatus expectedAccountStatus)
        {
            // Arrange
            // Setting up each dependency of ValidationService as a Mock
            Mock<IUserProfileService> mockProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> mockAccountService = new Mock<IUserAccountService>();

            // Instantiate WebUserAccountModel and set AccountStatus to active
            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = id;
            webUserAccountModel.AccountStatus = expectedAccountStatus.ToString();

            // This function reads as: If GetUserAccount is called, then return webUserAccountModel
            mockAccountService.Setup(x => x.GetUserAccount(id)).Returns(Task.FromResult(webUserAccountModel));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IValidationService validationService = new ValidationService(mockAccountService.Object, mockProfileService.Object);

            // Act
            // Call the function that is being tested
            var actualResult = await validationService.UserIsActive(id);

            // Assert
            // This unit test will pass if it returns false
            Assert.IsFalse(actualResult);
        }
    }
}
