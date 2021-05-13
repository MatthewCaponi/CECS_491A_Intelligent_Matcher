using Dapper;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public ClaimRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ClaimModel>> GetAllClaims()
        {
            string storedProcedure = "dbo.Claim_Get_All";

            return await _dataGateway.LoadData<ClaimModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<ClaimModel> GetClaimById(int id)
        {
            string storedProcedure = "dbo.Claim_Get_ById";

            var row = await _dataGateway.LoadData<ClaimModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateClaim(ClaimModel model)
        {
            var storedProcedure = "dbo.Claim_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("type", model.Type);
            p.Add("value", model.Value);
            p.Add("isDefault", model.IsDefault);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> UpdateClaim(ClaimModel model)
        {
            var storedProcedure = "dbo.Claim_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             type = model.Type,
                                             value = model.Value,
                                             isDefault = model.IsDefault
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteClaim(int id)
        {
            var storedProcedure = "dbo.Claim_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
