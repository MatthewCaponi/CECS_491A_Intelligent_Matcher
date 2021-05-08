using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels.UserAccessControl
{
    public class JwtPayloadModel
    {
        public List<UserClaimModel> PublicClaims { get; set; }
        public UserClaimModel Issuer { get; set; }
        public UserClaimModel Subject { get; set; }
        public UserClaimModel Audience { get; set; }
        public UserClaimModel ExpirationTime { get; set; }
        public UserClaimModel NotBefore { get; set; }
        public UserClaimModel IssuedAt { get; set; }

        public JwtPayloadModel()
        {
            {
                PublicClaims = new List<UserClaimModel>();
            }
        }
    }
}
