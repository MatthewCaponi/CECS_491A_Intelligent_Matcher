using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayerUnitTests.Services
{
    [TestClass]
    public class LoginAttemptsService
    {
        #region Unit Tests AddIpAddress
        [DataTestMethod]
        [DataRow("127.0.0.1")]
        public async Task AddIpAddress(string ipAddress)
        {
            // Arrange
            // Setting up each dependency of LoginAttemptsService as a Mock
            Mock<ILoginAttemptsRepository> mockLoginAttemptsRepository = new Mock<ILoginAttemptsRepository>();
        }
        #endregion
    }
}
