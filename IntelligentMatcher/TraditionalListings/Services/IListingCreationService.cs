﻿using BusinessModels.ListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings
{
    public interface IListingCreationService
    {
        Task<int> CreateListing(BusinessListingModel businessListingmodel);
    }
}
