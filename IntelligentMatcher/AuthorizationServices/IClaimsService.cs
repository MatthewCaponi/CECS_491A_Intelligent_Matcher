using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServices
{
    public interface IClaimsService
    {
        Task<List<BusinessModels.UserAccessControl.ClaimModel>> GetAllClaims();
        Task<List<BusinessModels.UserAccessControl.UserClaimModel>> GetAllUserClaims();
        Task<BusinessModels.UserAccessControl.ClaimModel> GetClaim(int id);
        Task<int> CreateClaim(BusinessModels.UserAccessControl.ClaimModel claimModel);
        Task<bool> UpdateClaim(BusinessModels.UserAccessControl.ClaimModel claimModel);
        Task<bool> DeleteClaim(int id);
    }
}
