using Dapper;
using DataAccess;
using DataAccess.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessUnitTestes
{
    public class TestRepo : ITestRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public TestRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> InsertUserAccountTestRows(UserAccountModel model)
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

        public async Task<int> InsertUserProfileTestRows(UserProfileModel model)
        {
            var query = "insert into [UserProfile]([FirstName], [LastName], [DateOfBirth], [AccountCreationDate], [AccountType], [AccountStatus], [UserAccountId])" +
                        "values (@FirstName, @LastName, @DateOfBirth, @AccountCreationDate, @AccountType, @AccountStatus, @UserAccountId); set @Id = SCOPE_IDENTITY(); ";
            DynamicParameters p = new DynamicParameters();

            p.Add("FirstName", model.FirstName);
            p.Add("LastName", model.Surname);
            p.Add("DateOfBirth", model.DateOfBirth);
            p.Add("AccountCreationDate", model.AccountCreationDate);
            p.Add("AccountType", model.accountType);
            p.Add("AccountStatus", model.accountStatus);
            p.Add("UserAccountId", model.userAccountId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteUserProfileTestRows()
        {
            var query = "delete from [UserProfile]; DBCC CHECKIDENT ([UserProfile], RESEED, 0);";

            return _dataGateway.SaveData(query,
                                         new
                                         {

                                         },
                                         _connectionString.SqlConnectionString);
        }

        public Task<int> DeleteUserAccountTestRows()
        {
            var query = "delete from [UserAccount]; DBCC CHECKIDENT ([UserAccount], RESEED, 0);";

            return _dataGateway.SaveData(query,
                                         new
                                         {

                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
