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
            var secret = _configuration["TestSecret"];
            var keySize = int.Parse(_configuration["SecurityKeySettings:KeySize"]);
            var securityKey = new RsaSecurityKey(RSA.Create(keySize));
            var jwtPayloadModel = new JwtPayloadModel();

            foreach (var userClaim in userClaims)
            {
                switch (userClaim.Key)
                {
                    case "Issuer":
                        jwtPayloadModel.Issuer.Key = "iss";
                        jwtPayloadModel.Issuer.Value = userClaim.Value;
                        break;

                    case "Subject":
                        jwtPayloadModel.Subject.Key = "sub";
                        jwtPayloadModel.Audience.Value = userClaim.Value;
                        break;

                    case "Audience":
                        jwtPayloadModel.Audience.Key = "aud";
                        jwtPayloadModel.Issuer.Value = userClaim.Value;
                        break;

                    case "ExpirationTime":
                        jwtPayloadModel.ExpirationTime.Key = "exp";
                        jwtPayloadModel.Issuer.Value = userClaim.Value;
                        break;

                    case "NotBefore":
                        jwtPayloadModel.NotBefore.Key = "nbf";
                        jwtPayloadModel.Issuer.Value = userClaim.Value;
                        break;

                    case "IssuedAt":
                        jwtPayloadModel.IssuedAt.Key = "iat";
                        jwtPayloadModel.Issuer.Value = userClaim.Value;
                        break;

                    default:
                        {
                            jwtPayloadModel.PublicClaims.Add(userClaim);
                        }
                        break;
                }
            }

            var token = _tokenBuilderService.CreateToken(jwtPayloadModel, secret, securityKey);

            return token;
        }
    }
}
