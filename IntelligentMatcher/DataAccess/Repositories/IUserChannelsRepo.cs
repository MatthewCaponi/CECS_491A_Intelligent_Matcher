using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace DataAccess.Repositories
{
    public interface IUserChannelsRepo
    {
        Task<int> AddUserChannel(int UserId, int ChannelId);
        Task<IEnumerable<int>> GetAllChannelsByUserId(int UserId);                               
       Task<IEnumerable<int>> GetAllUsersByChannelId(int channelId);

        Task<int> RemoveUserIdChannelId(int userId, int channelId);

        Task<int> RemoveChannelUsingChannelId(int channelId);

        Task<int> DeleteUserChannelsById(int id);

        Task<IEnumerable<UserChannelModel>> GetAllUserChannels();
    }
}
