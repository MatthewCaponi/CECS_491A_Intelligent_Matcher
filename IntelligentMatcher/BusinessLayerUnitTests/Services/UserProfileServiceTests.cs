using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class UserProfileServiceTests
    {
        #region Unit Tests GetUserProfileByAccountId
        [DataTestMethod]
        [DataRow(1, "TestFirstName1", "TestSurname1", "3/28/2007 7:13:50 PM +00:00", 1)]
        public async Task GetUserProfileByAccountId_UserProfileFound_ReturnsWebUserProfileModel(int profileId,
            string firstName, string surname, string dateOfBirth, int accountId)
        {
            // Arrange
            // Setting up each dependency of UserProfileService as a Mock
            Mock<IUserProfileRepository> mockUserProfileRepository = new Mock<IUserProfileRepository>();

            var userProfile = new UserProfileModel();
            var expectedResult = new WebUserProfileModel();

            userProfile.Id = profileId;
            userProfile.FirstName = firstName;
            userProfile.Surname = surname;
            userProfile.DateOfBirth = DateTimeOffset.Parse(dateOfBirth);
            userProfile.UserAccountId = accountId;

            expectedResult.Id = profileId;
            expectedResult.FirstName = firstName;
            expectedResult.Surname = surname;
            expectedResult.DateOfBirth = DateTimeOffset.Parse(dateOfBirth); ;
            expectedResult.UserAccountId = accountId;

            // This function reads as: If GetAccountByUsername is called, then return a list of userAccounts
            mockUserProfileRepository.Setup(x => x.GetUserProfileByAccountId(accountId)).Returns(Task.FromResult(userProfile));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IUserProfileService userProfileService = new UserProfileService(mockUserProfileRepository.Object);

            // Arrange
            var actualResult = await userProfileService.GetUserProfileByAccountId(accountId);

            // Act
            Assert.IsTrue
                (
                    actualResult.Id == expectedResult.Id &&
                    actualResult.FirstName == expectedResult.FirstName &&
                    actualResult.Surname == expectedResult.Surname &&
                    actualResult.DateOfBirth == expectedResult.DateOfBirth &&
                    actualResult.UserAccountId == expectedResult.UserAccountId
                );
        }

        [DataTestMethod]
        [DataRow(2)]
        public async Task GetUserProfileByAccountId_UserProfileNotFound_ReturnsNull(int givenAccountId)
        {
            // Arrange
            // Setting up each dependency of UserAccountService as a Mock
            Mock<IUserProfileRepository> mockUserProfileRepository = new Mock<IUserProfileRepository>();

            // Sets UserAccount to null
            UserProfileModel userProfile = null;

            // This function reads as: If GetAccountByEmail is called, then return a null UserAccountModel
            mockUserProfileRepository.Setup(x => x.GetUserProfileByAccountId(givenAccountId))
                .Returns(Task.FromResult(userProfile));

            // Finally, instantiate the actual class being tested and pass in the mock objects
            IUserProfileService userProfileService = new UserProfileService(mockUserProfileRepository.Object);

            // Arrange
            var actualResult = await userProfileService.GetUserProfileByAccountId(givenAccountId);

            // Act
            Assert.IsNull(actualResult);
        }
        #endregion
    }
}
