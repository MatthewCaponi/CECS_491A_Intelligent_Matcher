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
    public class UserScopeClaimRepository : IUserScopeClaimRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserScopeClaimRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<UserScopeClaimModel>> GetAllUserUserScopeClaims()
        {
            string storedProcedure = "dbo.UserScopeClaim_Get_All";

            return await _dataGateway.LoadData<UserScopeClaimModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<UserScopeClaimModel> GetUserScopeClaimById(int id)
        {
            string storedProcedure = "dbo.UserScopeClaim_Get_ById";

            var row = await _dataGateway.LoadData<UserScopeClaimModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateUserScopeClaim(UserScopeClaimModel model)
        {
            var storedProcedure = "dbo.UserScopeClaim_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("userAccountId", model.UserAccountId);
            p.Add("scopeClaimId", model.ScopeClaimId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> UpdateUserScopeClaim(UserScopeClaimModel model)
        {
            var storedProcedure = "dbo.UserScopeClaim_Update";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = model.Id,
                                             userAccountId = model.UserAccountId,
                                             scopeClaimId = model.ScopeClaimId
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteUserScopeClaim(int id)
        {
            var storedProcedure = "dbo.UserScopeClaim_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
