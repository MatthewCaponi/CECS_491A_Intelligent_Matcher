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
        private const string PRIVATE_KEY_CONFIG_KEY = "PrivateKey";
        private const string PRIVATE_KEY_CONFIG_VALUE = "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";
        private const string PUBLIC_KEY_CONFIG_KEY = "PublicKey";
        private const string PUBLIC_KEY_CONFIG_VALUE = "MIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEApDmmhyrbhv8XoeDgQUE6 AUcRldYOtJqcgJ02qPMTYr/OyoRGcxf+J0LEyER+tHeRjkLzCANTUBpLxbRAze+M Io+fiwnhJ6q3qZwt2KNWDVoOGwxmkrfJFJRbUgg2+qhwh9BV8pHwmfh86GBcWkFh cRaBTu3huknQ4XzhRjlsunUyCZ5Q4CP3TaT6YPOGlNvlmuj6MkwThfli5Nzq5E+/ jB/PMF5qkiA+eztShbJPMmHcQd+lIsAHY3MqX3urkmbpmCdVKyrIWTd56TAbuWdl Tp9WKXXe0ZDq/wxD9cDFJFZd8ESCBnUslczupl3uZ7PLW6J6BR0Jyn8r36imfFmA B2Fc7lTv1dNqhJHja12qTk/63pDIkuIF5eibnI2sZtjNPbH4iisXwJFbcN3PCVI4 neUdmb9kg6AGAgj4P3yVBLXgUbQpcmHzGushVWr7W+AFvmUdXKCL5U11dvyPnKgf F4KXTRc5mRL3mIfg7kFm7/ZKeFJPONFUR1Zwfh+tphhQ3i7u2omB0hIv8aU5A6MU vHYCjqA6juSWZwoe8PY7RLVLWlUIB6+d4doIZqCazGJC06Ld7YSKbN0XlGBGm+IF GuMgWfG/ODaJqa46Qrz/9MB1eW7rMSnwwovQHAu2qe39HhLJUpYf+L9DsryVLkYt OMIeYsN3yk3y+293kk/sf7MCAwEAAQ==";
        private const string KEY_SIZE_CONFIG_KEY = "SecurityKeySettings:KeySize";
        private const string KEY_SIZE_CONFIG_VALUE = "3072";
        private static IConfiguration configuration;
        private static ITokenService tokenService;
        private static List<UserClaimModel> userClaims;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _testConfigKeys.Add(PRIVATE_KEY_CONFIG_KEY, PRIVATE_KEY_CONFIG_VALUE);
            _testConfigKeys.Add(PUBLIC_KEY_CONFIG_KEY, PUBLIC_KEY_CONFIG_VALUE);
            _testConfigKeys.Add(KEY_SIZE_CONFIG_KEY, KEY_SIZE_CONFIG_VALUE);

            configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(_testConfigKeys)
                            .Build();


            userClaims = new List<UserClaimModel>
            {
                new UserClaimModel("Username", "TestUsername1"),
                new UserClaimModel("EmailAddress", "TestEmailAddress1"),
                new UserClaimModel("FirstName", "TestFirstName1"),
                new UserClaimModel("LastName", "TestLastName1"),
                new UserClaimModel("Birthdate", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                new UserClaimModel("iss", "TestIssuer1"),
                new UserClaimModel("sub", "TestSubject1"),
                new UserClaimModel("aud", "TestAudience1"),
                new UserClaimModel("exp", "30"),
                new UserClaimModel("nbf", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")),
                new UserClaimModel("iat", DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
            };

             tokenService = new TokenService(configuration, new JwtTokenBuilderService(configuration));
        }
        #endregion
        #region Unit Tests
        [TestMethod]
        public void CreateToken_ValidInfo_InfoIsValid()
        {
            // Act
            var token = tokenService.CreateToken(userClaims);
            Debug.WriteLine(token);
            var tokenPieces = token.Split('.');

            // Assert
            Assert.IsTrue(tokenPieces.Length == 3);
        }

        [TestMethod]
        public void ValidateToken_TokenValid_ReturnsTrue()
        {
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
