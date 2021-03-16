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
    public class MessagesRepo : IMessagesRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;
        
        public async Task<List<MessageModel>> GetAllMessagesByChannelId(int id)
        {
            var query = "select [Id], [ChannelId], [ChannelMessageId], [UserId], [Message], " +
                        "[Time]" +
                        "from [Messages] where ChannelId = @Id";

            var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(query,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.AsList();
        }
    }
}
