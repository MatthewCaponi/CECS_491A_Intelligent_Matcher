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
    public class UserClaimRepository : IUserClaimRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserClaimRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<UserClaimModel>> GetAllUserClaims()
        {
            string storedProcedure = "dbo.UserClaim_Get_All";

            return await _dataGateway.LoadData<UserClaimModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<UserClaimModel>> GetAllUserClaimsByUserAccountId(int userAccountId)
        {
            string storedProcedure = "dbo.UserClaim_Get_All_ByUserAccountId";

            return await _dataGateway.LoadData<UserClaimModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              UserAccountId = userAccountId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<UserClaimModel> GetUserClaimByUserClaimId(int userClaim)
        {
            string storedProcedure = "dbo.UserClaim_Get_ByUserClaimId";

            var row = await _dataGateway.LoadData<UserClaimModel, dynamic>(storedProcedure,
                new
                {
                    Id = userClaim
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateUserClaim(UserClaimModel model)
        {
            try
            {
                var storedProcedure = "dbo.UserClaim_Create";

                DynamicParameters p = new DynamicParameters();

                p.Add("Type", model.Type);
                p.Add("Value", model.Value);
                p.Add("UserAccountId", model.UserAccountId);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

                return p.Get<int>("Id");
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("UserClaim could not be created.", e.InnerException);
            }
        }

        public async Task<int> UpdateUserClaim(UserClaimModel model)
        {
            try
            {
                var storedProcedure = "dbo.UserClaim_Update";
                return await _dataGateway.Execute(storedProcedure,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Type = model.Type,
                                                 Value = model.Value,
                                                 UserAccountId = model.UserAccountId
                                             },
                                             _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException("UserClaim could not be updated.", e.InnerException);
            }
        }

        public async Task<int> DeleteUserClaimByUserClaimId(int userClaimId)
        {
            var storedProcedure = "dbo.UserClaim_Delete_ByUserClaimId";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = userClaimId
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
