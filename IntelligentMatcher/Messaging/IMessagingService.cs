using System;
using Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Messaging

{
    public interface IMessagingService
    {


         Task<bool> SendMessageAsync(MessageModel model);
        

         Task<IEnumerable<MessageModel>> GetAllChannelMessagesAsync(int channelId);
        

         Task<bool> CreateChannelAsync(ChannelModel model);

         Task<bool> DeleteChannelAsync(int id);

         Task<bool> RemoveUserFromChannelAsync(int channelId, int userId);

         Task<bool> AddUserToChannelAsync(int userId, int channelId);
         Task<IEnumerable<UserIdModel>> GetAllUsersInChannelAsync(int channelId);

         Task<IEnumerable<ChannelModel>> GetAllUserChannelsAsync(int userId);

         Task<int> GetChannelOwnerAsync(int id);
    }
}
