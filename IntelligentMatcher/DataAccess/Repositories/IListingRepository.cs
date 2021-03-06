using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IListingRepository
    {
       Task<int> CreateListing(UserProfileModel model, DALListingModel Lmodel); 
       Task<int> DeleteListing(int id);  
       Task<int> UpdateTitle(int id, string title);
       Task<int> UpdateDetails(int id,string details);
       Task<DALListingModel> GetListing(int id);
       Task<DALListingModel> GetDetails(int id);
       Task<DALListingModel> GetTitle(int id);




    }
}
