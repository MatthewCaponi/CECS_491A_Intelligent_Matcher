using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Services;

namespace DataAccess.Repositories.ListingRepositories
{
    public class DatingRepository : IDatingRepository
    {
        public Task<int> CreateListing(DALDatingModel dalDatingModel)
        {
            throw new NotImplementedException();
        }

        public Task<DALDatingModel> GetListing(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateListing(DALDatingModel dalDatingModel)
        {
            throw new NotImplementedException();
        }
    }
}
