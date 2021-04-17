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
    public class AccessPolicyRepository : IAccessPolicyRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public AccessPolicyRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<AccessPolicyModel>> GetAllAccessPolicies()
        {
            string storedProcedure = "dbo.AccessPolicy_Get_All";

            return await _dataGateway.LoadData<AccessPolicyModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<AccessPolicyModel> GetAccessPolicyById(int id)
        {
            string storedProcedure = "dbo.AccessPolicy_Get_ById";

            var row = await _dataGateway.LoadData<AccessPolicyModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateAccessPolicy(AccessPolicyModel model)
        {
            var storedProcedure = "dbo.AccessPolicy_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("name", model.Name);
            p.Add("resourceId", model.ResourceId);
            p.Add("priority", model.Priority);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> UpdateAccessPolicy(AccessPolicyModel model)
        {
            var storedProcedure = "dbo.AccessPolicy_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             name = model.Name,
                                             resourceId = model.ResourceId,
                                             priority = model.Priority
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteAccessPolicy(int id)
        {
            var storedProcedure = "dbo.AccessPolicy_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
