using BusinessModels;
using BusinessModels.ListingModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TraditionalListingSearch
{
    public interface ISearchManager
    {
        Task<Tuple<bool, ResultModel<List<BusinessListingModel>>>> GetAllListings();
    }
}
