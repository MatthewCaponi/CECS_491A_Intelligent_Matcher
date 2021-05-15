using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class JwtPayloadModel
    {
        public List<TokenClaimModel> PublicClaims { get; set; }
        public TokenClaimModel Issuer { get; set; }
        public TokenClaimModel Subject { get; set; }
        public TokenClaimModel Audience { get; set; }
        public TokenClaimModel ExpirationTime { get; set; }
        public TokenClaimModel NotBefore { get; set; }
        public TokenClaimModel IssuedAt { get; set; }

        public JwtPayloadModel()
        {
            {
                PublicClaims = new List<TokenClaimModel>();
            }
        }
    }
}
