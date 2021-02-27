using Models.App_Specific_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.App_Specific_Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;
        public async Task<UserInfoModel> GetAccountByEmail(string email)
        {
            var query = "select [Id], [Username], [Password], [EmailAddress]" +
                        "from [UserAccount] where EmailAddress = @EmailAddress";

            var row = await _dataGateway.LoadData<UserInfoModel, dynamic>(query,
                new
                {
                    EmailAddress = email
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
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
