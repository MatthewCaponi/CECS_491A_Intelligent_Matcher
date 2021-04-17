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
    public class AssignmentPolicyPairingRepository : IAssignmentPolicyPairingRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public AssignmentPolicyPairingRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<AssignmentPolicyPairingModel>> GetAllAssignmentPolicyPairings()
        {
            string storedProcedure = "dbo.AssignmentPolicyPairing_Get_All";

            return await _dataGateway.LoadData<AssignmentPolicyPairingModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<AssignmentPolicyPairingModel> GetAssignmentPolicyPairingById(int id)
        {
            string storedProcedure = "dbo.AssignmentPolicyPairing_Get_ById";

            var row = await _dataGateway.LoadData<AssignmentPolicyPairingModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateAssignmentPolicyPairing(AssignmentPolicyPairingModel model)
        {
            var storedProcedure = "dbo.AssignmentPolicyPairing_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("policyId", model.PolicyId);
            p.Add("scopeId", model.ScopeId);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> UpdateAssignmentPolicyPairing(AssignmentPolicyPairingModel model)
        {
            var storedProcedure = "dbo.AssignmentPolicyPairing_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             policyId = model.PolicyId,
                                             scopeId = model.ScopeId
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteAssignmentPolicyPairing(int id)
        {
            var storedProcedure = "dbo.AssignmentPolicyPairing_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
