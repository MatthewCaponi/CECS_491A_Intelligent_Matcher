using BusinessModels.ListingModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraditionalListingSearch;
using DataAccess.Repositories;
using DataAccess;
using TraditionalListings.Services;

namespace IntelligentMatcherUI.Controllers
{
    public class ListingCategoryController: ControllerBase
    {

        private readonly IListingSearchManager _listingSearchManager;


        public ListingCategoryController()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway, connectionString);
            ListingGetterService listingGetterService= new ListingGetterService(traditionalListingSearchRepository);
            _listingSearchManager =new ListingSearchManager(listingGetterService);
        }

        [HttpGet]
        public async Task<ActionResult<List<BusinessListingModel>>> GetAllCategories()
        {
            return (await _listingSearchManager.GetAllListings()).SuccessValue;
        }

    }
}
