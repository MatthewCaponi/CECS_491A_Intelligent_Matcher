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
    public interface IChannelsRepo
    {

        Task<IEnumerable<ChannelModel>> GetAllChannels();
        Task<int> CreateChannel(ChannelModel model);
        Task<ChannelModel> GetChannelbyId(int id);
        Task<int> DeleteChannelbyId(int id);

        Task<string> GetChannelOwnerbyId(int id);
    }
}
