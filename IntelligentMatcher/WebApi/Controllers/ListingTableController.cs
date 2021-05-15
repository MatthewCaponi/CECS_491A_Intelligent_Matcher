using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationPolicySystem;
using AuthorizationResolutionSystem;
using BusinessModels.ListingModels;
using Listings;
using ListingSearch;
using Microsoft.AspNetCore.Mvc;
using UserAnalysisManager;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ListingTableController : ApiBaseController
    {
        private readonly IListingManager _listingManager;
        private readonly IListingSearchManager _listingSearchManager;
        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;

        public ListingTableController(IListingManager listingmanager,IListingSearchManager listingSearchManager, 
            IAuthorizationPolicyManager authorizationPolicyManager, IAuthorizationResolutionManager authorizationResolutionManager)
        {
            _listingManager = listingmanager;
            _listingSearchManager = listingSearchManager;
            _authorizationPolicyManager = authorizationPolicyManager;
            _authorizationResolutionManager = authorizationResolutionManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<BusinessListingModel>>> GetAllParentListing()
        {
            var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
            var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
            var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "listings:read"
                    }, claims);
            if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
            {
                return StatusCode(403);
            }
            try
            {
                return Ok((await _listingSearchManager.GetAllListings()).SuccessValue);
            }
            catch
            {
                return StatusCode(404);
            }           
        }
    }
}
