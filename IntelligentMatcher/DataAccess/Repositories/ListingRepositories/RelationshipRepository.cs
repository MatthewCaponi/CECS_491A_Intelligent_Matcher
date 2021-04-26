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

        public Task<DALRelationshipModel> GetListing(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateListing(DALRelationshipModel dalRelationshipModell)
        {
            var storedProcedure = "dbo.EditRelationshipAttributes";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             DalRelationshipModel_RelationshipType = dalRelationshipModell.RelationshipType,
                                             DalRelationshipModel_Age = dalRelationshipModell.Age,
                                             DalRelationshipModel_Interests = dalRelationshipModell.Interests,
                                             DalRelationshipModel_GenderPreference = dalRelationshipModell.GenderPreference

                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
