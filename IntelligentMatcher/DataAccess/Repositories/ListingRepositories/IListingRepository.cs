using Models;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ListingRepositories
{
    public interface IListingRepository
    {
       Task<int> CreateListing(DALListingModel dalListingModel); 
       Task<int> DeleteListing(int id);
       Task<int> UpdateListing(DALListingModel dalListingModel);
      
        
           
    }
}
