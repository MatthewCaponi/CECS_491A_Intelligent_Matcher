using Dapper;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


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



        public async Task<int> CreateListing(DALDatingModel dalDatingModel)
        {

            string storedProcedure = "dbo.CreateDatingListing";

            DynamicParameters p = new DynamicParameters();
            p.Add("SexualOrientationPreference", dalDatingModel.SexualOrientationPreference);
            p.Add("LookingFor", dalDatingModel.LookingFor);
            p.Add("ListingId", dalDatingModel.ListingId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

   

        public async Task<int> UpdateListing(DALDatingModel dalDatingModel)
        {
            var storedProcedure = "dbo.EditDatingAttributes";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             DalDatingModel_SexualOrientationPreference = dalDatingModel.SexualOrientationPreference,
                                             DalDatingModel_LookingFor = dalDatingModel.LookingFor
                                         },
                                         _connectionString.SqlConnectionString) ;
        }
    }
}
