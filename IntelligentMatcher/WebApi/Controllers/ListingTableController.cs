using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ListingTableController(IListingManager listingmanager,IListingSearchManager listingSearchManager)
        {
            _listingManager = listingmanager;
            _listingSearchManager = listingSearchManager;
        }


        [HttpGet]
        public async Task<ActionResult<List<BusinessListingModel>>> GetAllParentListing()
        {
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
