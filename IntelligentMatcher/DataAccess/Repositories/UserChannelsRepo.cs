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
    public class UserChannelsRepo : IUserChannelsRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;
        public UserChannelsRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }




        public async Task<IEnumerable<UserChannelModel>> GetAllUserChannelsAsync ()
        {
            string storedProcedure = "dbo.UserChannels_Get_All";

            return await _dataGateway.LoadData<UserChannelModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteUserChannelsByIdAsync(int id)
        {
            var storedProcedure = "dbo.UserChannels_Delete_ById";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }




        public async Task<int> AddUserChannelAsync(int UserId, int ChannelId)
        {
            var storedProcedure = "dbo.UserChannel_Add";

            DynamicParameters p = new DynamicParameters();

            p.Add("UserId", UserId);
            p.Add("ChannelId", ChannelId);


            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<int> RemoveUserIdChannelIdAsync(int userId, int channelId)
        {
            var storedProcedure = "dbo.UserChannel_RemoveUser";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             ChannelId = channelId
                                         },
                                         _connectionString.SqlConnectionString);
        }
        public async Task<int> RemoveChannelUsingChannelIdAsync( int channelId)
        {
            var storedProcedure = "dbo.UserChannel_RemoveChannel";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             ChannelId = channelId
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<int>> GetAllChannelsByUserIdAsync(int userId)
        {
            string storedProcedure = "dbo.Channels_Get_UserId";

            return await _dataGateway.LoadData<int, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              UserId = userId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<int>> GetAllUsersByChannelIdAsync(int channelId)
        {
            string storedProcedure = "dbo.Users_Get_ChannelId";

            return await _dataGateway.LoadData<int, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              ChannelId = channelId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }
    }
}
