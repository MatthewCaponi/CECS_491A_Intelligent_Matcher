using System;
using BusinessModels;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using TraditionalListings.Models;

namespace TraditionalListings.Managers
{
    interface IListingsManager 

    {
   
        Task<Tuple<bool, ResultModel<int>>> EditListingTitle(int accountId, string update);
        Task<Tuple<bool, ResultModel<int>>> EditListingDetails(int accountId, string update);

        Task<Tuple<bool, ResultModel<int>>> DeleteListing(int accountId);
        Task<bool> CreateListing(WebUserProfileModel webUserProfileModel, CollaborationModel businessListingModels);
        Task<bool> GetTitle(string title);
        Task<Tuple<bool, ResultModel<int>>> GetDetails(int id);
        Task<bool> GetPresets();


    }
}
