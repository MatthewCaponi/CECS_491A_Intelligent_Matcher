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
        public MessagesRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }


        public async Task<IEnumerable<MessageModel>> GetAllMessagesAsync()
        {
            string storedProcedure = "dbo.Messages_Get_All";

            return await _dataGateway.LoadData<MessageModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteMessageByIdAsync(int id)
        {
            var storedProcedure = "dbo.Messages_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<MessageModel>> GetAllMessagesByChannelIdAsync(int channelId)
        {
            string storedProcedure = "dbo.Messages_Get_All_By_ChannelId";

            return await _dataGateway.LoadData<MessageModel, dynamic>(storedProcedure,
                                                                          new {
                                                                              ChannelId = channelId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> CreateMessageAsync(MessageModel model)
        {
            var storedProcedure = "dbo.Messages_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("ChannelId", model.ChannelId);
            p.Add("ChannelMessageId", model.ChannelMessageId);
            p.Add("UserId", model.UserId);
            p.Add("Message", model.Message);
            p.Add("Time", model.Time);
            p.Add("Date", model.Date);
            p.Add("Username", model.Username);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }
    }
}
