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


        // doesnt like the star change it. 
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
            string storedProcedure = "dbo.ListingSearch_GetAllCollaborationListing";
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


        // Gold plating... sorting thats not in the brd....... //Comment out or delete if you run out of time.
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
