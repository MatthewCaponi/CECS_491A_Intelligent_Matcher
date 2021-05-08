using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string storedProcedure = "dbo.ListingSearch_GetAllListings";
            return await _dataGateway.LoadData<DALListingModel, dynamic>(storedProcedure,
                                                                         new { },
                                                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<DALListingModel>> GetAllListingsByUserId(int userId)
        { 
        
            string storedProcedure = "dbo.ListingSearch_GetAllListingsByUserId";
            return await _dataGateway.LoadData<DALListingModel, dynamic>(storedProcedure,
                                                                         new 
                                                                         { 
                                                                            UserId = userId
    },
                                                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<DALCollaborationModel>> GetAllCollaborationListings()
        {
            string storedProcedure = "dbo.ListingSearch_GetAllCollaborationListings";
            return await _dataGateway.LoadData<DALCollaborationModel, dynamic>(storedProcedure,
                                                                               new { },
                                                                               _connectionString.SqlConnectionString);

        }

        public async Task<IEnumerable<DALRelationshipModel>> GetAllRelationshipListings()
        {
            string storedProcedure = "dbo.ListingSearch_GetAllRelationshipListing";
            return await _dataGateway.LoadData<DALRelationshipModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);
        }


        public async Task<IEnumerable<DALRelationshipModel>> GetAllTeamListings()
        {
            string storedProcedure = "dbo.ListingSearch_GetAllTeamListing";
            return await _dataGateway.LoadData<DALRelationshipModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<DALDatingModel>> GetAllDatingListings()
        {
            string storedProcedure = "dbo.ListingSearch_GetAllTeamListing";
            return await _dataGateway.LoadData<DALDatingModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);
        }

        public async Task<DALListingModel> GetAllListingsById(int id)
        {
            
            string storedProcedure = "dbo.GetListingById";

            var row = await _dataGateway.LoadData<DALDatingModel, dynamic>(storedProcedure,
                                                                             new  {Id=id },
                                                                             _connectionString.SqlConnectionString);
            return row.FirstOrDefault();
        }
    }
}
