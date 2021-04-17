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
    public class AccessPolicyPairingRepository : IAccessPolicyPairingRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public AccessPolicyPairingRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<AccessPolicyPairingModel>> GetAllAccessPoliciesPairings()
        {
            string storedProcedure = "dbo.AccessPolicyPairing_Get_All";

            return await _dataGateway.LoadData<AccessPolicyPairingModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<AccessPolicyPairingModel> GetAccessPolicyPairingById(int id)
        {
            string storedProcedure = "dbo.AccessPolicyPairing_Get_ById";

            var row = await _dataGateway.LoadData<AccessPolicyPairingModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateAccessPolicyPairing(AccessPolicyPairingModel model)
        {
            var storedProcedure = "dbo.AccessPolicyPairing_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("scopeId", model.ScopeId);
            p.Add("claimId", model.ClaimId);
            p.Add("accessPolicyId", model.AccessPolicyId);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> UpdateAccessPolicyPairing(AccessPolicyPairingModel model)
        {
            var storedProcedure = "dbo.AccessPolicyPairing_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             scopeId = model.ScopeId,
                                             claimId = model.ClaimId,
                                             accessPolicyId = model.AccessPolicyId
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteAccessPairingPolicy(int id)
        {
            var storedProcedure = "dbo.AccessPolicyPairing_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
