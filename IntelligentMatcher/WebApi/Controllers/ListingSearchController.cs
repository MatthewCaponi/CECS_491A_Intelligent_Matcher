using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraditionalListingSearch;
using BusinessModels.ListingModels;
using TraditionalListings;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ListingSearchController : ControllerBase
    {
        private readonly IListingSearchManager _listingSearchManager;

        public ListingSearchController(IListingSearchManager listingSearchManager)
        {
            _listingSearchManager = listingSearchManager;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<BusinessListingModel>>> GetAllListings()
        {
            return (await _listingSearchManager.GetAllListings()).SuccessValue;
        }

    }


}
