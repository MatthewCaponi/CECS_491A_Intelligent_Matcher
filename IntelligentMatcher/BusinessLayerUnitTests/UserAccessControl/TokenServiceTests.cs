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
    public class TokenServiceTests
    {
        #region Setup
        private static IDictionary<string, string> _testConfigKeys = new Dictionary<string, string>();
        private const string PRIVATE_KEY_CONFIG = "PrivateKey";
        private const string PUBLIC_KEY_CONFIG = "PublicKey";
        private const string KEY_SIZE_CONFIG = "SecurityKeySettings:KeySize";
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _testConfigKeys.Add({PrivateKey });
        }
        #endregion
        #region Unit Tests
        [DataTestMethod]
        [DataRow("TestKey", "TestValue", 10, "iss", "sub", "aud", "exp", "nbf", "iat",
            "30")]
        public void CreateToken_ValidInfo_InfoIsValid( string key, string value, 
            int numClaims, string issuer, string subject, string audience, 
            string expiration, string notBefore, string issuedAt, string expirationMinutes)
        {
            // Arrange
            var now = DateTime.UtcNow.ToString();
            var testConfigSettings = new Dictionary<string, string>
            {
               { secretKey, secret},
                { keySizeKey, keySize }
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
            userClaims.Add(new UserClaimModel(expiration, expirationMinutes));
            userClaims.Add(new UserClaimModel(notBefore, now));
            userClaims.Add(new UserClaimModel(issuedAt, now));

            // Act
            var token = tokenService.CreateToken(userClaims);
            Debug.WriteLine(token);
            var tokenPieces = token.Split('.');

            // Assert
            Assert.IsTrue(tokenPieces.Length == 3);
        }

        [DataTestMethod]
        [DataRow("TestSecret", "SecurityKeySettings:KeySize", "SuperSecretTestKey", "2048", "TestKey", "TestValue", 
            10, "iss", "sub", "aud", "exp", "nbf", "iat",
            "30")]
        public void ValidateToken_TokenValid_ReturnsTrue(string secretKey, string keySizeKey, string secret, string keySize, string key, string value, int numClaims,
            string issuer, string subject, string audience, string expiration, string notBefore, string issuedAt, string expirationMinutes)
        {
            // Arrange
            var now = DateTime.UtcNow.ToString();
            var testConfigSettings = new Dictionary<string, string>
            {
                { secretKey, secret},
                { keySizeKey, keySize }
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
            userClaims.Add(new UserClaimModel(expiration, expirationMinutes));
            userClaims.Add(new UserClaimModel(notBefore, now));
            userClaims.Add(new UserClaimModel(issuedAt, now));

            // Act
            var token = tokenService.CreateToken(userClaims);
            Debug.WriteLine(token);
            var isValid = tokenService.ValidateToken(token);

            // Assert
            Assert.IsTrue(isValid);
        }
        #endregion
    }
}
