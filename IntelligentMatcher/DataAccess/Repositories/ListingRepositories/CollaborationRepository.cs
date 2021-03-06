using Dapper;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Services;

namespace DataAccess.Repositories.ListingRepositories
{
    public class CollaborationRepository : ICollaborationRepository
    {

        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public CollaborationRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> CreateListing(DALCollaborationModel dalCollaborationModel)
        {

            var query = "insert into [Collaboration]([CollaborationType],[InvolvementType],[Experience],[ListingId])" +
                "values (@CollaborationType,@InvolvementType, @Experience,@ListingId); set @Id = SCOPE_IDENTITY();";

            DynamicParameters p = new DynamicParameters();
            p.Add("CollaborationType", dalCollaborationModel.Title);
            p.Add("InvolvementType", dalCollaborationModel.Details);
            p.Add("Experience", dalCollaborationModel.City);
            p.Add("ListingId", dalCollaborationModel.ListingId);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");



        }

        public async Task<DALCollaborationModel> GetListing(int id)
        {
            var query = "Select * from Collaboration where Id = @Id";

            var row = await _dataGateway.LoadData<DALCollaborationModel, dynamic>(query,
                new
                {
                    ID = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public Task<int> UpdateListing(DALListingModel dalListingModel)
        {
            throw new NotImplementedException();
        }
    }

        
    
}
