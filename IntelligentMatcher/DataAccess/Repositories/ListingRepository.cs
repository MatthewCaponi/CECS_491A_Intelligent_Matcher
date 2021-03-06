using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    class ListingRepository : IListingRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public ListingRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public Task<int> CreateListing(UserProfileModel model,DALListingModel lmodel)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteListing(int id)
        {
            var query = "delete from [Listing] where Id = @Id";

            return await _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<DALListingModel> GetListing(int id)
        {
            var query = " select * from [Listing] where Id = @Id";
            var row = await _dataGateway.LoadData<DALListingModel, dynamic>(query,
                 new
                 {
                     Id = id
                 },
                 _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<DALListingModel> GetDetails(int id)
        {
            var query = " select details from [Listing] where Id = @Id";
            var row = await _dataGateway.LoadData<DALListingModel, dynamic>(query,
                 new
                 {
                     Id = id
                 },
                 _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<DALListingModel> GetTitle(int id)
        {
            var query = "select Title from [Listing] where Id= @Id";
            var row = await _dataGateway.LoadData<DALListingModel, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        // combine into one update.. UpdateListing(DALListingModel dalListingModel)... multiple queries.
        // UpdateCollaborationListing(CollaborationModel collaborationmodel) ...multiple queries
        // ditto for the rest. 
        public async Task<int> UpdateDetails(int id, string details)
        {
            var query = "update ListingDetails set details = @details where Id = @Id";

            return await _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id, Details=details
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateTitle(int id, string title)
        {
            var query = "update ListingTitle set title = @title where Id=@Id";

            return await _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             Title = title
                                         },
                                         _connectionString.SqlConnectionString) ;
        }
    }
}

