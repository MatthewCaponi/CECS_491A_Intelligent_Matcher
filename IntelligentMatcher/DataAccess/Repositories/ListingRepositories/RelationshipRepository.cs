using Dapper;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Services;

namespace DataAccess.Repositories.ListingRepositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public RelationshipRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }
        public async Task<int> CreateListing(DALRelationshipModel dalRelationshipModel)
        {

            var query = "insert into [Relationship]([RelationshipType],[Age],[Interests],[GenderPreference],[ListingId])" +
                "values (@RelationshipType,@Age, @Interests,@GenderPreference,@ListingId); set @Id = SCOPE_IDENTITY();";

            DynamicParameters p = new DynamicParameters();
            p.Add("RelationshipType", dalRelationshipModel.Title);
            p.Add("Age", dalRelationshipModel.Age);
            p.Add("Interests", dalRelationshipModel.City);
            p.Add("GenderPreference", dalRelationshipModel.State);
            p.Add("ListingId", dalRelationshipModel.ListingId);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");



        }

        public Task<DALRelationshipModel> GetListing(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateListing(DALRelationshipModel dalRelationshipModell)
        {
            throw new NotImplementedException();
        }
    }
}
