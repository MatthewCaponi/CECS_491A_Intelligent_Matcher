using Dapper;
using Models;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using Exceptions;

namespace DataAccess.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserProfileRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<UserProfileModel>> GetAllUserProfiles()
        {
            try
            {
                var storedProcedure = "dbo.UserProfile_Get_All";

                return await _dataGateway.LoadData<UserProfileModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<UserProfileModel> GetUserProfileById(int id)
        {
            try
            {
                var storedProcedure = "dbo.UserProfile_Get_ById";

                var row = await _dataGateway.LoadData<UserProfileModel, dynamic>(storedProcedure,
                    new
                    {
                        Id = id
                    },
                    _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<UserProfileModel> GetUserProfileByAccountId(int accountId)
        {
            try
            {
                var storedProcedure = "dbo.UserProfile_Get_ByAccountId";

                var row = await _dataGateway.LoadData<UserProfileModel, dynamic>(storedProcedure,
                    new
                    {
                        UserAccountId = accountId
                    },
                    _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> CreateUserProfile(UserProfileModel model)
        {
            try
            {
                var storedProcedure = "dbo.UserProfile_Create";

                DynamicParameters p = new DynamicParameters();

                p.Add("FirstName", model.FirstName);
                p.Add("Surname", model.Surname);
                p.Add("DateOfBirth", model.DateOfBirth);
                p.Add("UserAccountId", model.UserAccountId);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);
                return p.Get<int>("Id");
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> DeleteUserProfileByAccountId(int userAccountId)
        {
            try
            {
                var storedProcedure = "dbo.UserProfile_Delete_ById";

                return await _dataGateway.Execute(storedProcedure,
                                             new
                                             {
                                                 UserAccountId = userAccountId
                                             },
                                             _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }
    }
}
