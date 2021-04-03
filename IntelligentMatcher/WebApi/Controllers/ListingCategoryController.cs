using BusinessModels.ListingModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraditionalListingSearch;

namespace IntelligentMatcherUI.Controllers
{
    public class ListingCategoryController: ControllerBase
    {

        private readonly IListingSearchManager _listingSearchManager;

        public ListingCategoryController(IListingSearchManager listingSearchManager)
        {
            _listingSearchManager = listingSearchManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<BusinessListingModel>>> GetAllCategories()
        {
            return (await _listingSearchManager.GetAllListings()).SuccessValue;
        }

    }
}
