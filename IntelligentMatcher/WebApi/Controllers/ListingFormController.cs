using AuthorizationPolicySystem;
using AuthorizationResolutionSystem;
using BusinessModels.ListingModels;
using ControllerModels.ListingModel;
using ControllerModels.RegistrationModels;
using IntelligentMatcher.UserManagement;
using Listings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ListingFormController : ApiBaseController
    {
        private readonly IListingManager _listingManager;
        private readonly IAuthorizationPolicyManager _authorizationPolicyManager;
        private readonly IAuthorizationResolutionManager _authorizationResolutionManager;

        public ListingFormController(IListingManager listingmanager, IAuthorizationPolicyManager authorizationPolicyManager, IAuthorizationResolutionManager authorizationResolutionManager)
        {
            _listingManager = listingmanager;
            _authorizationPolicyManager = authorizationPolicyManager;
            _authorizationResolutionManager = authorizationResolutionManager;
        }

        [HttpPost]
        public async Task<ActionResult<int>> FilloutForm([FromBody] ParentListingModel parentListingModel)
        {
                var token = ExtractHeader(HttpContext, "Authorization", ',', 1);
                var claims = new List<BusinessModels.UserAccessControl.UserClaimModel>();
                claims.Add(new BusinessModels.UserAccessControl.UserClaimModel("Id", parentListingModel.UserAccountId.ToString()));
                var accessPolicy = _authorizationPolicyManager.ConfigureCustomPolicy(new List<string>()
                    {
                        "listings:write"
                    }, claims);
                if (!_authorizationResolutionManager.Authorize(token, accessPolicy))
                {
                    return StatusCode(403);
                }

                var listingForm = new BusinessListingModel();
                listingForm.Title = parentListingModel.Title;
                listingForm.Details = parentListingModel.Details;
                listingForm.City = parentListingModel.City;
                listingForm.State = parentListingModel.State;
                listingForm.InPersonOrRemote = parentListingModel.InPersonOrRemote;
                listingForm.NumberOfParticipants = parentListingModel.NumberOfParticipants;
                listingForm.UserAccountId = parentListingModel.UserAccountId;
               
            try
            {
                var fillOutResult = await _listingManager.CreateListing(listingForm);
                return Ok(fillOutResult);
            }
            catch
            {
                return StatusCode(404);
            }
        }
    }
}
