using BusinessModels;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class UserAccountCodeServiceTests
    {
        #region Unit Tests GetUserAccountCodeByAccountId
        [DataTestMethod]
        [DataRow(1, "ABC1", "3/28/2007 7:13:50 PM +00:00", 1)]
        public async Task GetUserAccountCodeByAccountId_AccountIdFound_ReturnBusinessUserAccountCodeModel(int id, string code,
            string expirationTime, int accountId)
        {
            // Arrange
            // Setting up each dependency of LoginAttemptsService as a Mock
            Mock<IUserAccountCodeRepository> mockUserAccountCodeRepository = new Mock<IUserAccountCodeRepository>();

            var userAccountCodeModel = new UserAccountCodeModel();

            userAccountCodeModel.Id = id;
            userAccountCodeModel.Code = code;
            userAccountCodeModel.ExpirationTime = DateTimeOffset.Parse(expirationTime);
            userAccountCodeModel.UserAccountId = accountId;

            var expectedResult = new BusinessUserAccountCodeModel();

            expectedResult.Id = id;
            expectedResult.Code = code;
            expectedResult.ExpirationTime = DateTimeOffset.Parse(expirationTime);
            expectedResult.UserAccountId = accountId;

            mockUserAccountCodeRepository.Setup(x => x.GetUserAccountCodeByAccountId(accountId)).Returns
                (Task.FromResult(userAccountCodeModel));

            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(mockUserAccountCodeRepository.Object);

            // Act
            var actualResult = await userAccountCodeService.GetUserAccountCodeByAccountId(accountId);

            // Assert
            Assert.IsTrue
                (
                    actualResult.Id == expectedResult.Id &&
                    actualResult.Code == expectedResult.Code &&
                    actualResult.ExpirationTime == expectedResult.ExpirationTime &&
                    actualResult.UserAccountId == expectedResult.UserAccountId
                );
        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task GetUserAccountCodeByAccountId_AccountIdNotFound_ReturnNull(int accountId)
        {
            // Arrange
            // Setting up each dependency of LoginAttemptsService as a Mock
            Mock<IUserAccountCodeRepository> mockUserAccountCodeRepository = new Mock<IUserAccountCodeRepository>();

            UserAccountCodeModel userAccountCodeModel = null;

            mockUserAccountCodeRepository.Setup(x => x.GetUserAccountCodeByAccountId(accountId)).Returns
                (Task.FromResult(userAccountCodeModel));

            IUserAccountCodeService userAccountCodeService = new UserAccountCodeService(mockUserAccountCodeRepository.Object);

            // Act
            var actualResult = await userAccountCodeService.GetUserAccountCodeByAccountId(accountId);

            // Assert
            Assert.IsNull(actualResult);
        }
        #endregion
    }
}
