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

        Task<IEnumerable<ChannelModel>> GetAllChannelsAsync();
        Task<int> CreateChannelAsync(ChannelModel model);
        Task<ChannelModel> GetChannelbyIdAsync(int id);
        Task<int> DeleteChannelbyIdAsync(int id);

        Task<int> GetChannelOwnerbyIdAsync(int id);
    }
}
