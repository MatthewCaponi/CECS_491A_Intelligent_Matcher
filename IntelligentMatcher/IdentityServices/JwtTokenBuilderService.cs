using BusinessModels.UserAccessControl;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityServices
{
    public class JwtTokenBuilderService : ITokenBuilderService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenBuilderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(JwtPayloadModel jwtPayloadModel)
        {
            var privateKeyEncrypted = _configuration["PrivateKey"];
            var privateKey = Convert.FromBase64String(privateKeyEncrypted);

            var keySize = int.Parse(_configuration["SecurityKeySettings:KeySize"]);

            using RSA rsa = RSA.Create(keySize);
            rsa.ImportRSAPrivateKey(privateKey, out _);
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };

            var claims = new List<Claim>();
            foreach (var userClaim in jwtPayloadModel.PublicClaims)
            {
                claims.Add(new Claim(userClaim.Key, userClaim.Value));
            }

            var tokenHandler = new JsonWebTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtPayloadModel.Issuer.Value,
                Audience = jwtPayloadModel.Audience.Value,
                IssuedAt = DateTime.Parse(jwtPayloadModel.IssuedAt.Value).ToUniversalTime(),
                NotBefore = DateTime.Parse(jwtPayloadModel.NotBefore.Value).ToUniversalTime(),
                Expires = DateTime.UtcNow.AddMinutes(Double.Parse(jwtPayloadModel.ExpirationTime.Value)).ToUniversalTime(),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials,
            };

            string finalToken = tokenHandler.CreateToken(tokenDescriptor);

            return finalToken;
        }
    }
}
