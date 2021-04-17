using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraditionalListingSearch;
using BusinessModels.ListingModels;
using TraditionalListings;
using DataAccess.Repositories;
using DataAccess;
using TraditionalListings.Services;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ListingSearchController : ControllerBase
    {
        private readonly IListingSearchManager _listingSearchManager;

        public ListingSearchController()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway, connectionString);
            ListingGetterService listingGetterService = new ListingGetterService(traditionalListingSearchRepository);
            _listingSearchManager = new ListingSearchManager(listingGetterService);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<BusinessListingModel>>> GetAllListings()
        {
            return (await _listingSearchManager.GetAllListings()).SuccessValue;
        }

    }


}
