using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserAccountCodeRepository : IUserAccountCodeRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserAccountCodeRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> CreateUserAccountCode(UserAccountCodeModel model)
        {
            try
            {
                var storedProcedure = "dbo.UserAccountCode_Create";
                DynamicParameters p = new DynamicParameters();

                p.Add("Code", model.Code);
                p.Add("ExpirationTime", model.ExpirationTime);
                p.Add("UserAccountId", model.UserAccountId);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                return await _dataGateway.SaveData(storedProcedure, p, _connectionString.SqlConnectionString);
            }
            catch
            {
                return default;
            }
        }

        public async Task<int> DeleteUserAccountCodeById(int id)
        {
            try
            {
                var storedProcedure = "dbo.UserAccountCode_Delete_ById";

                return await _dataGateway.SaveData(storedProcedure,
                                             new
                                             {
                                                 Id = id
                                             },
                                             _connectionString.SqlConnectionString);
            }
            catch
            {
                return default;
            }
        }

        public async Task<int> DeleteUserAccountCodeByAccountId(int accountId)
        {
            try
            {
                var storedProcedure = "dbo.UserAccountCode_Delete_ByUserAccountId";

                return await _dataGateway.SaveData(storedProcedure,
                                             new
                                             {
                                                 UserAccountId = accountId
                                             },
                                             _connectionString.SqlConnectionString);
            }
            catch
            {
                return default;
            }
        }

        public async Task<IEnumerable<UserAccountCodeModel>> GetAllUserAccountCodes()
        {
            try
            {
                string storedProcedure = "dbo.UserAccountCode_Get_All";

                return await _dataGateway.LoadData<UserAccountCodeModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);
            }
            catch
            {
                return default;
            }
        }

        public async Task<UserAccountCodeModel> GetUserAccountCodeById(int id)
        {
            try
            {
                string storedProcedure = "dbo.UserAccountCode_Get_ById";

                var row = await _dataGateway.LoadData<UserAccountCodeModel, dynamic>(storedProcedure,
                    new
                    {
                        Id = id
                    },
                    _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch
            {
                return default;
            }
        }

        public async Task<UserAccountCodeModel> GetUserAccountCodeByAccountId(int accountId)
        {
            try
            {
                string storedProcedure = "dbo.UserAccountCode_Get_ByUserAccountId";

                var row = await _dataGateway.LoadData<UserAccountCodeModel, dynamic>(storedProcedure,
                    new
                    {
                        UserAccountId = accountId
                    },
                    _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch
            {
                return default;
            }
        }
    }
}
