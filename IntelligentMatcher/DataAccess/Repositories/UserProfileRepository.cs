using Dapper;
using Models;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;

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
            var storedProcedure = "dbo.UserProfile_Get_All";

            return await _dataGateway.LoadData<UserProfileModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<UserProfileModel> GetUserProfileById(int id)
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

        public async Task<UserProfileModel> GetUserProfileByAccountId(int accountId)
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

        public async Task<int> CreateUserProfile(UserProfileModel model)
        {
            var storedProcedure = "dbo.UserProfile_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("FirstName", model.FirstName);
            p.Add("Surname", model.Surname);
            p.Add("DateOfBirth", model.DateOfBirth);
            p.Add("UserAccountId", model.UserAccountId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(storedProcedure, p, _connectionString.SqlConnectionString);
            return p.Get<int>("Id");
        }

        public async Task<int> DeleteUserProfileByAccountId(int userAccountId)
        {
            var storedProcedure = "dbo.UserProfile_Delete_ById";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             UserAccountId = userAccountId
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
