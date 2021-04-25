using BusinessModels.UserAccessControl;
using IdentityServices;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayerUnitTests.UserAccessControl
{
    //Taking out the test until I get back to this
    //[TestClass]
    public class JwtTokenBuilderServiceTests
    {
        ITokenBuilderService tokenBuilderService = new JwtTokenBuilderService();
        public static List<UserClaimModel> publicClaimsTest = new List<UserClaimModel>
        {
            new UserClaimModel("Username", "TestUsername1"),
            new UserClaimModel("EmailAddress", "TestEmailAddress1")
        };

        public static JwtPayloadModel jwtPayloadModel = new JwtPayloadModel
        {
            PublicClaims = publicClaimsTest,
            Issuer = new UserClaimModel("iss", "TestIssuer1"),
            Subject = new UserClaimModel("sub", "TestSubject1"),
            Audience = new UserClaimModel("aud", "TestAudience1"),
            ExpirationTime = new UserClaimModel("exp", DateTime.UtcNow.AddMinutes(30).ToString()),
            NotBefore = new UserClaimModel("nbf", "TestNotBefore1"),
            IssuedAt = new UserClaimModel("iat", "TestIssuedAt1")
        };

        [DataTestMethod]
        [DataRow("TestSecret1")]
        public void CreateToken_InformationValid_OutputsString(string expectedSecret)
        {
            // Arrange
            var securityKey = new RsaSecurityKey(RSA.Create(2048));
            // Act
            string actualJwt = tokenBuilderService.CreateToken(jwtPayloadModel, expectedSecret, securityKey);

            Debug.WriteLine(actualJwt);

            // Assert
            Assert.IsTrue(actualJwt.GetType() == typeof(string) && !String.IsNullOrEmpty(actualJwt));
        }

        [DataTestMethod]
        [DataRow("TestSecret1")]
        public void CreateToken_InformationValid_TokenValid(string expectedSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new RsaSecurityKey(RSA.Create(2048));
            string actualJwt = tokenBuilderService.CreateToken(jwtPayloadModel, expectedSecret, securityKey);
            

            try
            {
                tokenHandler.ValidateToken(actualJwt, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                Assert.IsTrue(false);
            }

            Assert.IsTrue(true);
            
        }
    }
}
