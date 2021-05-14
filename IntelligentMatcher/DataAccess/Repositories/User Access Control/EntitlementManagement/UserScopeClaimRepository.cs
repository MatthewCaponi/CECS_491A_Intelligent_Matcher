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

        public async Task<IEnumerable<UserScopeClaimModel>> GetAllUserScopeClaims()
        {
            string storedProcedure = "dbo.UserScopeClaim_Get_All";

            return await _dataGateway.LoadData<UserScopeClaimModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<UserScopeClaimModel>> GetAllUserScopeClaimsByAccountId(int id)
        {
            string storedProcedure = "dbo.UserScopeClaim_Get_All_ByAccountId";

            return await _dataGateway.LoadData<UserScopeClaimModel, dynamic>(storedProcedure,
                                                                          new 
                                                                          {
                                                                              userAccountId = id
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<UserScopeClaimModel>> GetAllUserScopeClaimsByAccountIdAndRole(int id, string role)
        {
            string storedProcedure = "dbo.UserScopeClaim_Get_All_ByAccountIdAndRole";

            return await _dataGateway.LoadData<UserScopeClaimModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              userAccountId = id,
                                                                              role = role
                                                                          },
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
            p.Add("userScopeId", model.UserScopeId);
            p.Add("userClaimId", model.UserClaimId);
            p.Add("role", model.Role);
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
                                             userScopeId = model.UserScopeId,
                                             userClaimId = model.UserClaimId,
                                             role = model.Role
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
