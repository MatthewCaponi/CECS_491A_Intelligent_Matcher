using BusinessModels.ListingModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraditionalListingSearch;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ListingFormController : ControllerBase
    {
        private readonly IListingSearchManager _listingSearchManager;

        public ListingFormController(IListingSearchManager listingSearchManager)
        {
            _listingSearchManager = listingSearchManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<BusinessListingModel>>> GetForm()
        {
            return (await _listingSearchManager.GetAllListings()).SuccessValue;
        }

    }
}
