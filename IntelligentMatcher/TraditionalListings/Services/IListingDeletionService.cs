using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public interface  IListingDeletionService
    {
        Task<bool> DeleteListing(int id);
    }
}
