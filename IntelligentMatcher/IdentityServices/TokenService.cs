using BusinessModels.UserAccessControl;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenBuilderService _tokenBuilderService;

        public TokenService(IConfiguration configuration, ITokenBuilderService tokenBuilderService)
        {
            _configuration = configuration;
            _tokenBuilderService = tokenBuilderService;
        }
        public string CreateToken(List<UserClaimModel> userClaims)
        {
            var jwtPayloadModel = new JwtPayloadModel();

            foreach (var userClaim in userClaims)
            {
                switch (userClaim.Type)
                {
                    case "iss":
                        jwtPayloadModel.Issuer = new UserClaimModel("iss", userClaim.Value);
                        break;

                    case "sub":
                        jwtPayloadModel.Subject = new UserClaimModel("sub", userClaim.Value);
                        break;

                    case "aud":
                        jwtPayloadModel.Audience = new UserClaimModel("aud", userClaim.Value);
                        break;

                    case "exp":
                        jwtPayloadModel.ExpirationTime = new UserClaimModel("exp", userClaim.Value);
                        break;

                    case "nbf":
                        jwtPayloadModel.NotBefore = new UserClaimModel("nbf", userClaim.Value);
                        break;

                    case "iat":
                        jwtPayloadModel.IssuedAt = new UserClaimModel("iat", userClaim.Value);
                        break;

                    default:
                        {
                            jwtPayloadModel.PublicClaims.Add(userClaim);
                        }
                        break;
                }
            }

            var token = _tokenBuilderService.CreateToken(jwtPayloadModel);

            return token;
        }

        public bool ValidateToken(string token)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var publicKeyEncrypted = _configuration["PublicKey"];
            var publicKey = Convert.FromBase64String(publicKeyEncrypted);
            var keySize = int.Parse(_configuration["SecurityKeySettings:KeySize"]);

            using RSA rsa = RSA.Create(keySize);
            rsa.ImportSubjectPublicKeyInfo(publicKey, out _);

            try
            {
                tokenhandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    CryptoProviderFactory = new CryptoProviderFactory()
                    {
                        CacheSignatureProviders = false
                    },
                    ClockSkew = TimeSpan.FromSeconds(5)
                }, out var validatedToken);

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private JwtSecurityToken DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            return jwtToken;
        }

        public List<UserClaimModel> ExtractClaims(string token)
        {

        }
    }
}
