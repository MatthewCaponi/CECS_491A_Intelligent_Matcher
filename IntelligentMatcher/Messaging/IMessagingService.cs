using System;
using Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Messaging

{
    public interface IMessagingService
    {

        Task<bool> RemoveUserFromChannel(int channelId, int userId);
        Task<bool> sendMessageAsync(MessageModel model);

        Task<IEnumerable<MessageModel>> GetAllChannelMessages(int ChannelId);

        Task<bool> CreateChannel(ChannelModel model);

        Task<bool> DeleteChannel(int id);

        Task<bool> AddUserToChannel(int UserId, int ChannelId);

        Task<IEnumerable<UserIdModel>> GetAllUsersInChannel(int channelId);

        Task<IEnumerable<ChannelModel>> GetAllUserChannels(int UserId);

        Task<string> GetChannelOwner(int id);
    }
}
