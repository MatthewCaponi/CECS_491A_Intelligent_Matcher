using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Models;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class ModelConverterServiceTests
    {

        [DataTestMethod]
        [DataRow(1, "TestUserName1", "TestEmail1", AccountType.Admin, AccountStatus.Active)]
        [DataRow(2, "TestUserName2", "TestEmail2", AccountType.User, AccountStatus.Deleted)]
        [DataRow(3, "TestUserName3", "TestEmail3", AccountType.Moderator, AccountStatus.Banned)]
        public void ConvertTo_WebToUser_ConversionSuccessful(int expectedId, string expectedUsername, string expectedEmailAddress, 
            AccountType expectedAccountType, AccountStatus expectedAccountStatus)
        {
            // Arrange
            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = expectedUsername;
            webUserAccountModel.EmailAddress = expectedEmailAddress;
            webUserAccountModel.AccountType = expectedAccountType.ToString();
            webUserAccountModel.AccountStatus = expectedAccountStatus.ToString();
            webUserAccountModel.CreationDate = DateTimeOffset.UtcNow;
            webUserAccountModel.UpdationDate = DateTimeOffset.UtcNow;

            UserAccountModel userAccountModel = new UserAccountModel();

            // Act
            var convertedUserAccountModel = ModelConverterService.ConvertTo<WebUserAccountModel, UserAccountModel>(webUserAccountModel, userAccountModel);

            // Assert
            Assert.IsTrue(convertedUserAccountModel.GetType().Equals(typeof(UserAccountModel)));
        }

        [DataTestMethod]
        [DataRow(1, "TestUserName1", "TestEmail1", AccountType.Admin, AccountStatus.Active)]
        [DataRow(2, "TestUserName2", "TestEmail2", AccountType.User, AccountStatus.Deleted)]
        [DataRow(3, "TestUserName3", "TestEmail3", AccountType.Moderator, AccountStatus.Banned)]
        public void ConvertTo_WebAccountToUserAccount_ConvertedObjectHasAccurateData(int expectedId, string expectedUsername, string expectedEmailAddress,
            AccountType expectedAccountType, AccountStatus expectedAccountStatus)
        {
            // Arrange
            DateTimeOffset expectedDate = DateTimeOffset.UtcNow;
            WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
            webUserAccountModel.Id = expectedId;
            webUserAccountModel.Username = expectedUsername;
            webUserAccountModel.EmailAddress = expectedEmailAddress;
            webUserAccountModel.AccountType = expectedAccountType.ToString();
            webUserAccountModel.AccountStatus = expectedAccountStatus.ToString();
            webUserAccountModel.CreationDate = expectedDate;
            webUserAccountModel.UpdationDate = expectedDate;

            UserAccountModel userAccountModel = new UserAccountModel();

            // Act
            var convertedUserAccountModel = ModelConverterService.ConvertTo(webUserAccountModel, userAccountModel);

            // Assert
            Assert.IsTrue
                (
                    convertedUserAccountModel.Id == expectedId &&
                    convertedUserAccountModel.Username == expectedUsername &&
                    convertedUserAccountModel.Password == null &&
                    convertedUserAccountModel.Salt == null &&
                    convertedUserAccountModel.EmailAddress == expectedEmailAddress &&
                    convertedUserAccountModel.AccountType == expectedAccountType.ToString() &&
                    convertedUserAccountModel.AccountStatus == expectedAccountStatus.ToString() &&
                    convertedUserAccountModel.CreationDate == expectedDate &&
                    convertedUserAccountModel.UpdationDate == expectedDate
                );
        }

        [DataTestMethod]
        [DataRow(1, "TestFirstName1", "TestSurname1", 1)]
        [DataRow(2, "TestFirstName2", "TestSurname2", 2)]
        [DataRow(3, "TestFirstName3", "TestSurname3", 3)]
        public void ConvertTo_WebProfileToUserProfile_ConvertedObjectHasAccurateData(int expectedId, string expectedFirstName, string expectedSurname,
            int expectedUserAccountId)
        {
            // Arrange
            DateTimeOffset expectedDate = DateTimeOffset.UtcNow;
            WebUserProfileModel webUserProfileModel = new WebUserProfileModel();
            webUserProfileModel.Id = expectedId;
            webUserProfileModel.FirstName = expectedFirstName;
            webUserProfileModel.Surname = expectedSurname;
            webUserProfileModel.DateOfBirth = expectedDate;
            webUserProfileModel.UserAccountId = expectedUserAccountId;

            UserProfileModel userProfileModel = new UserProfileModel();

            // Act
            var convertedUserProfileModel = ModelConverterService.ConvertTo(webUserProfileModel, userProfileModel);

            // Assert
            Assert.IsTrue
                (
                    convertedUserProfileModel.Id == expectedId &&
                    convertedUserProfileModel.FirstName == expectedFirstName &&
                    convertedUserProfileModel.Surname == expectedSurname &&
                    convertedUserProfileModel.DateOfBirth == expectedDate &&
                    convertedUserProfileModel.UserAccountId == expectedUserAccountId
                );
        }
    }
}
