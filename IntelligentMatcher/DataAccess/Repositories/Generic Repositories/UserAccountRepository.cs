using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Models;
using System.Threading.Tasks;
using System.Linq;
using Dapper;

namespace DataAccess.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserAccountRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<List<UserAccountModel>> GetAllAccounts()
        {
            var query = "select * from [UserAccount]";

            return await _dataGateway.LoadData<UserAccountModel, dynamic>(query,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<UserAccountModel> GetAccountById(int id)
        {
            var query = "select [Id], [Username], [Password], [EmailAddress]" +
                        "from [UserAccount] where Id = @Id";

            var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<UserAccountModel> GetAccountByUsername(string username)
        {
            var query = "select [Id], [Username], [Password], [EmailAddress]" +
                        "from [UserAccount] where Username = @Username";

            var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(query,
                new
                {
                    Username = username
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<string> GetSaltById(int id)
        {
            var query = "select [Salt]" +
                       "from [UserAccount] where Id = @Id";

            var row = await _dataGateway.LoadData<string, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateAccount(UserAccountModel model)
        {
            var query = "insert into [UserAccount]([Username], [Salt], [EmailAddress])" +
                        "values (@Username, @Salt); set @Id = SCOPE_IDENTITY(); ";
            DynamicParameters p = new DynamicParameters();

            p.Add("Username", model.Username);
            p.Add("Salt", model.Salt);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAccountById(int id)
        {
            var query = "delete from [UserAccount] where Id = @Id";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public Task<int> UpdateAccountUsername(int id, string username)
        {
            var query = "update [UserAccount] set Username = @Username where Id = @Id;";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             Username = username
                                         },
                                         _connectionString.SqlConnectionString);
        }
        public Task<int> UpdateAccountSalt(int id, string salt)
        {
            var query = "update [UserAccount] set Salt = @Salt where Id = @Id;";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             Salt = salt
                                         },
                                         _connectionString.SqlConnectionString);
        }


        public Task<int> UpdateAccountStatus(int id, string accountStatus)
        {
            var query = "update [UserProfile] set AccountStatus = @AccountStatus where UserAccountId = @UserAccountId;";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             AccountStatus = accountStatus.ToString(),
                                             UserAccountId = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public Task<int> UpdateAccountType(int id, string accountType)
        {
            var query = "update [UserAccount] set AccountType = @AccountType where Id = @Id;";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             AccountType = accountType.ToString(),
                                             UserAccountId = id

                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
