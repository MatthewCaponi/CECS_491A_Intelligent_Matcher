using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public Task<List<UserListTransferModel>> GetUserList()
        {
            var query = "select [ua.Id], [FirstName], [LastName], [Username], [AccountCreationDate]" +
                        "from [UserAccount ua] inner join [UserProfile up]" +
                        "on ua.Id = up.Id;";

            return _dataGateway.LoadData<UserListTransferModel, dynamic>(query,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

    }
}
