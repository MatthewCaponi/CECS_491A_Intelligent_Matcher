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
    public class IdTokenService : ITokenService
    {
        public string CreateToken()
        {
            return String.Empty;
        }
    }
}
