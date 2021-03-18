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
    public class ChannelsRepo : IChannelsRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;
        public ChannelsRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> CreateChannel(ChannelModel model)
        {
            var storedProcedure = "dbo.Channels_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("OwnerId", model.OwnerId);
            p.Add("Name", model.Name);


            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<ChannelModel> GetChannelbyId(int id)
        {
            string storedProcedure = "dbo.Channel_Get_Id";

            var row = await _dataGateway.LoadData<ChannelModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<string> GetChannelOwnerbyId(int id)
        {
            string storedProcedure = "dbo.ChannelOwner_Get_Id";

            var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> DeleteChannelbyId(int id)
        {
            var storedProcedure = "dbo.Channel_Delete";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}
