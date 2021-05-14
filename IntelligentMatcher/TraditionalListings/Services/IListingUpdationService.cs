using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings
{
    public interface IListingUpdationService
    {
        Task<int> UpdateListing(DALListingModel dalListingModel);
    }
}