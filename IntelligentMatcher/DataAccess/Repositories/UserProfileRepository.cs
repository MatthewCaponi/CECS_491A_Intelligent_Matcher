using Dapper;
using Models;
using System.Linq;
using System.Threading.Tasks;
using System.Data;



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

        public async Task<UserProfileModel> GetUserProfileById(int id)
        {
            var query = "select [Id], [FirstName], [LastName], [DateOfBirth], [AccountCreationType], [AccountType], [AccountStatus]" +
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
            var query = "select [Id], [FirstName], [LastName], [DateOfBirth], [AccountCreationDate], [AccountType], [AccountStatus], [UserAccountId]" +
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
            var query = "insert into [UserProfile]([FirstName], [LastName], [DateOfBirth], [AccountCreationDate], [AccountType], [AccountStatus], [UserAccountId])" +
                        "values (@FirstName, @LastName, @DateOfBirth, @AccountCreationDate, @AccountType, @AccountStatus, @UserAccountId); set @Id = SCOPE_IDENTITY(); ";
            DynamicParameters p = new DynamicParameters();

            p.Add("FirstName", model.FirstName);
            p.Add("LastName", model.LastName);
            p.Add("DateOfBirth", model.DateOfBirth);
            p.Add("AccountCreationDate", model.AccountCreationDate);
            p.Add("AccountType", model.accountType);
            p.Add("AccountStatus", model.accountStatus);
            p.Add("UserAccountId", model.userAccountId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public Task<int> UpdateUserAccountType(int id, UserProfileModel.AccountType accountType)
        {
            var query = "update [UserProfile]" +
                        "set AccountType = @AccountType" +
                        "where Id = @Id;";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             AccountType = accountType.ToString()
                                         },
                                         _connectionString.SqlConnectionString); ;
        }

        public Task<int> UpdateUserAccountStatus(int id, UserProfileModel.AccountStatus accountStatus)
        {
            var query = "update [UserProfile]" +
                        "set AccountStatus = @AccountStatus" +
                        "where Id = @Id;";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             AccountStatus = accountStatus.ToString()
                                         },
                                         _connectionString.SqlConnectionString); ;
        }

        public Task<int> DeleteUserProfileById(int id)
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
