using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
namespace DataAccess.Repositories
{
    public class MessagingService : IMessagingService
    {

        public async Task<MessageModel> GetMessagesByGroupId(int id)
        {
            var query = "select [Id], [Username], [Password], [Salt], [EmailAddress], " +
                        "[AccountType], [AccountStatus], [CreationDate], [UpdationDate]" +
                        "from [UserAccount] where Id = @Id";

            var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

    }
}
