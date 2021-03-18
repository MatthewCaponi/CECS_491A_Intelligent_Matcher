using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace DataAccess.Repositories
{
    public interface IUserChannelsRepo
    {
        Task<int> AddUserChannelAsync(int UserId, int ChannelId);
        Task<IEnumerable<int>> GetAllChannelsByUserIdAsync(int UserId);                               
       Task<IEnumerable<int>> GetAllUsersByChannelIdAsync(int channelId);

        Task<int> RemoveUserIdChannelIdAsync(int userId, int channelId);

        Task<int> RemoveChannelUsingChannelIdAsync(int channelId);

        Task<int> DeleteUserChannelsByIdAsync(int id);

        Task<IEnumerable<UserChannelModel>> GetAllUserChannelsAsync();
    }
}
