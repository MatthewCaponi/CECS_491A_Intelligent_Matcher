
using BusinessModels.ListingModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraditionalListingSearch;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TraditionalListingTableController : Controller
    {
        private readonly IListingSearchManager _listingSearchManager;

        public TraditionalListingTableController(IListingSearchManager listingSearchManager)
        {
            _listingSearchManager= listingSearchManager;
        }

        [HttpGet]
      
        public async Task<ActionResult<List<BusinessListingModel>>> GetAllParentListing()
        {
            return (await _listingSearchManager.GetAllListings()).SuccessValue;
        }
    }
}
