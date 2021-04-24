using IdentityServices;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayerUnitTests.UserAccessControl.UnitTests
{
    [TestClass]
    public class TokenServiceUnitTests
    {
        private readonly Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();
        private readonly Mock<ITokenBuilderService> tokenBuilderServiceMock = new Mock<ITokenBuilderService>();

        #region Unit Tests
        [DataTestMethod]
        [DataRow(1, "TestResource1")]
        public void CreateToken_ValidInfo_InfoIsValid(int id, string name)
        {


        }
        #endregion
    }
}
