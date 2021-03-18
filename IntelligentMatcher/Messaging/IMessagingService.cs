using System;
using Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Messaging

{
    public interface IMessagingService
    {


         Task<bool> sendMessageAsync(MessageModel model);
        

         Task<IEnumerable<MessageModel>> GetAllChannelMessagesAsync(int ChannelId);
        

         Task<bool> CreateChannelAsync(ChannelModel model);

         Task<bool> DeleteChannelAsync(int id);

         Task<bool> RemoveUserFromChannelAsync(int channelId, int userId);

         Task<bool> AddUserToChannelAsync(int UserId, int ChannelId);
         Task<IEnumerable<UserIdModel>> GetAllUsersInChannelAsync(int channelId);

         Task<IEnumerable<ChannelModel>> GetAllUserChannelsAsync(int UserId);

         Task<int> GetChannelOwnerAsync(int id);
    }
}
