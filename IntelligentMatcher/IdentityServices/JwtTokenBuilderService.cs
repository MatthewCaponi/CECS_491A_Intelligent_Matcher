using BusinessModels.UserAccessControl;
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
        public string CreateToken(JwtPayloadModel jwtPayloadModel, string secret, RsaSecurityKey key)
        {
            var claims = new List<Claim>();
            foreach (var userClaim in jwtPayloadModel.PublicClaims)
            {
                claims.Add(new Claim(userClaim.Key, userClaim.Value));
            }

            //var key = new RsaSecurityKey(RSA.Create(2048));

            var tokenHandler = new JsonWebTokenHandler();
            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtPayloadModel.Issuer.Value,
                Audience = jwtPayloadModel.Audience.Value,
                IssuedAt = now,
                NotBefore = now,
                Expires = now.AddMinutes(Double.Parse(jwtPayloadModel.ExpirationTime.Value)),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSsaPssSha256)
            };

            string finalToken = tokenHandler.CreateToken(tokenDescriptor);

            return finalToken;
        }
    }
}
