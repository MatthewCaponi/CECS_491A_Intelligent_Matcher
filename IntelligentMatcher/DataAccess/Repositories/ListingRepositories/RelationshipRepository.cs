using Dapper;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


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

            string storedProcedure = "dbo.CreateRelationshipListing";

            DynamicParameters p = new DynamicParameters();
            p.Add("RelationshipType", dalRelationshipModel.RelationshipType);
            p.Add("Age", dalRelationshipModel.Age);
            p.Add("Interests", dalRelationshipModel.Interests);
            p.Add("GenderPreference", dalRelationshipModel.GenderPreference);
            p.Add("ListingId", dalRelationshipModel.ListingId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");



        }

        public async Task<int> DeleteRelationshipListing(int id)
        {
            var storedProcedure = "dbo.DeleteRelationship";
            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id

                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateListing(DALRelationshipModel dalRelationshipModell)
        {
            var storedProcedure = "dbo.EditRelationshipAttributes";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             RelationshipType = dalRelationshipModell.RelationshipType,
                                             Age = dalRelationshipModell.Age,
                                             Interests = dalRelationshipModell.Interests,
                                             GenderPreference = dalRelationshipModell.GenderPreference

                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
