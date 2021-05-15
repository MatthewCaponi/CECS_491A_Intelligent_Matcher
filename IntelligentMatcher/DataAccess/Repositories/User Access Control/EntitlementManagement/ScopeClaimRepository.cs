using Dapper;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public class ScopeClaimRepository : IScopeClaimRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public ScopeClaimRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ScopeClaimModel>> GetAllScopeClaims()
        {
            string storedProcedure = "dbo.ScopeClaim_Get_All";

            return await _dataGateway.LoadData<ScopeClaimModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }
        
        public async Task<IEnumerable<ScopeClaimModel>> GetAllScopeClaimsByScopeId(int scopeId)
        {
            string storedProcedure = "dbo.ScopeClaim_Get_AllByScopeId";

            return await _dataGateway.LoadData<ScopeClaimModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              scopeId = scopeId

                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<ScopeClaimModel> GetScopeClaimById(int id)
        {
            string storedProcedure = "dbo.ScopeClaim_Get_ById";

            var row = await _dataGateway.LoadData<ScopeClaimModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateScopeClaim(ScopeClaimModel model)
        {
            var storedProcedure = "dbo.ScopeClaim_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("scopeId", model.ScopeId);
            p.Add("claimId", model.ClaimId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> UpdateScopeClaim(ScopeClaimModel model)
        {
            var storedProcedure = "dbo.ScopeClaim_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             scopeId = model.ScopeId,
                                             claimid = model.ClaimId,
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteScopeClaim(int id)
        {
            var storedProcedure = "dbo.ScopeClaim_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
