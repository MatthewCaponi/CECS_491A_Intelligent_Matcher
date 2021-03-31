using BusinessModels;
using BusinessModels.ListingModels;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Services;

namespace TraditionalListingSearch
{
    public class SearchManager : ISearchManager
    {
        private ListingGetterService _listingGetterService;


        public SearchManager(ListingGetterService listingGetterService)
        {
            _listingGetterService = listingGetterService;
        }



        public async Task<Tuple<bool, ResultModel<List<BusinessListingModel>>>> GetAllListings()
        {
            ResultModel<List<BusinessListingModel>> resultModel = new ResultModel<List<BusinessListingModel>>();
            resultModel.Result = await _listingGetterService.GetAllListing();
            return new Tuple<bool, ResultModel<List<BusinessListingModel>>>(true, resultModel);
        }
    }
}
