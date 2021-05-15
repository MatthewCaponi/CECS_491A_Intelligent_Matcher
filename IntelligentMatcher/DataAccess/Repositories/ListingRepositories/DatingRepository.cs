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

        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public DatingRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }
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
