using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ListingServices
{
    public interface IListingDeletionService
    {
        Task<bool> DeleteListing(int id);
    }
}