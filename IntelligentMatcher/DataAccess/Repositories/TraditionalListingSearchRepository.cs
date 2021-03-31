using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TraditionalListingSearchRepository : ITraditionalListingSearchRepository
    {

        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public TraditionalListingSearchRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DALListingModel>> GetAllListings()
        {
            var query = "Select * from Listing";
            return await _dataGateway.LoadData<DALListingModel, dynamic>(query,
                                                                         new { },
                                                                         _connectionString.SqlConnectionString);
        }



        // Gold plating... sorting thats not in the brd 
        public async Task<IEnumerable<DALListingModel>> SortListingbyCity()
        {
            var query = "Select * from Listing ORDER BY [City] ASC";
            return await _dataGateway.LoadData<DALListingModel, dynamic>(query,
                                                                         new { },
                                                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<DALListingModel>> SortListingbyState()
        {
            var query = "Select * from Listing ORDER BY [State] ASC";
            return await _dataGateway.LoadData<DALListingModel, dynamic>(query,
                                                                         new { },
                                                                         _connectionString.SqlConnectionString);
        }
    }
}
