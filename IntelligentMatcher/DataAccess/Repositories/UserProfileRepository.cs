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

        public async Task<List<UserProfileModel>> GetAllUserProfiles()
        {
            var query = "select [Id], [FirstName], [Surname], [DateOfBirth], " +
                "[UserAccountId] from [UserProfile]";

            return await _dataGateway.LoadData<UserProfileModel, dynamic>(query,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<UserProfileModel> GetUserProfileById(int id)
        {
            var query = "select [Id], [FirstName], [Surname], [DateOfBirth], [UserAccountId]" +
                        "from [UserProfile] where Id = @Id";

            var row = await _dataGateway.LoadData<UserProfileModel, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<UserProfileModel> GetUserProfileByAccountId(int accountId)
        {
            var query = "select [Id], [FirstName], [Surname], [DateOfBirth], [UserAccountId]" +
                        "from [UserProfile] where UserAccountId = @UserAccountId";

            var row = await _dataGateway.LoadData<UserProfileModel, dynamic>(query,
                new
                {
                    UserAccountId = accountId
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateUserProfile(UserProfileModel model)
        {
            var query = "insert into [UserProfile]([FirstName], [Surname], [DateOfBirth], [UserAccountId])" +
                        "values (@FirstName, @Surname, @DateOfBirth, @UserAccountId); " +
                        "set @Id = SCOPE_IDENTITY(); ";

            DynamicParameters p = new DynamicParameters();

            p.Add("FirstName", model.FirstName);
            p.Add("Surname", model.Surname);
            p.Add("DateOfBirth", model.DateOfBirth);
            p.Add("UserAccountId", model.UserAccountId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);
            return p.Get<int>("Id");
        }

        public Task<int> DeleteUserProfile(int id)
        {
            var query = "delete from [UserProfile] where Id = @Id";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
