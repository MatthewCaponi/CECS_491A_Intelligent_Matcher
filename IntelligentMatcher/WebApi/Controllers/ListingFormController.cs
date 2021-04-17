using BusinessModels.ListingModels;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraditionalListings.Services;
using TraditionalListingSearch;

namespace IntelligentMatcherUI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ListingFormController : ControllerBase
    {
        private readonly IListingSearchManager _listingSearchManager;

        public ListingFormController()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway, connectionString);
            ListingGetterService listingGetterService = new ListingGetterService(traditionalListingSearchRepository);
            _listingSearchManager = new ListingSearchManager(listingGetterService);
        }

        [HttpGet]
        public async Task<ActionResult<List<BusinessListingModel>>> GetForm()
        {
            return (await _listingSearchManager.GetAllListings()).SuccessValue;
        }

    }
}
