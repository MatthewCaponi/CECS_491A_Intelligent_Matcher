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

        public async Task<UserAccountModel> GetUserAccountById(int id)
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

        public async Task<UserAccountModel> GetAccountByEmail(string email)
        {
            var query = "select [Id], [Username], [Password], [EmailAddress]" +
                        "from [UserAccount] where EmailAddress = @EmailAddress";

            var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(query,
                new
                {
                    EmailAddress = email
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> CreateUserAccount(UserAccountModel model)
        {
            var query = "insert into [UserAccount]([Username], [Password], [EmailAddress])" +
                        "values (@Username, @Password, @EmailAddress); set @Id = SCOPE_IDENTITY(); ";
            DynamicParameters p = new DynamicParameters();

            p.Add("Username", model.Username);
            p.Add("Password", model.Password);
            p.Add("EmailAddress", model.EmailAddress);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public Task<int> UpdateAccountPassword(int id, string password)
        {
            var query = "update [UserAccount] set Password = @Password where Id = @Id;";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             Password = password
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public Task<int> DeleteUserAccountById(int id)
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
        public Task<int> UpdateAccountEmail(int id, string email)
        {
            var query = "update [UserAccount] set EmailAddress = @EmailAddress where Id = @Id;";

            return _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id,
                                             EmailAddress = email
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
