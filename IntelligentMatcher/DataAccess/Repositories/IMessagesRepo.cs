using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Repositories
{
    public interface IMessagesRepo
    {

        Task<IEnumerable<MessageModel>> GetAllMessagesByChannelId(int id);

        Task<int> CreateAccount(MessageModel model);
    }
}
