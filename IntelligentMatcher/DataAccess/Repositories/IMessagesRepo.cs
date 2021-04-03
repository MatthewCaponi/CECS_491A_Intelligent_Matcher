using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Repositories
{
    public interface IMessagesRepo
    {

        Task<IEnumerable<MessageModel>> GetAllMessagesByChannelIdAsync(int id);

        Task<int> CreateMessageAsync(MessageModel model);

        Task<IEnumerable<MessageModel>> GetAllMessagesAsync();

        Task<int> DeleteMessageByIdAsync(int id);
    }
}
