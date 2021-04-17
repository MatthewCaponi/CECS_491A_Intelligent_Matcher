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
    public class ScopeRepository : IScopeRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public ScopeRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ScopeModel>> GetAllScopes()
        {
            string storedProcedure = "dbo.Scope_Get_All";

            return await _dataGateway.LoadData<ScopeModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<ScopeModel> GetScopeById(int id)
        {
            string storedProcedure = "dbo.Scope_Get_ById";

            var row = await _dataGateway.LoadData<ScopeModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateScope(ScopeModel model)
        {
            var storedProcedure = "dbo.Scope_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("name", model.Name);
            p.Add("description", model.Description);
            p.Add("isDefault", model.IsDefault);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> UpdateScope(ScopeModel model)
        {
            var storedProcedure = "dbo.Scope_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             name = model.Name,
                                             description = model.Description,
                                             isDefault = model.IsDefault
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteScope(int id)
        {
            var storedProcedure = "dbo.Scope_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
