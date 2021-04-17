using System;
using BusinessModels;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using BusinessModels.ListingModels;

namespace TraditionalListings.Managers
{
    public interface IListingsManager 

    {
   
        Task<Tuple<bool, ResultModel<int>>> EditListing(BusinessListingModel businessListingModel);
        Task<Tuple<bool, ResultModel<int>>> DeleteListing(int Id);
        Task<bool> CreateListing(WebUserProfileModel webUserProfileModel, BusinessListingModel businessListingModels);
        Task<bool> GetListing(int Id);
        Task<bool> UpdateListing(BusinessListingModel businessListingModel);
        
       


    }
}
