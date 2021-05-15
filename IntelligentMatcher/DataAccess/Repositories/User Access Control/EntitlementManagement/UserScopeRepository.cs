using Dapper;
using Exceptions;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.User_Access_Control.EntitlementManagement
{
    public class UserScopeRepository : IUserScopeRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserScopeRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<UserScopeModel>> GetAllUserScopes()
        {
            string storedProcedure = "dbo.UserScope_Get_All";

            return await _dataGateway.LoadData<UserScopeModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<UserScopeModel>> GetAllUserScopesByUserAccountId(int userAccountId)
        {
            string storedProcedure = "dbo.UserScope_Get_All_ByUserAccountId";

            return await _dataGateway.LoadData<UserScopeModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              UserAccountId = userAccountId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<UserScopeModel> GetUserScopeByScopeId(int scopeId)
        {
            string storedProcedure = "dbo.UserScope_Get_ByScopeId";

            var row = await _dataGateway.LoadData<UserScopeModel, dynamic>(storedProcedure,
                new
                {
                    scopeId = scopeId
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateScope(UserScopeModel model)
        {
            try
            {
                var storedProcedure = "dbo.UserScope_Create";

                DynamicParameters p = new DynamicParameters();

                p.Add("Type", model.Type);
                p.Add("UserAccountId", model.UserAccountId);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

                return p.Get<int>("Id");
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.InnerException.Message, e.InnerException);
            }
        }

        public async Task<int> UpdateScope(UserScopeModel model)
        {
            try
            {
                var storedProcedure = "dbo.UserScope_Update";
                return await _dataGateway.Execute(storedProcedure,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Type = model.Type,
                                                 UserAccountId = model.UserAccountId
                                             },
                                             _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("Scope could not be updated.", e.InnerException);
            }
        }

        public async Task<int> DeleteUserScopeByUserScopeId(int userScopeId)
        {
            var storedProcedure = "dbo.UserScope_Delete_ByScopeId";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = userScopeId
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
