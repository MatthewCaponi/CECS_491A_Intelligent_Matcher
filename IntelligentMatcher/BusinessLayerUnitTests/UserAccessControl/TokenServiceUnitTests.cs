using BusinessModels.UserAccessControl;
using IdentityServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayerUnitTests.UserAccessControl.UnitTests
{
    [TestClass]
    public class TokenServiceUnitTests
    {
        #region Unit Tests
        [DataTestMethod]
        [DataRow("TestSecret1", 2048, "TestKey", "TestValue", 10, "iss", "sub", "aud", "exp", "nbf", "iat")]
        public void CreateToken_ValidInfo_InfoIsValid(string secret, int keySize, string key, string value, int numClaims,
            string issuer, string subject, string audience, string expiration, string notBefore, string issuedAt)
        {
            // Arrange
            var testConfigSettings = new Dictionary<string, string>
            {
                { "TestSecret", "SuperSecretTestKey"},
                { "SecurityKeySettings:KeySize", "2048" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(testConfigSettings)
                .Build();

            List<UserClaimModel> userClaims = new List<UserClaimModel>();

            ITokenBuilderService tokenBuilderService = new JwtTokenBuilderService();
            ITokenService tokenService = new TokenService(configuration, tokenBuilderService);

            for (int i = 0; i < numClaims; ++i)
            {
                userClaims.Add(new UserClaimModel((key + i), (value + i)));
            }

            userClaims.Add(new UserClaimModel(issuer, value));
            userClaims.Add(new UserClaimModel(subject, value));
            userClaims.Add(new UserClaimModel(audience, value));
            userClaims.Add(new UserClaimModel(expiration, value));
            userClaims.Add(new UserClaimModel(notBefore, value));
            userClaims.Add(new UserClaimModel(issuedAt, value));

            // Act
            var token = tokenService.CreateToken(userClaims);
            Debug.WriteLine(token);
            var tokenPieces = token.Split('.');

            // Assert
            Assert.IsTrue(tokenPieces.Length == 3);
        }
        #endregion
    }
}
