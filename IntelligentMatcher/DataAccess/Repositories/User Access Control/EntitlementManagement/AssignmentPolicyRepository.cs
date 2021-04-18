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
    public class AssignmentPolicyRepository : IAssignmentPolicyRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public AssignmentPolicyRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<AssignmentPolicyModel>> GetAllAssignmentPolicies()
        {
            string storedProcedure = "dbo.AssignmentPolicy_Get_All";

            return await _dataGateway.LoadData<AssignmentPolicyModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<AssignmentPolicyModel> GetAssignmentPolicyById(int id)
        {
            string storedProcedure = "dbo.AssignmentPolicy_Get_ById";

            var row = await _dataGateway.LoadData<AssignmentPolicyModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateAssignmentPolicy(AssignmentPolicyModel model)
        {
            var storedProcedure = "dbo.AssignmentPolicy_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("name", model.Name);
            p.Add("isDefault", model.IsDefault);
            p.Add("requiredAccountType", model.RequiredAccountType);
            p.Add("priority", model.Priority);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> UpdateAssignmentPolicy(AssignmentPolicyModel model)
        {
            var storedProcedure = "dbo.AssignmentPolicy_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             name = model.Name,
                                             isDefault = model.IsDefault,
                                             requiredAccountType = model.RequiredAccountType,
                                             priority = model.Priority
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteAssignmentPolicy(int id)
        {
            var storedProcedure = "dbo.AssignmentPolicy_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
